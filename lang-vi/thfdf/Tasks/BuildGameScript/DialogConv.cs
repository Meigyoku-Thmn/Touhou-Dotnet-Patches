using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

public class DialogConv
{
   class TextData
   {
      [Ignore] public string Name { get { return SC; } set { SC = value; } } // alias for SC
      public string SC { get; set; }
      public string JP { get; set; }
      public string EN { get; set; }
      public string VN { get; set; }
   }
   class LineGroup
   {
      public List<TextData> Lines = new List<TextData>();
      public string FileName;
   }
   static bool Test(string sc, string jp, string vn, string en)
   {
      var scTag = sc.Split(']')[0];
      var jpTag = jp.Split(']')[0];
      var vnTag = vn.Split(']')[0];
      var enTag = en.Split(']')[0];
      return scTag.Equals(jpTag) && jpTag.Equals(vnTag) && vnTag.Equals(enTag);
   }
   public static void Run(string inputFilePath, string outputDirPath)
   {
      Console.WriteLine("Making dialog files...");
      using (var reader = new StreamReader(new FileStream(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
      using (var csv = new CsvReader(reader))
      {
         csv.Configuration.HasHeaderRecord = true;
         var records = csv.GetRecords<TextData>();
         var lineGroups = records
            .Select(e =>
            {
               e.JP = e.JP.Trim();
               e.SC = e.SC.Trim();
               e.VN = e.VN.Trim();
               e.EN = e.EN.Trim();
               return e;
            })
            .Aggregate(new List<LineGroup>() { new LineGroup() }, (acc, textData) =>
            {
               if (textData.Name.Length == 0)
                  acc.Add(new LineGroup());
               else if (textData.Name.Length != 0 && textData.JP.Length == 0 && textData.VN.Length == 0 && textData.EN.Length == 0)
                  acc.Last().FileName = textData.Name;
               else if (Test(textData.SC, textData.JP, textData.VN, textData.EN))
                  acc.Last().Lines.Add(textData);
               else throw new Exception("Inconsistent tag!");
               return acc;
            })
            .Where(e => e.FileName != null);
         foreach (var lineGroup in lineGroups)
         {
            using (var outputFile = new StreamWriter(Path.Combine(outputDirPath, lineGroup.FileName), false, Encoding.UTF8))
            {
               foreach (var line in lineGroup.Lines.Select((e, i) => new { i, d = e.VN }))
               {
                  outputFile.Write(line.d);
                  if (line.i + 1 < lineGroup.Lines.Count) outputFile.WriteLine();
               }
            }
         }
         Console.WriteLine($" {lineGroups.Count()} dialog file(s) generated.");
      }
   }
}

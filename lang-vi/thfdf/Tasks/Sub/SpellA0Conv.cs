//css_nuget CsvHelper
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

public class SpellA0Conv
{
   class TextData
   {
      [Index(0)] public string SC { get; set; }
      [Index(1)] public string JP { get; set; }
      [Index(2)] public string EN { get; set; }
      [Index(3)] public string VN { get; set; }
   }
   public static void Run(string inputFilePath, string outputFilePath)
   {
      return;
      Console.WriteLine("Making music room file...");
      using (var reader = new StreamReader(new FileStream(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
      using (var csv = new CsvReader(reader))
      {
         csv.Configuration.HasHeaderRecord = true;
         var records = csv.GetRecords<TextData>();
         var comments = records
            .Select(e =>
            {
               e.JP = e.JP.Trim();
               e.SC = e.SC.Trim();
               e.EN = e.EN.Trim();
               e.VN = e.VN.Trim() + "]";
               return e;
            })
            .ToArray() // you have to execute the linq for csvhelper right here or error would occur
            ;
         using (var outputFile = new StreamWriter(outputFilePath, false, Encoding.UTF8))
         {
            foreach (var comment in comments.Select((e, i) => new { i, VN = e.VN }))
            {
               outputFile.Write(comment.VN);
               if (comment.i + 1 < comments.Count()) outputFile.WriteLine();
            }
         }
         Console.WriteLine($" {outputFilePath} generated.");
      }
   }
}

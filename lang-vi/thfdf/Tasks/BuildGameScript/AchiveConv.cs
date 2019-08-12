using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

public class AchiveConv {
   class TextData {
      public string Group { get; set; }
      public string SC { get; set; }
      public string JP { get; set; }
      public string EN { get; set; }
      public string VN { get; set; }
   }
   static Dictionary<string, int> GroupMap = new Dictionary<string, int>() {
      { "Normal", 1 }, { "Challenge", 2 }, { "Hidden", 3 }
   };
   public static void Run(string inputFilePath, string outputFilePath) {
      Console.WriteLine("Making achivement file...");
      using (var reader = new StreamReader(new FileStream(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
      using (var csv = new CsvReader(reader)) {
         csv.Configuration.HasHeaderRecord = true;
         var records = csv.GetRecords<TextData>();
         var achiveGroups = records
            .Select((e, i) => {
               var lines = e.VN.Split(new [] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(line => line.Trim());
               if (lines.Count() < 2) throw new Exception($"Record {i} doesn't have enough 2 line!");
               int group;
               if (!GroupMap.TryGetValue(e.Group.Trim(), out group))
                  throw new Exception($"Unknown group name: {e.Group}");
               return new { Group = group, VN = $"{lines.ElementAt(0)}：{lines.ElementAt(1)}" };
            })
            .ToArray() // you have to execute the linq for csvhelper right here or error would occur
            .GroupBy(record => record.Group)
            .OrderBy(group => group.Key)
            ;
         using (var outputFile = new StreamWriter(outputFilePath, false, Encoding.UTF8)) {
            foreach (var achiveGroup in achiveGroups.Select((achiveGroup, i) => new { i, set = achiveGroup })) {
               outputFile.WriteLine(achiveGroup.set.Count());
               foreach (var description in achiveGroup.set.Select((des, i) => new { i, des.VN })) {
                  outputFile.Write(description.VN);
                  if (description.i + 1 < achiveGroup.set.Count()) outputFile.WriteLine();
               }
               if (achiveGroup.i + 1 < achiveGroups.Count()) outputFile.WriteLine();
            }
         }
         Console.WriteLine($" {outputFilePath} generated.");
      }
   }
}


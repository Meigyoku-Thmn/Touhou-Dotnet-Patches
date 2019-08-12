﻿using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

public class SpellA0Conv {
   private enum Difficulty {
      Easy = 1, Normal = 2, Hard = 3, Lunatic = 4, Extra = 5, Challenge = 6
   }
   class A0
   {
      public string Options { get; set; }
      public string SC { get; set; }
      public string JP { get; set; }
      public string EN { get; set; }
      public string VN { get; set; }
   }
   class TextData {
      public int Stage { get; set; }
      [TypeConverter(typeof(DifficultyConv))]
      public Difficulty[] Difficulties { get; set; }
      [TypeConverter(typeof(SpellIdConv))]
      public string[] BarrageIds { get; set; }
      public string SC { get; set; }
      public string JP { get; set; }
      public string EN { get; set; }
      public string VN { get; set; }
      public int? DoExport2A0 { get; set; }
   }
   public class DifficultyConv : DefaultTypeConverter {
      public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData) {
         return text.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(str => (Difficulty)Enum.Parse(typeof(Difficulty), str.Trim())).ToArray();
      }
   }
   public class SpellIdConv : DefaultTypeConverter {
      public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData) {
         return text.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries).Select(str => str.Trim()).ToArray();
      }
   }
   static Regex NewLine = new Regex(@"\n", RegexOptions.Compiled);
   public static void Run(string inputFilePath, string a0InputFilePath, string outputDirPath) {
      Console.WriteLine("Making spellcard files and a0 file...");
      var exportedMap = new Dictionary<int, string>();
      using (var reader = new StreamReader(new FileStream(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
      using (var csv = new CsvReader(reader)) {
         csv.Configuration.HasHeaderRecord = true;
         var records = csv.GetRecords<TextData>();
         var spells = records
            .Select((e, i) => {
               e.VN = e.VN.Trim();
               if (NewLine.Match(e.VN).Success)
                  throw new Exception($"There is a newline character at spell {i}");
               if (e.DoExport2A0 != null) exportedMap.Add(e.DoExport2A0.Value, e.VN);
               return e;
            })
            .ToArray() // you have to execute the linq for csvhelper right here or error would occur
            .OrderBy(record => record.Stage)
            ;
         var linesCollection =  new List<string>[6].Select(e => new List<string>()).ToArray();
         foreach (var spell in spells) {
            foreach (var diffWrapper in spell.Difficulties.Select((diff, i) => new { i, diff })) {
               var baseId = spell.BarrageIds[diffWrapper.i];
               var diff = diffWrapper.diff;
               var id = diff == Difficulty.Challenge
                  ? baseId.Split(new [] {'\\' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim()
                  : spell.Stage + baseId;
               linesCollection[(int)diff - 1].Add($"{id} {spell.VN}");
            }
         }
         for (var i = 1; i <= linesCollection.Length; i++) {
            var lines = linesCollection[i - 1];
            File.WriteAllText(Path.Combine(outputDirPath, $"s{i}.txt"), string.Join("\r\n", lines), Encoding.UTF8);
         }
         Console.WriteLine($" {linesCollection.Length} files generated.");
         var challengeLines = spells
            .Where(spell => spell.Difficulties.Any(diff => diff == Difficulty.Challenge))
            .Select(spell =>
            {
               var ids = spell.BarrageIds[Array.IndexOf(spell.Difficulties, Difficulty.Challenge)].Split(new [] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
               var stageId = ids[0].Trim();
               var id = ids[1].Trim();
               return new { stageId, id, spell.VN, spell.Stage };
            })
            .GroupBy(spell => spell.Stage)
            .Aggregate(new List<string>(), (list, grouping) =>
            {
               list.Add($"{grouping.Count()}");
               list.AddRange(grouping.Select(spell => $"{spell.VN.Replace(' ', ' ')} {spell.stageId} {spell.id}"));
               return list;
            })
            ;
         File.WriteAllText(Path.Combine(outputDirPath, $"7.txt"), string.Join("\r\n", challengeLines), Encoding.UTF8);
         Console.WriteLine($" {Path.Combine(outputDirPath, $"7.txt")} generated.");
      }
      using (var reader = new StreamReader(new FileStream(a0InputFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
      using (var csv = new CsvReader(reader))
      {
         csv.Configuration.HasHeaderRecord = true;
         var records = csv.GetRecords<A0>();
         var a0Lines = records
            .Select((record, i) =>
            {
               if (record.Options.Trim() != "NoTrim") record.VN = record.VN.Trim();
               if (record.Options.Trim() == "Imported") record.VN = exportedMap[i + 1];
               return record.VN;
            })
            .ToArray()
            ;
         File.WriteAllText(Path.Combine(outputDirPath, $"a0.txt"), string.Join("\r\n", a0Lines), Encoding.UTF8);
         Console.WriteLine($" {Path.Combine(outputDirPath, $"a0.txt")} generated.");
      }
   }
}


//css_nuget -noref CsvHelper
//css_dir %css_nuget%\CsvHelper\**\net45
//css_ref CsvHelper.dll
using System.Collections;
using System.IO;
using System;
using System.Reflection;
public class Script
{
   public static void Main(string[] args)
   {
      if (typeof(bool).Assembly.GetType("System.ValueTuple") == null)
      {
         Console.WriteLine("Hello World");
      }
   }
}
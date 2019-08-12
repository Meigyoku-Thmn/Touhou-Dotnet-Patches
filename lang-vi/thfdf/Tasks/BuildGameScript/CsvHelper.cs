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
         AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(MyResolveEventHandler);
      }
   }
   private static Assembly MyResolveEventHandler(object sender, ResolveEventArgs args)
   {
      if (args.Name.StartsWith("System.ValueTuple")) {
         var _CSSUtils = Assembly.GetEntryAssembly().GetType("csscript.CSSUtils");
         var _GetDirectories = _CSSUtils.GetMethod("GetDirectories");
         var targetDirPath = ((string[])_GetDirectories.Invoke(null, 
            new object[] { 
               Directory.GetCurrentDirectory(), 
               Environment.ExpandEnvironmentVariables(@"%css_nuget%\CsvHelper\System.ValueTuple*\**\net461") 
            }
         ))[0];
         return Assembly.LoadFile(targetDirPath + @"\System.ValueTuple.dll");
      }
      return null;
   }
}
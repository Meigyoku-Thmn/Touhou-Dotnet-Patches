using System.Collections;
using System.IO;
public class Precompiler
{
   public static bool Compile(ref string code, string scriptFile, bool isPrimaryScript, Hashtable context)
   {
      if (typeof(bool).Assembly.GetType("System.ValueTuple") == null)
      {
         code = code.Replace(
            "<System.ValueTuple>",
            @"css_reference %CSSCRIPT_DIR%\lib\Bin\Roslyn\System.ValueTuple.dll"
         );
      }
      else
      {
         code = code.Replace("<System.ValueTuple>", "");
      }
      return true;
   }
}
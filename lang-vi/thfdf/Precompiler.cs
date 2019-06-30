using System.Collections;
using System.IO;

public class Precompiler {
   public static bool Compile(ref string code, string scriptFile, bool isPrimaryScript, Hashtable context) {
      object TargetInfo = context["TargetInfo"];
      if (TargetInfo is IDictionary && TargetInfo != null) {
         object TargetVersion = (TargetInfo as IDictionary)["Version"];
         if (TargetVersion is string && TargetVersion != null) {
            code = "#define " + TargetVersion.ToString() + "\r\n" + code;
         }
      }
      if (Path.GetFileName(scriptFile) == "Resource.cs") {
         code = code.Replace("<CURRENT_FILE>", scriptFile);
      }
      return true;
   }
}
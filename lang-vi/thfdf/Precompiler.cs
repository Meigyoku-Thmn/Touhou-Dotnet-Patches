using System.Collections;

public class Precompiler {
   public static bool Compile(ref string code, string scriptFile, bool isPrimaryScript, Hashtable context) {
      code = code.Replace("<CURRENT_FILE>", scriptFile);
      return true;
   }
}
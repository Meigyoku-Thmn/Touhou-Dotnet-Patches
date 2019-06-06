using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

//css_precompiler Precompiler.cs
class Resource {
   public static string CurrentDirPath = Path.GetDirectoryName(@"<CURRENT_FILE>");
   public static string DebugResourceWorkingPath = Path.Combine(CurrentDirPath, "DebugResource");
   public static string ResourceWorkingPath = CurrentDirPath;
   static Regex ignoredFiles = new Regex(
         @"^(:?(?:4\.xna)|(?:5\.xna)|(?:8\.xna)|(?:.*?\.dat))$", RegexOptions.Compiled);
   public static string MapResourcePath(string path)
   {
      path = path.Replace('/', '\\');
      var fileName = Path.GetFileNameWithoutExtension(path);
      var fileExt = Path.GetExtension(path).ToLower();
      var newExt = default(string);
      var graphicsRootPath = @"Content\Graphics";
      var dataRootPath = @"Content\Data";
      if (path.IndexOf(dataRootPath, 0, dataRootPath.Length) == 0 && !ignoredFiles.IsMatch(fileName + fileExt))
         newExt = ".txt";
      else if (fileExt == ".dat" || path.IndexOf(graphicsRootPath, 0, graphicsRootPath.Length) == 0)
         newExt = ".png";
      if (newExt != null) path = Path.Combine(Path.GetDirectoryName(path), fileName + newExt);
      return path;
   }
}
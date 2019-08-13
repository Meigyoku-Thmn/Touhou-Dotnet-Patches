//css_args -provider:%CSSCRIPT_DIR%\lib\CSSRoslynProvider.dll
//css_dir %CSSCRIPT_DIR%\lib\Bin\Roslyn
//css_import Extractor\Decipher.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

class Program : IWin32Window
{
   public IntPtr Handle
   {
      get { return GetConsoleWindow(); }
   }
   [DllImport("kernel32.dll")]
   static extern IntPtr GetConsoleWindow();
   [STAThread]
   static void Main(string[] args)
   {
      var consoleWindow = new Program();
      var errorStrings = new List<string>();
      string lastRootPath = null;
      string lastOutputPath = null;
      try { lastRootPath = File.ReadAllText("rootPath.txt"); } catch (Exception) { }
      try { lastOutputPath = File.ReadAllText("outputPath.txt"); } catch (Exception) { }
      var folderDialog = new OpenFileDialog();
      if (lastRootPath != null) folderDialog.InitialDirectory = lastRootPath;
      folderDialog.Title = "Select game's directory";
      folderDialog.FileName = "Access to a directory then type any name at here";
      folderDialog.CheckFileExists = false;
      var dialogRs = folderDialog.ShowDialog(consoleWindow);
      if (dialogRs == DialogResult.Cancel) return;
      var rootPath = Path.GetDirectoryName(folderDialog.FileName) + '\\';
      File.WriteAllText("rootPath.txt", rootPath);
      if (lastOutputPath != null) folderDialog.InitialDirectory = lastOutputPath;
      folderDialog.Title = "Select base output directory";
      folderDialog.FileName = "Access to a directory then type any name at here";
      folderDialog.CheckFileExists = false;
      dialogRs = folderDialog.ShowDialog(consoleWindow);
      if (dialogRs == DialogResult.Cancel) return;
      var outputPath = Path.GetDirectoryName(folderDialog.FileName);
      File.WriteAllText("outputPath.txt", outputPath);
      Console.WriteLine("Enter output folder name:");
      string outputName = "";
      while (outputName.Length == 0)
      {
         outputName = Console.ReadLine();
         outputName = outputName.Trim();
      }
      var graphicsPath = @"Content\Graphics";
      var dataPath = @"Content\Data";
      var dataFilePatterns = new[] {
        ( @"^0\.xna$", 2, "Text" ),
        ( @"^1\.xna$", 2, "Text" ),
        ( @"^2\.xna$", 2, "Text" ),
        ( @"^3\.xna$", 2, "Text" ),
        ( @"^6\.xna$", 2, "Text" ),
        ( @"^7\.xna$", 2, "Text" ),
        ( @"^9\.xna$", 2, "Text" ),
        ( @"^10\.xna$", 2, "Text" ),
        ( @"^a0\.xna$", 2, "Text" ),
        ( @"^.+?\.dat$", 0, "Texture2D" ),
        ( @"^b.+?\.xna$", 0, "Text" ),
        ( @"^bsc.+?\.xna$", 0, "Text" ),
        ( @"^e.+?\.xna$", 0, "Text" ),
        ( @"^bg.+?\.xna$", 0, "Text" ),
        ( @"^d.+?\.xna$", 2, "Text" ),
        ( @"^sc.+?\.xna$", 2, "Text" ),
        ( @"^s.+?\.xna$", 2, "Text" ),
        ( @"^e.+?\.xna$", 0, "Text" ),
     };
      var graphicsFileNames = Directory.GetFiles(Path.Combine(rootPath, graphicsPath), "*", SearchOption.AllDirectories);
      var formats = new SortedSet<string>();
      foreach (var fileName in graphicsFileNames)
      {
         try
         {
            Image.FromStream(Decipher.Decry(fileName, 0), true, true);
            var texture = Image.FromStream(Decipher.Decry(fileName, 0), true, true);
            var newFileName = fileName.Replace(rootPath, "");
            Console.WriteLine(newFileName);
            newFileName = Path.Combine(outputPath, outputName, newFileName);
            var newDirName = Path.GetDirectoryName(newFileName);
            Directory.CreateDirectory(newDirName);
            newFileName = newFileName + ".png";
            texture.Save(newFileName, ImageFormat.Png);
         }
         catch (Exception e)
         {
            errorStrings.Add("When processing file: " + fileName.Replace(rootPath, ""));
            errorStrings.Add(e.ToString());
         }
      }
      var dataFileNames = Directory.GetFiles(Path.Combine(rootPath, dataPath), "*", SearchOption.AllDirectories);
      foreach (var filePath in dataFileNames)
      {
         try
         {
            var fileName = Path.GetFileName(filePath);
            var tt = dataFilePatterns.FirstOrDefault(e => new Regex(e.Item1).IsMatch(fileName));
            if (tt.Item1 == null)
            {
               var stream = Decipher.Decry(filePath, 2);
               byte[] buffer = new byte[stream.Length];
               stream.Read(buffer, 0, (int)stream.Length);
               var newFilePath = filePath.Replace(rootPath, "");
               Console.WriteLine(newFilePath);
               newFilePath = Path.Combine(outputPath, outputName, newFilePath);
               var newDirName = Path.GetDirectoryName(newFilePath);
               Directory.CreateDirectory(newDirName);
               newFilePath = newFilePath + ".txt";
               File.WriteAllBytes(newFilePath, buffer);
            }
            else
            {
               var stream = Decipher.Decry(filePath, tt.Item2);
               if (tt.Item3 == "Text")
               {
                  byte[] buffer = new byte[stream.Length];
                  stream.Read(buffer, 0, (int)stream.Length);
                  var newFilePath = filePath.Replace(rootPath, "");
                  Console.WriteLine(newFilePath);
                  newFilePath = Path.Combine(outputPath, outputName, newFilePath);
                  var newDirName = Path.GetDirectoryName(newFilePath);
                  Directory.CreateDirectory(newDirName);
                  newFilePath = newFilePath + ".txt";
                  File.WriteAllBytes(newFilePath, buffer);
               }
               else if (tt.Item3 == "Texture2D")
               {
                  var texture = Image.FromStream(stream);
                  var newFilePath = filePath.Replace(rootPath, "");
                  Console.WriteLine(newFilePath);
                  newFilePath = Path.Combine(outputPath, outputName, newFilePath);
                  var newDirName = Path.GetDirectoryName(newFilePath);
                  Directory.CreateDirectory(newDirName);
                  newFilePath = newFilePath + ".png";
                  texture.Save(newFilePath, ImageFormat.Png);
               }
            }
         }
         catch (Exception e)
         {
            errorStrings.Add("When processing file: " + filePath.Replace(rootPath, ""));
            errorStrings.Add(e.ToString());
         }
      }
      Console.WriteLine("Number of errors: " + errorStrings.Count);
      File.WriteAllLines("Error.txt", errorStrings.ToArray());
      Console.WriteLine("Press enter to exit.");
      Console.ReadLine();
   }
}

//css_args -provider:%CSSCRIPT_DIR%\lib\CSSRoslynProvider.dll
//css_import BuildGameScript\CsvHelper.cs
//css_prescript BuildGameScript\CsvHelper()
//css_import BuildGameScript\DialogConv.cs
//css_import BuildGameScript\MusicConv.cs
//css_import BuildGameScript\AchiveConv.cs
//css_import BuildGameScript\SpellA0Conv.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Text;

public class GameScriptBuilder
{
   static void Main(string[] args)
   {
      Console.OutputEncoding = Encoding.Unicode;
      DialogConv.Run(@"../GameScript/Dialogue.csv", @"../Content/Data");
      MusicConv.Run(@"../GameScript/MusicRoom.csv", @"../Content/Data/6.txt");
      AchiveConv.Run(@"../GameScript/Achivement.csv", @"../Content/Data/9.txt");
      SpellA0Conv.Run(@"../GameScript/SpellName.csv", @"../GameScript/a0.csv", @"../Content/Data");
   }
}

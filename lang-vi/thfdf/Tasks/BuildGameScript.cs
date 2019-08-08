//css_precompiler Sub\Preprocessor.cs
//css_args -provider:%CSSCRIPT_DIR%\lib\CSSRoslynProvider.dll
//<System.ValueTuple>
//css_import Sub\DialogConv.cs
//css_import Sub\MusicConv.cs
//css_import Sub\AchiveConv.cs
//css_import Sub\SpellA0Conv.cs
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
      // SpellA0Conv.Run(@"../GameScript/MusicRoom.csv", @"../Content/Data/6.txt");
   }
}

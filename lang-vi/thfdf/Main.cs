//css_reference Microsoft.CSharp.dll
//css_precompiler Precompiler.cs
//css_import Types.cs
//css_import Detours.cs
//css_import Transpilers.cs
//css_import Resource.cs
//css_import Custom.cs
//css_import Custom.Designer.cs
//css_import Helper.cs
//css_import Reentrants.cs
//css_ref System.ValueTuple.dll
//css_ignore_ns Microsoft.Xna.Framework
//css_ignore_ns Microsoft.Xna.Framework.Graphics
//css_ignore_ns Microsoft.Xna.Framework.Storage
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DotnetPatching {
   class Config {
      [DllImport("steam_api.dll", SetLastError = true)]
      static extern bool SteamAPI_Init();
      public static bool OnInit() {
         var customForm = new Custom();
         var dialogRs = customForm.ShowDialog();
         if (dialogRs != DialogResult.OK) return false;
         try {
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "steam_appid.txt"), "882710");
         }
         catch (Exception e) {
            Console.WriteLine("Failed to create steam_appid.txt:");
            Console.WriteLine(" " + e.ToString());
         }
         try {
            if (SteamAPI_Init() == false) throw new Exception("SteamAPI_Init() return false.");               
         }
         catch (Exception e) {
            Console.WriteLine("Failed to run SteamAPI_Init():");
            Console.WriteLine(" " + e.ToString());
         }
         return true;
      }
   }
}
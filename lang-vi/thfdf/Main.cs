//css_reference Microsoft.CSharp.dll
//css_precompiler Precompiler.cs
//css_import Detours.cs
//css_import Transpilers.cs
//css_import Types.cs
//css_import Resource.cs
//css_import Custom.cs
//css_import Custom.Designer.cs
//css_import Helper.cs
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DotnetPatching {
   class Config {
      [DllImport("steam_api.dll", SetLastError = true)]
      static extern bool SteamAPI_Init();
      public static bool OnInit() {
         Assembly.GetExecutingAssembly().GetReferencedAssemblies();
         var customForm = new Custom();
         var dialogRs = customForm.ShowDialog();
         if (dialogRs != DialogResult.OK) return false;
         try { SteamAPI_Init(); }
         catch (Exception e) { Console.WriteLine(e.ToString()); }
         return true;
      }
   }
}
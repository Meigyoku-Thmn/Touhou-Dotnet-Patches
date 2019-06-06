//css_reference Microsoft.CSharp.dll
//css_import Detours.cs
//css_import Transpilers.cs
//css_import Types.cs
//css_import Resource.cs
using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
namespace DotnetPatching {
   class Config {
      [DllImport("steam_api.dll", SetLastError = true)]
      static extern bool SteamAPI_Init();
      public static void OnInit() {
         try { SteamAPI_Init(); }
         catch (Exception e) { Console.WriteLine(e.ToString()); }
      }
   }
}
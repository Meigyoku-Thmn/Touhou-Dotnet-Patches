//css_import Detours.cs
//css_import Transpilers.cs
//css_import Types.cs
//css_import Resource.cs
using System;
using System.Runtime.InteropServices;
namespace DotnetPatching {
   class Config {
      [DllImport("steam_api.dll", SetLastError = true)]
      public static extern bool SteamAPI_Init();
      static void OnInit() {
         try { SteamAPI_Init(); }
         catch (Exception e) { Console.WriteLine(e.ToString()); }
      }
   }
}
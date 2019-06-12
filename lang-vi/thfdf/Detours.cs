using System;
using static Types;
using static Resource;
using static Launcher.Helper;
using static Launcher.Launcher;
using static Assemblies;
using System.IO;
using Harmony;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Text;
using System.Collections.Generic;
using System.Diagnostics;

namespace DotnetPatching {
   using PatchTuple = ValueTuple<Type, string, Type[], Type, string, Type[]>;
   class Detours {
      public static List<PatchTuple> OnSetup() {
         return new List<PatchTuple>() {
            // For replace game resource on the fly
            (CryT, "Decry", TypeL(StringT, IntT), typeof(Detours), nameof(Decry), null),
            (CryT, "Decry", TypeL(StringT), typeof(Detours), nameof(Decry0), null),
            // To change game title
            (WindowsGameWindowT, "SetTitle", null, typeof(Detours), nameof(SetTitle), null),
            // To Set Game Icon, since we use launcher method to hook code
            (WindowsGameWindowT, "GetDefaultIcon", null, typeof(Detours), nameof(GetDefaultIcon), null),
            // To set the game working directory for XNA
            (TitleLocationT, "get_Path", null, typeof(Detours), nameof(TitleLocationPath), null),
            // To set the game's fonts
            (SpriteFontXT, "Initialize", TypeL(FontT, IGraphicsDeviceServiceT, TextRenderingHintT), typeof(Detours), nameof(SpriteFontXInitialize), null),
         };
      }
      public static void Decry(ref string FileName, ref int type, ref Stream __result, HMState __state) {
         dynamic ReentrantDecry = __state.ReentrantDelegate;
         var modeFilePath = MapResourcePath(FileName);
         var newFileName = Path.Combine(DebugResourceWorkingPath, modeFilePath);
         var message = "Load debug mod: ";
         var isNotSameFolder = ResourceWorkingPath != DebugResourceWorkingPath;
         if (isNotSameFolder && !File.Exists(newFileName)) {
            message = "Load mod: ";
            newFileName = Path.Combine(ResourceWorkingPath, modeFilePath);
         }
         if (isNotSameFolder == false) message = "Load mod: ";
         if (!File.Exists(newFileName))
            __result = ReentrantDecry((dynamic)FileName, (dynamic)type);
         else {
            Console.WriteLine(message + FileName);
            __result = new MemoryStream(File.ReadAllBytes(newFileName));
         }
      }
      public static void Decry0(ref string FileName, ref Stream __result, HMState __state) {
         dynamic ReentrantDecry0 = __state.ReentrantDelegate;
         var modeFilePath = MapResourcePath(FileName);
         var newFileName = Path.Combine(DebugResourceWorkingPath, modeFilePath);
         var message = "Load debug mod: ";
         var isNotSameFolder = ResourceWorkingPath != DebugResourceWorkingPath;
         if (isNotSameFolder && !File.Exists(newFileName)) {
            message = "Load mod: ";
            newFileName = Path.Combine(ResourceWorkingPath, modeFilePath);
         }
         if (isNotSameFolder == false) message = "Load mod: ";
         if (!File.Exists(newFileName))
            __result = ReentrantDecry0((dynamic)FileName);
         else {
            Console.WriteLine(message + FileName);
            __result = new MemoryStream(File.ReadAllBytes(newFileName));
         }
      }
      [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
      public static extern bool SetWindowText(IntPtr hWnd, string lpString);
      static bool IsTextSet = false;
      public static void SetTitle(Form ___mainForm) {
         if (IsTextSet == true) return;
         var version = Traverse.Create(TargetAssembly.GetType("THMHJ.Main")).Field("version").GetValue();
         SetWindowText(___mainForm.Handle, $"Đông Phương Mạc Hoa Tế ～ Touhou Fantastic Danmaku Festival | Version {version} | Bản Việt hóa Version 1.0");
         IsTextSet = true;
      }
      public static void GetDefaultIcon(ref Icon __result) {
         __result = TargetIcon;
      }
      public static void TitleLocationPath(ref string __result) {
         __result = Path.GetDirectoryName(TargetAssembly.Location);
      }
      public static void SpriteFontXInitialize(object __instance, Font font, object gds, TextRenderingHint trh, HMState __state) {
         dynamic SFXInitialize = __state.ReentrantDelegate;
         if (font.OriginalFontName == "Cambria")
            font = new Font("Arial", font.Size, font.Style, font.Unit);
         SFXInitialize((dynamic)__instance, (dynamic)font, (dynamic)gds, (dynamic)trh);
      }
   }
}


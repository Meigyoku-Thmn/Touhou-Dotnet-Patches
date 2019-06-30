using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Delegates;
using static Resource;
using static RuntimePatcher.Helper;
using static RuntimePatcher.Launcher;
using static Types;

//css_import Detours.1.04sc.cs
namespace DotnetPatching {
   using PatchTuple = ValueTuple<Type, string, Type[], Type, string, Type[]>;
   class Detours {
      public static List<PatchTuple> OnSetup() {
         return new List<PatchTuple>() {
            // For replace game resource on the fly
            (CryT, "Decry", TypeL(StringT, IntT), typeof(Detours), nameof(Decry), null),
#if   _1_05_en
            // For replace game resource on the fly
            (CryT, "Decry", TypeL(StringT), typeof(Detours), nameof(Decry0), null),
            // To set the game working directory for XNA
            (TitleLocationT, "get_Path", null, typeof(Detours), nameof(TitleLocationPath), null),
#elif _1_04_sc
            // To set the game working directory for XNA
            (StorageContainerT, "get_TitleLocation", null, typeof(Detours), nameof(TitleLocationPath), null),
#endif
            // To change game title
            (WindowsGameWindowT, "SetTitle", null, typeof(Detours), nameof(SetTitle), null),
            // To Set Game Icon, since we use launcher method to hook code
            (WindowsGameWindowT, "GetDefaultIcon", null, typeof(Detours), nameof(GetDefaultIcon), null),
            // To set the game's fonts
            (SpriteFontXT, "Initialize", null, typeof(Detours), nameof(SpriteFontXInitialize), null),
            // collect character textures
            (SpriteFontXT, "newTex", null, typeof(Detours), nameof(newTex), null),
         }
#if _1_04_sc
         .Concat(Detours_1_04_sc.OnSetup())
         .ToList()
#endif
         ;
      }
      public static void newTex(object __instance, ref object ___CurrentTex2d, HMState __state) {
         __state.ReentrantDelegate(null, __instance);
         charTextures.Add(___CurrentTex2d);
      }
      public static void Decry(string FileName, int type, ref Stream __result, HMState __state) {
         var newFileName = ResolveModPath(FileName);
         if (newFileName == null)
            __result = (Stream)__state.ReentrantDelegate(null, FileName, type);
         else {
            __result = new MemoryStream(File.ReadAllBytes(newFileName));
         }
      }
#if   _1_05_en
      public static void Decry0(string FileName, ref Stream __result, HMState __state) {
         var newFileName = ResolveModPath(FileName);
         if (newFileName == null)
            __result = (Stream)__state.ReentrantDelegate(null, FileName);
         else {
            __result = new MemoryStream(File.ReadAllBytes(newFileName));
         }
      }
#endif
      [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
      public static extern bool SetWindowText(IntPtr hWnd, string lpString);
      static bool IsTextSet = false;
      public static void SetTitle(Form ___mainForm) {
         if (IsTextSet == true) return;
         var version = R_Version();
         SetWindowText(___mainForm.Handle, $"Đông Phương Mạc Hoa Tế ～ Touhou Fantastic Danmaku Festival. ver {version} | Bản Việt hóa Version 1.0");
         IsTextSet = true;
      }
      public static void GetDefaultIcon(ref Icon __result) {
         __result = TargetIcon;
      }
      public static void TitleLocationPath(ref string __result) {
         __result = Path.GetDirectoryName(TargetAssembly.Location);
      }
      static List<ValueTuple<Font, TextRenderingHint>> fontList;
      static Detours() {
         var FontCfg = Resource.Config.AlterTextCfg.AlterFontCfg;
         fontList = new List<(Font, TextRenderingHint)>() {
            // FPS
            (FontCfg.FPSFont.Enabled ? FontCfg.FPSFont.FontI : null, TextRenderingHint.ClearTypeGridFit),
            // almost anything else in game
            (FontCfg.RestFont.Enabled ? FontCfg.RestFont.FontI : null, TextRenderingHint.AntiAlias),
            // spellcard name (when used)
            (FontCfg.SpellNameFont.Enabled ? FontCfg.SpellNameFont.FontI : null, TextRenderingHint.AntiAlias),
            // spellcard statistics (when spellcard is used)
            (FontCfg.SpellStatFont.Enabled ? FontCfg.SpellStatFont.FontI: null, TextRenderingHint.SingleBitPerPixelGridFit),
         };
      }
      static int fontIndex = 0;
      public static void SpriteFontXInitialize(object __instance, Font font, object gds, TextRenderingHint trh, HMState __state) {
         var _font = fontList[fontIndex].Item1;
         if (_font != null) {
            font = _font;
            trh = fontList[fontIndex].Item2;
         }
         SetSpriteFont(fontIndex, __instance);
         fontIndex++;
         __state.ReentrantDelegate(null, __instance, font, gds, trh);
      }
   }
}


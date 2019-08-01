using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RuntimePatcher;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Delegates;
using static Methods;
using static Reentrants;
using static Resource;
using static RuntimePatcher.Helper;
using static RuntimePatcher.Launcher;

//css_import Detours.1.04sc.cs
namespace DotnetPatching {
   using System.Diagnostics;
   using static PatchInfo;
   using PatchTuple = PatchInfo;
#if _1_05_en
   using XnaColor = Microsoft.Xna.Framework.Color;
#elif _1_04_sc
   using XnaColor = Microsoft.Xna.Framework.Graphics.Color;
#endif
   using XnaRectangle = Microsoft.Xna.Framework.Rectangle;
   partial class Detours {
      public static List<PatchTuple> OnSetup() {
         return new PatchTuple[] {
            // For replace game resource on the fly
            PM(DecryM, DecryH, DecryR),
#if _1_05_en
            // For replace game resource on the fly
            PM(Decry0M, Decry0H, Decry0R),
            // To set the game working directory for XNA
            PM(get_PathM, TitleLocationPathH),
#elif _1_04_sc
            // To set the game working directory for XNA
            PM(get_TitleLocationM, TitleLocationPathH),
#endif
            // To change game title
            PM(SetTitleM, SetTitleH),
            // To Set Game Icon, since we use launcher method to hook code
            PM(GetDefaultIconM, GetDefaultIconH),
            // To set the game's fonts
            PM(SPXInitializeM, SpriteFontXInitializeH, SpriteFontXInitializeR),
            // collect character textures
            PM(SPXnewTexM, newTexH, newTexR),
         }.Concat(!(Resource.Config.AlterTextCfg.Enabled && Resource.Config.AlterTextCfg.UseBlackBorder) ? new PatchTuple[0] : new PatchTuple[] {
         })
#if _1_04_sc
         .Concat(OnSetup_1_04_sc())
         .ToList()
#endif
         ;
      }
      public static void newTex(SpriteFontX __instance, ref Texture2D ___CurrentTex2d) {
         _newTex(__instance);
         charTextures.Add(___CurrentTex2d, __instance);
      }
      public static void Decry(string FileName, int type, ref Stream __result) {
         var newFileName = ResolveModPath(FileName);
         if (newFileName == null)
            __result = _Decry(FileName, type);
         else {
            __result = new MemoryStream(File.ReadAllBytes(newFileName));
         }
      }
      public static void Decry0(string FileName, ref Stream __result) {
         var newFileName = ResolveModPath(FileName);
         if (newFileName == null)
            __result = _Decry0(FileName);
         else {
            __result = new MemoryStream(File.ReadAllBytes(newFileName));
         }
      }
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
      static bool IfUseFontMod(bool last) {
         var config = Resource.Config;
         var alterTextCfg = config.AlterTextCfg;
         var alterFontCfg = alterTextCfg.AlterFontCfg;
         return alterTextCfg.Enabled && alterFontCfg.Enabled && last;
      }
      static Detours() {
         var FontCfg = Resource.Config.AlterTextCfg.AlterFontCfg;
         fontList = new List<(Font, TextRenderingHint)>() {
            // FPS
            (IfUseFontMod(FontCfg.FPSFont.Enabled) ? FontCfg.FPSFont.FontI : null, TextRenderingHint.ClearTypeGridFit),
            // almost anything else in game
            (IfUseFontMod(FontCfg.RestFont.Enabled) ? FontCfg.RestFont.FontI : null, TextRenderingHint.AntiAlias),
            // spellcard name (when used)
            (IfUseFontMod(FontCfg.SpellNameFont.Enabled) ? FontCfg.SpellNameFont.FontI : null, TextRenderingHint.AntiAlias),
            // spellcard statistics (when spellcard is used)
            (IfUseFontMod(FontCfg.SpellStatFont.Enabled) ? FontCfg.SpellStatFont.FontI: null, TextRenderingHint.SingleBitPerPixelGridFit),
         };
      }
      static int fontIndex = 0;
      public static void SpriteFontXInitialize(SpriteFontX __instance, Font font, IGraphicsDeviceService gds, TextRenderingHint trh) {
         var _font = fontList[fontIndex].Item1;
         if (_font != null) {
            font = _font;
            trh = fontList[fontIndex].Item2;
         }
         SetSpriteFont(fontIndex, __instance);
         fontIndex++;
         _SpriteFontXInitialize(__instance, font, gds, trh);
      }
   }
}


using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace DotnetPatching {
   public enum FontWeight : int {
      FW_DONTCARE = 0,
      FW_THIN = 100,
      FW_EXTRALIGHT = 200,
      FW_LIGHT = 300,
      FW_NORMAL = 400,
      FW_MEDIUM = 500,
      FW_SEMIBOLD = 600,
      FW_BOLD = 700,
      FW_EXTRABOLD = 800,
      FW_HEAVY = 900,
   }
   [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
   public struct LOGFONT {
      public int lfHeight;
      public int lfWidth;
      public int lfEscapement;
      public int lfOrientation;
      public int lfWeight;
      public byte lfItalic;
      public byte lfUnderline;
      public byte lfStrikeOut;
      public byte lfCharSet;
      public byte lfOutPrecision;
      public byte lfClipPrecision;
      public byte lfQuality;
      public byte lfPitchAndFamily;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst=32)]
      public string lfFaceName;
   }
   [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
   public struct CHOOSEFONT {
      public int lStructSize;
      public IntPtr hwndOwner;
      public IntPtr hDC;
      public IntPtr lpLogFont;
      public int iPointSize;
      public int Flags;
      public int rgbColors;
      public IntPtr lCustData;
      public IntPtr lpfnHook;
      public string lpTemplateName;
      public IntPtr hInstance;
      public string lpszStyle;
      public short nFontType;
      public short __MISSING_ALIGNMENT__;
      public int nSizeMin;
      public int nSizeMax;
   }
   [Flags]
   public enum CHOOSEFONTFLAGS : int {
      CF_SCREENFONTS = 0x00000001,
      CF_PRINTERFONTS = 0x00000002,
      CF_BOTH = (CF_SCREENFONTS | CF_PRINTERFONTS),
      CF_SHOWHELP = 0x00000004,
      CF_ENABLEHOOK = 0x00000008,
      CF_ENABLETEMPLATE = 0x00000010,
      CF_ENABLETEMPLATEHANDLE = 0x00000020,
      CF_INITTOLOGFONTSTRUCT = 0x00000040,
      CF_USESTYLE = 0x00000080,
      CF_EFFECTS = 0x00000100,
      CF_APPLY = 0x00000200,
      CF_ANSIONLY = 0x00000400,
      CF_SCRIPTSONLY = CF_ANSIONLY,
      CF_NOVECTORFONTS = 0x00000800,
      CF_NOOEMFONTS = CF_NOVECTORFONTS,
      CF_NOSIMULATIONS = 0x00001000,
      CF_LIMITSIZE = 0x00002000,
      CF_FIXEDPITCHONLY = 0x00004000,
      CF_WYSIWYG = 0x00008000,
      CF_FORCEFONTEXIST = 0x00010000,
      CF_SCALABLEONLY = 0x00020000,
      CF_TTONLY = 0x00040000,
      CF_NOFACESEL = 0x00080000,
      CF_NOSTYLESEL = 0x00100000,
      CF_NOSIZESEL = 0x00200000,
      CF_SELECTSCRIPT = 0x00400000,
      CF_NOSCRIPTSEL = 0x00800000,
      CF_NOVERTFONTS = 0x01000000,
      CF_INACTIVEFONTS = 0x02000000
   }
   static class Helper {
      [DllImport("comdlg32.dll", CharSet = CharSet.Unicode, EntryPoint = "ChooseFont", SetLastError = true)]
      public extern static bool ChooseFont(IntPtr lpcf);
      public static int MulDiv(int number, int numerator, int denominator) {
         return (int)(((long)number * numerator + (denominator >> 1)) / denominator);
      }
      const int LOGPIXELSY = 90;
      const byte TRUE = 1;
      const byte FALSE = 0;
      [DllImport("user32.dll", EntryPoint = "GetDC")]
      public static extern IntPtr GetDC(IntPtr ptr);
      [DllImport("gdi32.dll")]
      static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
      static public Font OpenFontDialog(Font initialFont) {
         LOGFONT logfont = new LOGFONT();
         logfont.lfFaceName = initialFont.Name;
         logfont.lfHeight = -MulDiv((int)Math.Round(initialFont.Size), GetDeviceCaps(GetDC(IntPtr.Zero), LOGPIXELSY), 72);
         logfont.lfItalic = (initialFont.Style & FontStyle.Italic) != 0 ? TRUE : FALSE;
         logfont.lfStrikeOut = (initialFont.Style & FontStyle.Strikeout) != 0 ? TRUE : FALSE;
         logfont.lfUnderline = (initialFont.Style & FontStyle.Underline) != 0 ? TRUE : FALSE;
         logfont.lfWeight = (initialFont.Style & FontStyle.Regular) != 0 ? 400 : 700;
         IntPtr pLogfont = Marshal.AllocHGlobal(Marshal.SizeOf(logfont));
         Marshal.StructureToPtr(logfont, pLogfont, false);
         CHOOSEFONT choosefont = new CHOOSEFONT();
         choosefont.lStructSize = Marshal.SizeOf(choosefont);
         choosefont.Flags = (int)(CHOOSEFONTFLAGS.CF_EFFECTS | CHOOSEFONTFLAGS.CF_INITTOLOGFONTSTRUCT | CHOOSEFONTFLAGS.CF_TTONLY | CHOOSEFONTFLAGS.CF_NOVERTFONTS);
         choosefont.lpLogFont = pLogfont;
         IntPtr pChoosefont = Marshal.AllocHGlobal(Marshal.SizeOf(choosefont));
         Marshal.StructureToPtr(choosefont, pChoosefont, false);
         bool result = ChooseFont(pChoosefont);
         if (result == false) return null;
         choosefont = Marshal.PtrToStructure<CHOOSEFONT>(pChoosefont);
         logfont = Marshal.PtrToStructure<LOGFONT>(pLogfont);
         Marshal.FreeHGlobal(pChoosefont);
         Marshal.FreeHGlobal(pLogfont);
         var style = FontStyle.Regular;
         // sometime lfItalic equals to 255
         if (logfont.lfItalic != 0) style |= FontStyle.Italic;
         if (logfont.lfStrikeOut != 0) style |= FontStyle.Strikeout;
         if (logfont.lfUnderline != 0) style |= FontStyle.Underline;
         style |= logfont.lfWeight > (int)FontWeight.FW_NORMAL ? FontStyle.Bold : FontStyle.Regular;
         return new Font(logfont.lfFaceName, choosefont.iPointSize / 10f, style, GraphicsUnit.Pixel);
      }
   }
}

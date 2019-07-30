using HarmonyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
#if _1_05_en
using XnaColor = Microsoft.Xna.Framework.Color;
#elif _1_04_sc
using XnaColor = Microsoft.Xna.Framework.Graphics.Color;
#endif
using XnaRectangle = Microsoft.Xna.Framework.Rectangle;
class Reentrants {
   static public MethodInfo newTexR = AccessTools.Method(typeof(Reentrants), "_newTex");
   public static void _newTex(SpriteFontX __instance) { }

   static public MethodInfo DecryR = AccessTools.Method(typeof(Reentrants), "_Decry");
   public static Stream _Decry(string FileName, int type) { return null; }

   static public MethodInfo Decry0R = AccessTools.Method(typeof(Reentrants), "_Decry0");
   public static Stream _Decry0(string FileName) { return null; }

   static public MethodInfo SpriteFontXInitializeR = AccessTools.Method(typeof(Reentrants), "_SpriteFontXInitialize");
   public static void _SpriteFontXInitialize(SpriteFontX __instance, Font font, IGraphicsDeviceService gds, TextRenderingHint trh) { }

   static public MethodInfo SBDraw2R = AccessTools.Method(typeof(Reentrants), "_SBDraw2");
   public static void _SBDraw2(SpriteBatch __instance, Texture2D texture, Vector2 position, XnaRectangle? sourceRectangle, XnaColor color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth) { }
}

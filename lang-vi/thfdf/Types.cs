using System;
using System.Drawing;
using System.Drawing.Text;
using System.Reflection;
using static Assemblies;
using static Launcher.Launcher;

class Assemblies {
   public static Assembly XnaAssembly = ReferenceAssemblies["Microsoft.Xna.Framework"];
   public static Assembly XnaGameAssembly = ReferenceAssemblies["Microsoft.Xna.Framework.Game"];
   public static Assembly XnaGraphicsAssembly = ReferenceAssemblies["Microsoft.Xna.Framework.Graphics"];
}
class Types
{
   public static Type WindowsGameWindowT = XnaGameAssembly.GetType("Microsoft.Xna.Framework.WindowsGameWindow");
   public static Type TitleLocationT = XnaAssembly.GetType("Microsoft.Xna.Framework.TitleLocation");
   public static Type SpriteFontXT = TargetAssembly.GetType("Microsoft.Xna.Framework.SpriteFontX");
   public static Type IntT = typeof(int);
   public static Type StringT = typeof(string);
   public static Type MainT = TargetAssembly.GetType("THMHJ.Main");
   public static Type CryT = TargetAssembly.GetType("THMHJ.Cry");
   public static Type FontT = typeof(Font);
   public static Type IGraphicsDeviceServiceT = XnaGraphicsAssembly.GetType("Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService");
   public static Type TextRenderingHintT = typeof(TextRenderingHint);
   public static Type EDT = TargetAssembly.GetType("THMHJ.ED");
   public static Type TitleT = TargetAssembly.GetType("THMHJ.Title");
   public static Type EntranceT = TargetAssembly.GetType("THMHJ.Entrance");
   public static Type BonusT = TargetAssembly.GetType("THMHJ.Bonus");
   public static Type StageclearT = TargetAssembly.GetType("THMHJ.Stageclear");
   public static Type GameT = TargetAssembly.GetType("THMHJ.Game");
   public static Type SPECIALT = TargetAssembly.GetType("THMHJ.SPECIAL");
   public static Type PLAYDATAT = TargetAssembly.GetType("THMHJ.PLAYDATA");
   public static Type Texture2DT = XnaGraphicsAssembly.GetType("Microsoft.Xna.Framework.Graphics.Texture2D");
   public static Type SpriteT = TargetAssembly.GetType("THMHJ.Sprite");
   public static Type PlayDataT = TargetAssembly.GetType("THMHJ.PlayData");
   public static Type RecordManagerT = TargetAssembly.GetType("THMHJ.RecordManager");
   public static Type LongT = typeof(long);
   public static Type RecordSaveT = TargetAssembly.GetType("THMHJ.RecordSave");
   public static Type ReplaySaveT = TargetAssembly.GetType("THMHJ.ReplaySave");
}
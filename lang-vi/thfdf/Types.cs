using System;
using System.Drawing;
using System.Drawing.Text;
using System.Reflection;
using static Assemblies;
using static Launcher.Launcher;

class Assemblies {
   public static Assembly         XnaAssembly = ReferenceAssemblies["Microsoft.Xna.Framework"];
   public static Assembly     XnaGameAssembly = ReferenceAssemblies["Microsoft.Xna.Framework.Game"];
   public static Assembly XnaGraphicsAssembly = ReferenceAssemblies["Microsoft.Xna.Framework.Graphics"];
}
class Types {
   public static Type                Vector2T = XnaAssembly.GetType("Microsoft.Xna.Framework.Vector2");
   public static Type              RectangleT = XnaAssembly.GetType("Microsoft.Xna.Framework.Rectangle");
   public static Type      NullableRectangleT = typeof(Nullable<>).MakeGenericType(RectangleT);
   public static Type          SpriteEffectsT = XnaGraphicsAssembly.GetType("Microsoft.Xna.Framework.Graphics.SpriteEffects");
   public static Type                  ColorT = XnaAssembly.GetType("Microsoft.Xna.Framework.Color");
   public static Type            SpriteBatchT = XnaGraphicsAssembly.GetType("Microsoft.Xna.Framework.Graphics.SpriteBatch");
   public static Type              Texture2DT = XnaGraphicsAssembly.GetType("Microsoft.Xna.Framework.Graphics.Texture2D");
   public static Type IGraphicsDeviceServiceT = XnaGraphicsAssembly.GetType("Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService");
   public static Type      WindowsGameWindowT = XnaGameAssembly.GetType("Microsoft.Xna.Framework.WindowsGameWindow");
   public static Type          TitleLocationT = XnaAssembly.GetType("Microsoft.Xna.Framework.TitleLocation");
   public static Type            SpriteFontXT = TargetAssembly.GetType("Microsoft.Xna.Framework.SpriteFontX");


   public static Type               IntT = typeof(int);
   public static Type            StringT = typeof(string);
   public static Type              FontT = typeof(Font);
   public static Type              LongT = typeof(long);
   public static Type             FloatT = typeof(float);
   public static Type TextRenderingHintT = typeof(TextRenderingHint);


   public static Type         BonusT = TargetAssembly.GetType("THMHJ.Bonus");
   public static Type           CryT = TargetAssembly.GetType("THMHJ.Cry");
   public static Type            EDT = TargetAssembly.GetType("THMHJ.ED");
   public static Type      EntranceT = TargetAssembly.GetType("THMHJ.Entrance");
   public static Type          GameT = TargetAssembly.GetType("THMHJ.Game");
   public static Type          MainT = TargetAssembly.GetType("THMHJ.Main");
   public static Type      PLAYDATAT = TargetAssembly.GetType("THMHJ.PLAYDATA");
   public static Type      PlayDataT = TargetAssembly.GetType("THMHJ.PlayData");
   public static Type RecordManagerT = TargetAssembly.GetType("THMHJ.RecordManager");
   public static Type    RecordSaveT = TargetAssembly.GetType("THMHJ.RecordSave");
   public static Type    ReplaySaveT = TargetAssembly.GetType("THMHJ.ReplaySave");
   public static Type    StageclearT = TargetAssembly.GetType("THMHJ.Stageclear");
   public static Type       SPECIALT = TargetAssembly.GetType("THMHJ.SPECIAL");
   public static Type        SpriteT = TargetAssembly.GetType("THMHJ.Sprite");
   public static Type         TitleT = TargetAssembly.GetType("THMHJ.Title");
}
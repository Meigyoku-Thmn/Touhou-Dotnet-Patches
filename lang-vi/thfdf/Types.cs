using Harmony;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Text;
using System.Reflection;
using static Assemblies;
using static Constructors;
using static Fields;
using static Properties;
using static RuntimePatcher.Helper;
using static RuntimePatcher.Launcher;
using static Types;
using static Methods;

class Assemblies {
   public static Assembly         XnaA = ReferenceAssemblies["Microsoft.Xna.Framework"];
   public static Assembly     XnaGameA = ReferenceAssemblies["Microsoft.Xna.Framework.Game"];
   public static Assembly XnaGraphicsA = TargetVersion == "_1_05_en" ? ReferenceAssemblies["Microsoft.Xna.Framework.Graphics"] : null;
   public static Assembly SpriteFontXA = TargetVersion == "_1_04_sc" ? ReferenceAssemblies["SpriteFontX"] : null;
   public static Assembly   XnaCommonA = XnaGraphicsA ?? XnaA;
}
class Types {
   public static Type                Vector2T = XnaA.GetType("Microsoft.Xna.Framework.Vector2");
   public static Type                Vector4T = XnaA.GetType("Microsoft.Xna.Framework.Vector4");
   public static Type              RectangleT = XnaA.GetType("Microsoft.Xna.Framework.Rectangle");
   public static Type      NullableRectangleT = typeof(Nullable<>).MakeGenericType(RectangleT);
   public static Type          SpriteEffectsT = XnaCommonA.GetType("Microsoft.Xna.Framework.Graphics.SpriteEffects");
   public static Type                  ColorT = XnaA.GetType(TargetVersion == "_1_05_en" ? "Microsoft.Xna.Framework.Color" : "Microsoft.Xna.Framework.Graphics.Color");
   public static Type            SpriteBatchT = XnaCommonA.GetType("Microsoft.Xna.Framework.Graphics.SpriteBatch");
   public static Type              Texture2DT = XnaCommonA.GetType("Microsoft.Xna.Framework.Graphics.Texture2D");
   public static Type IGraphicsDeviceServiceT = XnaCommonA.GetType("Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService");
   public static Type      WindowsGameWindowT = XnaGameA.GetType("Microsoft.Xna.Framework.WindowsGameWindow");
   public static Type            SpriteFontXT = TargetAssembly.GetType("Microsoft.Xna.Framework.SpriteFontX") ?? SpriteFontXA.GetType("Microsoft.Xna.Framework.SpriteFontX");
   public static Type          TitleLocationT = XnaA.GetType("Microsoft.Xna.Framework.TitleLocation");
   public static Type       StorageContainerT = XnaA.GetType("Microsoft.Xna.Framework.Storage.StorageContainer");


   public static Type               IntT = typeof(int);
   public static Type            StringT = typeof(string);
   public static Type              FontT = typeof(Font);
   public static Type              LongT = typeof(long);
   public static Type             FloatT = typeof(float);
   public static Type         HashtableT = typeof(Hashtable);
   public static Type      HashtableArrT = HashtableT.MakeArrayType();
   public static Type              CharT = typeof(char);
   public static Type           CharArrT = CharT.MakeArrayType();
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

   public static Type AchievementManagerT = TargetAssembly.GetType("THMHJ.AchievementManager");
   public static Type    AchievementBaseT = TargetAssembly.GetType("THMHJ.AchievementBase");
   public static Type              BoardT = TargetAssembly.GetType("THMHJ.Board");
   public static Type             DialogT = TargetAssembly.GetType("THMHJ.Dialog");
   public static Type        CardDisplayT = TargetAssembly.GetType("THMHJ.CardDisplay");
   public static Type    BlackHoleAchiveT = TargetAssembly.GetType("THMHJ.Achievements.见证银河的消失");
   public static Type    MissAtAllAchiveT = TargetAssembly.GetType("THMHJ.Achievements.百发不中");
   public static Type    PotentialAchiveT = TargetAssembly.GetType("THMHJ.Achievements.底力爆发");
   public static Type  NothingLeftAchiveT = TargetAssembly.GetType("THMHJ.Achievements.无计可施");
   public static Type     SurvivalAchiveT = TargetAssembly.GetType("THMHJ.Achievements.风暴幸存者");
}
class Constructors {
   public static ConstructorInfo Vector2C = AccessTools.Constructor(Vector2T, TypeL(FloatT, FloatT));
   public static ConstructorInfo Vector4C = AccessTools.Constructor(Vector4T, TypeL(FloatT, FloatT, FloatT, FloatT));
   public static ConstructorInfo ColorC = AccessTools.Constructor(ColorT, TypeL(FloatT, FloatT, FloatT, FloatT));
   public static ConstructorInfo RectangleC = AccessTools.Constructor(RectangleT, TypeL(IntT, IntT, IntT, IntT));
   public static ConstructorInfo NullableRectangleC = AccessTools.Constructor(NullableRectangleT, TypeL(RectangleT));
}
class Methods {
   public static MethodInfo SBDrawM = AccessTools.Method(SpriteBatchT, "Draw", TypeL(Texture2DT, Vector2T, NullableRectangleT, ColorT, FloatT, Vector2T, FloatT, SpriteEffectsT, FloatT));
   public static MethodInfo SBInternalDraw = AccessTools.Method(SpriteBatchT, "InternalDraw");
}
class Fields {
   public static FieldInfo fontF       = AccessTools.Field(MainT, "font");
   public static FieldInfo dfontF      = AccessTools.Field(MainT, "dfont");
   public static FieldInfo scfontF     = AccessTools.Field(MainT, "scfont");
   public static FieldInfo scdfontF    = AccessTools.Field(MainT, "scdfont");
   public static FieldInfo SpacingF    = AccessTools.Field(SpriteFontXT, "Spacing");
   public static FieldInfo Vector2_XF  = AccessTools.Field(Vector2T, "X");
   public static FieldInfo Vector2_YF  = AccessTools.Field(Vector2T, "Y");
   public static FieldInfo achivNameF  = AccessTools.Field(AchievementManagerT, "achivName");
   public static FieldInfo achivIndexF = AccessTools.Field(AchievementManagerT, "achivIndex");
   public static FieldInfo versionF    = AccessTools.Field(MainT, "version");
}
class Properties {
   public static PropertyInfo ZeroP    = Vector2T.GetProperty("Zero");
   public static PropertyInfo Color_GP = AccessTools.Property(ColorT, "G");
   public static PropertyInfo Color_BP = AccessTools.Property(ColorT, "B");
   public static PropertyInfo Color_RP = AccessTools.Property(ColorT, "R");
}
class Delegates {
   // For Constructor
   public static FastInvokeHandler NewVector2           = Vector2C.MakeMethod().MakeDelegate();
   public static FastInvokeHandler NewVector4           = Vector4C.MakeMethod().MakeDelegate();
   public static FastInvokeHandler NewColor             = ColorC.MakeMethod().MakeDelegate();
   public static FastInvokeHandler NewRectangle         = RectangleC.MakeMethod().MakeDelegate();
   public static FastInvokeHandler NewNullableRectangle = NullableRectangleC.MakeMethod().MakeDelegate();

   // For Property
   public static GetHandler Vector2_GetZero = ZeroP.MakeGetDelegate();
   public static GetHandler CL_G            = Color_GP.MakeGetDelegate();
   public static GetHandler CL_R            = Color_RP.MakeGetDelegate();
   public static GetHandler CL_B            = Color_BP.MakeGetDelegate();

   // For Field
   public static GetHandler R_AchivName  = achivNameF.MakeDelegate();
   public static GetHandler R_AchivIndex = achivIndexF.MakeDelegate();
   public static GetHandler R_Version    = versionF.MakeDelegate();
   public static GetHandler VT2_X        = Vector2_XF.MakeDelegate();
   public static GetHandler VT2_Y        = Vector2_YF.MakeDelegate();

   // For Method
   public static FastInvokeHandler InternalDraw = SBInternalDraw.MakeDelegate();
}
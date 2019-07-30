using DotnetPatching;
using HarmonyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#if _1_04_sc
using Microsoft.Xna.Framework.Storage;
#endif
using System;
using System.Collections;
using System.Drawing.Text;
using System.Reflection;
using THMHJ;
using static Assemblies;
using static Fields;
using static RuntimePatcher.Helper;
using static RuntimePatcher.Launcher;
using static Types;

class Assemblies {
   public static Assembly         XnaA = ReferenceAssemblies["Microsoft.Xna.Framework"];
   public static Assembly     XnaGameA = ReferenceAssemblies["Microsoft.Xna.Framework.Game"];
   public static Assembly XnaGraphicsA = TargetVersion == "_1_05_en" ? ReferenceAssemblies["Microsoft.Xna.Framework.Graphics"] : null;
   public static Assembly SpriteFontXA = TargetVersion == "_1_04_sc" ? ReferenceAssemblies["SpriteFontX"] : null;
   public static Assembly   XnaCommonA = XnaGraphicsA ?? XnaA;
}
class Types {
   public static Type                Vector2T = typeof(Vector2);
   public static Type              RectangleT = typeof(Rectangle);
   public static Type      NullableRectangleT = typeof(Nullable<>).MakeGenericType(RectangleT);
   public static Type          SpriteEffectsT = typeof(SpriteEffects);
   public static Type                  ColorT = typeof(Color);
   public static Type            SpriteBatchT = typeof(SpriteBatch);
   public static Type              Texture2DT = typeof(Texture2D);
   public static Type IGraphicsDeviceServiceT = typeof(IGraphicsDeviceService);
   public static Type      WindowsGameWindowT = XnaGameA.GetType("Microsoft.Xna.Framework.WindowsGameWindow");
   public static Type            SpriteFontXT = typeof(SpriteFontX);
   public static Type          TitleLocationT = XnaA.GetType("Microsoft.Xna.Framework.TitleLocation");
#if _1_04_sc
   public static Type       StorageContainerT = typeof(StorageContainer);
#endif


   public static Type               IntT = typeof(int);
   public static Type            StringT = typeof(string);
   public static Type              FontT = typeof(System.Drawing.Font);
   public static Type              LongT = typeof(long);
   public static Type             FloatT = typeof(float);
   public static Type         HashtableT = typeof(Hashtable);
   public static Type      HashtableArrT = HashtableT.MakeArrayType();
   public static Type              CharT = typeof(char);
   public static Type           CharArrT = CharT.MakeArrayType();
   public static Type TextRenderingHintT = typeof(TextRenderingHint);


   public static Type         BonusT = TargetAssembly.GetType("THMHJ.Bonus");
   public static Type           CryT = typeof(Cry);
   public static Type            EDT = typeof(ED);
   public static Type      EntranceT = typeof(Entrance);
   public static Type          GameT = typeof(THMHJ.Game);
   public static Type          MainT = typeof(THMHJ.Main);
   public static Type      PLAYDATAT = TargetAssembly.GetType("THMHJ.PLAYDATA");
   public static Type      PlayDataT = typeof(PlayData);
   public static Type RecordManagerT = typeof(RecordManager);
   public static Type    RecordSaveT = typeof(RecordSave);
   public static Type    ReplaySaveT = typeof(ReplaySave);
   public static Type    StageclearT = TargetAssembly.GetType("THMHJ.Stageclear");
   public static Type       SPECIALT = TargetAssembly.GetType("THMHJ.SPECIAL");
   public static Type        SpriteT = typeof(Sprite);
   public static Type         TitleT = TargetAssembly.GetType("THMHJ.Title");

   public static Type AchievementManagerT = typeof(AchievementManager);
   public static Type    AchievementBaseT = typeof(AchievementBase);
   public static Type              BoardT = TargetAssembly.GetType("THMHJ.Board");
   public static Type             DialogT = TargetAssembly.GetType("THMHJ.Dialog");
   public static Type        CardDisplayT = TargetAssembly.GetType("THMHJ.CardDisplay");
   public static Type    BlackHoleAchiveT = TargetAssembly.GetType("THMHJ.Achievements.见证银河的消失");
   public static Type    MissAtAllAchiveT = TargetAssembly.GetType("THMHJ.Achievements.百发不中");
   public static Type    PotentialAchiveT = TargetAssembly.GetType("THMHJ.Achievements.底力爆发");
   public static Type  NothingLeftAchiveT = TargetAssembly.GetType("THMHJ.Achievements.无计可施");
   public static Type     SurvivalAchiveT = TargetAssembly.GetType("THMHJ.Achievements.风暴幸存者");


   public static Type            DetoursT = typeof(Detours);
   public static Type        TranspilersT = typeof(DotnetPatching.Transpilers);
}
class Constructors {
   public static ConstructorInfo Vector2C = AccessTools.Constructor(Vector2T, TypeL(FloatT, FloatT));
   public static ConstructorInfo BoardC       = AccessTools.Constructor(BoardT, TypeL(Texture2DT, StringT));
   public static ConstructorInfo PLAYDATAC    = AccessTools.Constructor(PLAYDATAT, TypeL(Texture2DT, SpriteT, SpriteT, SpriteT, SpriteT));
   public static ConstructorInfo RecordSaveC  = AccessTools.Constructor(RecordSaveT, TypeL(PlayDataT, IntT, LongT));
   public static ConstructorInfo ReplaySaveC  = AccessTools.Constructor(ReplaySaveT, TypeL(RecordManagerT, IntT, LongT, IntT, IntT, IntT));
   public static ConstructorInfo ReplaySaveC2 = AccessTools.Constructor(ReplaySaveT, TypeL(RecordManagerT, IntT, LongT, IntT, IntT, IntT, IntT));
   public static ConstructorInfo CardDisplayC = AccessTools.Constructor(CardDisplayT, TypeL(Texture2DT, HashtableArrT, IntT, IntT));
}
class Methods {
   public static MethodInfo SBDrawM              = AccessTools.Method(SpriteBatchT, "Draw", TypeL(Texture2DT, Vector2T, NullableRectangleT, ColorT, FloatT, Vector2T, FloatT, SpriteEffectsT, FloatT));
   public static MethodInfo SBDraw2M             = AccessTools.Method(SpriteBatchT, "Draw", TypeL(Texture2DT, Vector2T, NullableRectangleT, ColorT, FloatT, Vector2T, Vector2T, SpriteEffectsT, FloatT));
   public static MethodInfo SBInternalDraw       = AccessTools.Method(SpriteBatchT, "InternalDraw");
   public static MethodInfo DecryM               = AccessTools.Method(CryT, "Decry", TypeL(StringT, IntT));
   public static MethodInfo Decry0M              = AccessTools.Method(CryT, "Decry", TypeL(StringT));
   public static MethodInfo get_PathM            = AccessTools.Method(TitleLocationT, "get_Path");
#if _1_04_sc
   public static MethodInfo get_TitleLocationM   = AccessTools.Method(StorageContainerT, "get_TitleLocation");
#endif
   public static MethodInfo SetTitleM            = AccessTools.Method(WindowsGameWindowT, "SetTitle");
   public static MethodInfo GetDefaultIconM      = AccessTools.Method(WindowsGameWindowT, "GetDefaultIcon");
   public static MethodInfo SPXInitializeM       = AccessTools.Method(SpriteFontXT, "Initialize");
   public static MethodInfo SPXnewTexM           = AccessTools.Method(SpriteFontXT, "newTex");
   public static MethodInfo AchiveManInitializeM = AccessTools.Method(AchievementManagerT, "Initialize");
   public static MethodInfo BlackHoleAchiveCheckM = AccessTools.Method(BlackHoleAchiveT, "Check");
   public static MethodInfo MissAtAllAchiveM      = AccessTools.Method(MissAtAllAchiveT, "Check");
   public static MethodInfo PotentialAchiveM      = AccessTools.Method(PotentialAchiveT, "Check");
   public static MethodInfo NothingLeftAchiveM    = AccessTools.Method(NothingLeftAchiveT, "Check");
   public static MethodInfo SurvivalAchiveM       = AccessTools.Method(SurvivalAchiveT, "Check");
   public static MethodInfo DialogDrawM           = AccessTools.Method(DialogT, "Draw");
   public static MethodInfo CardDisplayUpdateM    = AccessTools.Method(CardDisplayT, "Update");

   public static MethodInfo BoardDrawM            = AccessTools.Method(BoardT, "Draw");
   public static MethodInfo EntranceLoadM         = AccessTools.Method(EntranceT, "Load");
   public static MethodInfo GameInitM             = AccessTools.Method(GameT, "Init");
   public static MethodInfo EntranceDrawM         = AccessTools.Method(EntranceT, "Draw");
   public static MethodInfo addTexM               = AccessTools.Method(SpriteFontXT, "addTex");
   public static MethodInfo SPECIALDrawM          = AccessTools.Method(SPECIALT, "Draw");
   public static MethodInfo GameSDrawM            = AccessTools.Method(GameT, "SDraw");
   public static MethodInfo BonusDrawM            = AccessTools.Method(BonusT, "Draw");
   public static MethodInfo StageclearDrawM       = AccessTools.Method(StageclearT, "Draw");
   public static MethodInfo SPECIALTextureM       = AccessTools.Method(SPECIALT, "Texture");
   public static MethodInfo TitleDrawM            = AccessTools.Method(TitleT, "Draw");
   public static MethodInfo EDDrawM               = AccessTools.Method(EDT, "Draw");


   public static MethodInfo DecryH                 = AccessTools.Method(DetoursT, nameof(Detours.Decry));
   public static MethodInfo Decry0H                = AccessTools.Method(DetoursT, nameof(Detours.Decry0));
   public static MethodInfo TitleLocationPathH     = AccessTools.Method(DetoursT, nameof(Detours.TitleLocationPath));
   public static MethodInfo SetTitleH              = AccessTools.Method(DetoursT, nameof(Detours.SetTitle));
   public static MethodInfo GetDefaultIconH        = AccessTools.Method(DetoursT, nameof(Detours.GetDefaultIcon));
   public static MethodInfo SpriteFontXInitializeH = AccessTools.Method(DetoursT, nameof(Detours.SpriteFontXInitialize));
   public static MethodInfo newTexH                = AccessTools.Method(DetoursT, nameof(Detours.newTex));
   public static MethodInfo AchieveInitializeH     = AccessTools.Method(DetoursT, nameof(Detours.AchieveInitialize));
   public static MethodInfo SBDraw2H               = AccessTools.Method(DetoursT, nameof(Detours.SBDraw2));

   public static MethodInfo BoardConstructorH        = AccessTools.Method(TranspilersT, nameof(DotnetPatching.Transpilers.BoardConstructor));
   public static MethodInfo BlackHoleAchiveCheckH    = AccessTools.Method(TranspilersT, nameof(DotnetPatching.Transpilers.BlackHoleAchiveCheck));
   public static MethodInfo MissAtAllAchiveCheckH    = AccessTools.Method(TranspilersT, nameof(DotnetPatching.Transpilers.MissAtAllAchiveCheck));
   public static MethodInfo PotentialAchiveCheckH    = AccessTools.Method(TranspilersT, nameof(DotnetPatching.Transpilers.PotentialAchiveCheck));
   public static MethodInfo NothingLeftAchiveCheckH  = AccessTools.Method(TranspilersT, nameof(DotnetPatching.Transpilers.NothingLeftAchiveCheck));
   public static MethodInfo SurvivalAchiveCheckH     = AccessTools.Method(TranspilersT, nameof(DotnetPatching.Transpilers.SurvivalAchiveCheck));
   public static MethodInfo DrawMethodOfDialogH      = AccessTools.Method(TranspilersT, nameof(DotnetPatching.Transpilers.DrawMethodOfDialog));
   public static MethodInfo UpdateOfCardDisplayH     = AccessTools.Method(TranspilersT, nameof(DotnetPatching.Transpilers.UpdateOfCardDisplay));

   public static MethodInfo DrawMethodOfBoardH   = AccessTools.Method(TranspilersT, nameof(DotnetPatching.Transpilers.DrawMethodOfBoard));
   public static MethodInfo LoadMethodOfEntranceTranspilerH   = AccessTools.Method(TranspilersT, nameof(DotnetPatching.Transpilers.LoadMethodOfEntranceTranspiler));
   public static MethodInfo InitMethodOfGameTranspilerH   = AccessTools.Method(TranspilersT, nameof(DotnetPatching.Transpilers.InitMethodOfGameTranspiler));
   public static MethodInfo DrawMethodOfEntranceTranspilerH   = AccessTools.Method(TranspilersT, nameof(DotnetPatching.Transpilers.DrawMethodOfEntranceTranspiler));
   public static MethodInfo AddTexTranspilerH   = AccessTools.Method(TranspilersT, nameof(DotnetPatching.Transpilers.AddTexTranspiler));
   public static MethodInfo DrawMethodOfSPECIALTranspilerH   = AccessTools.Method(TranspilersT, nameof(DotnetPatching.Transpilers.DrawMethodOfSPECIALTranspiler));
   public static MethodInfo SDrawMethodOfGameTranspilerH   = AccessTools.Method(TranspilersT, nameof(DotnetPatching.Transpilers.SDrawMethodOfGameTranspiler));
   public static MethodInfo DrawMethodOfBonusH   = AccessTools.Method(TranspilersT, nameof(DotnetPatching.Transpilers.DrawMethodOfBonus));
   public static MethodInfo DrawMethodOfStageclearH   = AccessTools.Method(TranspilersT, nameof(DotnetPatching.Transpilers.DrawMethodOfStageclear));
   public static MethodInfo TextureMethodOfSPECIALH   = AccessTools.Method(TranspilersT, nameof(DotnetPatching.Transpilers.TextureMethodOfSPECIAL));
   public static MethodInfo PLAYDATAConstructorH   = AccessTools.Method(TranspilersT, nameof(DotnetPatching.Transpilers.PLAYDATAConstructor));
   public static MethodInfo RecordSaveConstructorH   = AccessTools.Method(TranspilersT, nameof(DotnetPatching.Transpilers.RecordSaveConstructor));
   public static MethodInfo ReplaySaveConstructor1H   = AccessTools.Method(TranspilersT, nameof(DotnetPatching.Transpilers.ReplaySaveConstructor1));
   public static MethodInfo ReplaySaveConstructor2H   = AccessTools.Method(TranspilersT, nameof(DotnetPatching.Transpilers.ReplaySaveConstructor2));
   public static MethodInfo DrawMethodOfTitleH   = AccessTools.Method(TranspilersT, nameof(DotnetPatching.Transpilers.DrawMethodOfTitle));
   public static MethodInfo DrawMethodOfEDH   = AccessTools.Method(TranspilersT, nameof(DotnetPatching.Transpilers.DrawMethodOfED));
   public static MethodInfo CardDisplayConstructorH   = AccessTools.Method(TranspilersT, nameof(DotnetPatching.Transpilers.CardDisplayConstructor));
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
class Delegates {
   // For Field
   public static GetHandler R_AchivName  = achivNameF.MakeDelegate();
   public static GetHandler R_AchivIndex = achivIndexF.MakeDelegate();
   public static GetHandler R_Version    = versionF.MakeDelegate();
}
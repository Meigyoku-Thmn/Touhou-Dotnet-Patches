using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

using XNATexture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
// this is the best way to implement INotifyPropertyChanged without AOP I think.
public abstract class ConfigBase : INotifyPropertyChanged {
   protected Dictionary<string, object> BackStore = new Dictionary<string, object>();
   public event PropertyChangedEventHandler PropertyChanged;
   protected dynamic GetValue([CallerMemberName] string key = null) => BackStore[key];
   protected void SetValue<T>(T value, [CallerMemberName] string key = null) {
      if (BackStore[key]?.Equals(value) ?? false) return;
      BackStore[key] = value;
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(key));
   }
   protected ConfigBase() {
      var props = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
      foreach (var prop in props) {
         BackStore[prop.Name] = prop.PropertyType.IsValueType ? Activator.CreateInstance(prop.PropertyType) : null;
      }
   }
}
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class Config : ConfigBase {
   [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
   public class AlterTextConfig : ConfigBase {
      public bool UseBlackBorder { get => GetValue(); set => SetValue(value); }
      public bool Enabled { get => GetValue(); set => SetValue(value); }
      [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
      public class AlterFontConfig : ConfigBase {
         public bool Enabled { get => GetValue(); set => SetValue(value); }
         [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
         public class FontDetail : ConfigBase {
            public FontDetail(Font font) => FontI = font;
            [JsonProperty] private string Font;
            public bool Enabled { get => GetValue(); set => SetValue(value); }
            [JsonIgnore] public Font FontI { get => GetValue(); set => SetValue(value); }
            [OnSerializing]
            internal void OnSerializingMethod(StreamingContext context)
               => Font = new FontConverter().ConvertToInvariantString(FontI);
            [OnDeserialized]
            internal void OnDeserializedMethod(StreamingContext context)
               => FontI = Font != null ? (Font)new FontConverter().ConvertFromInvariantString(Font) : FontI;
         }
         public FontDetail FPSFont = new FontDetail(new Font(new FontFamily("Cambria"), 12f, FontStyle.Regular, GraphicsUnit.Pixel));
         public FontDetail SpellNameFont = new FontDetail(new Font("Arial", 16f, FontStyle.Bold, GraphicsUnit.Pixel));
         public FontDetail SpellStatFont = new FontDetail(new Font("Cambria", 10f, FontStyle.Bold, GraphicsUnit.Pixel));
         public FontDetail RestFont = new FontDetail(new Font("Arial", 16f, FontStyle.Bold, GraphicsUnit.Pixel));
         public class ColorDetail : ConfigBase {
            [JsonProperty] private string Color;
            public bool Enabled { get => GetValue(); set => SetValue(value); }
            [JsonIgnore] public Color ColorI { get => GetValue(); set => SetValue(value); }
            [OnSerializing]
            internal void OnSerializingMethod(StreamingContext context)
               => Color = new ColorConverter().ConvertToInvariantString(ColorI);
            [OnDeserialized]
            internal void OnDeserializedMethod(StreamingContext context)
               => ColorI = Color != null ? (Color)new ColorConverter().ConvertFromInvariantString(Color) : ColorI;
         }
         public ColorDetail ProtagonistLineColor = new ColorDetail() { ColorI = Color.Green };
         public ColorDetail AntagonistLineColor = new ColorDetail() { ColorI = Color.Red };
      }
      public AlterFontConfig AlterFontCfg = new AlterFontConfig();
   }
   public AlterTextConfig AlterTextCfg = new AlterTextConfig();
   public bool TachieOnTop { get => GetValue(); set => SetValue(value); }
   public bool UseSkipPatch { get => GetValue(); set => SetValue(value); }

   private string ConfigFileName = "Config.jsonc";
   public void Save() {
      var json = JsonConvert.SerializeObject(this, Formatting.Indented);
      File.WriteAllText(Path.Combine(Resource.CurrentDirPath, ConfigFileName), json);
   }
   public Config() {
      try {
         var json = File.ReadAllText(Path.Combine(Resource.CurrentDirPath, ConfigFileName));
         JsonConvert.PopulateObject(json, this);
      }
      catch (Exception ex) when (ex is FileNotFoundException || ex is DirectoryNotFoundException) {
      }
   }
}

class Resource {
   public static string CurrentDirPath = Path.GetDirectoryName(@"<CURRENT_FILE>");
   public static Config Config = new Config();
   public static string DebugResourceWorkingPath = Path.Combine(CurrentDirPath, "DebugResource");
   public static string ResourceWorkingPath = CurrentDirPath;
   static Regex ignoredFiles = new Regex(
         @"^(:?(?:4\.xna)|(?:5\.xna)|(?:8\.xna)|(?:.*?\.dat))$", RegexOptions.Compiled);
   public static string MapResourcePath(string path) {
      path = path.Replace('/', '\\');
      var fileName = Path.GetFileNameWithoutExtension(path);
      var fileExt = Path.GetExtension(path).ToLower();
      var newExt = default(string);
      var graphicsRootPath = @"Content\Graphics";
      var dataRootPath = @"Content\Data";
      if (path.IndexOf(dataRootPath, 0, dataRootPath.Length) == 0 && !ignoredFiles.IsMatch(fileName + fileExt))
         newExt = ".txt";
      else if (fileExt == ".dat" || path.IndexOf(graphicsRootPath, 0, graphicsRootPath.Length) == 0)
         newExt = ".png";
      if (newExt != null) path = Path.Combine(Path.GetDirectoryName(path), fileName + newExt);
      return path;
   }
   public static string ResolveModPath(string fileName) {
      var modeFilePath = MapResourcePath(fileName);
      string newFileName;
      if (Config.UseSkipPatch) {
         newFileName = Path.Combine(DebugResourceWorkingPath, modeFilePath);
         if (File.Exists(newFileName)) {
            Console.WriteLine("Found debug mod: " + fileName);
            return newFileName;
         }
      }
      newFileName = Path.Combine(ResourceWorkingPath, modeFilePath);
      if (File.Exists(newFileName)) {
         Console.WriteLine("Found mod: " + fileName);
         return newFileName;
      }
      return null;
   }
   static List<string> strs = ((Func<List<string>>)(() => {
      var rs = new List<string>(){ "" };
      rs.AddRange(File.ReadAllLines(Path.Combine(CurrentDirPath, @"Content\Data\a0.txt")));
      return rs;
   }))();
   static public IDictionary<XNATexture2D, SpriteFontX> charTextures = new Dictionary<XNATexture2D, SpriteFontX>();
   static public SpriteFontX font = null;
   static public SpriteFontX dfont = null;
   static public SpriteFontX scfont = null;
   static public SpriteFontX scdfont = null;
   public static void SetSpriteFont(int key, SpriteFontX _font) {
      switch (key) {
         case 0: font = _font; break;
         case 1: dfont = _font; break;
         case 2: scfont = _font; break;
         case 3: scdfont = _font; break;
      }
   }
   public static readonly string GET = strs[2];
   public static readonly string ACHIEVED_DIFFICULTY_LEVEL = strs[5];
   public static readonly string NEED_UNLOCK_ALL_SPELLS = strs[3];
   public static readonly string NEED_TO_PASS_THIS_STAGE = strs[4];
   public static readonly string BLACK_HOLE_HEART = strs[6];
   public static readonly string BLACK_HOLE_CATACLYSM = strs[7];
   public static readonly string ICE_SIGN_NO_MISS = strs[8];
   public static readonly string AMERGADDON = strs[9];
   public static readonly string MURDER_INTENTION = strs[10];
   public static readonly string KILLING_VORTEX = strs[11];
}
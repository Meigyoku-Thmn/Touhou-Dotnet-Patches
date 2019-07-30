using RuntimePatcher;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace DotnetPatching {
   public partial class Custom : Form {
      public Custom() {
         InitializeComponent();
         Icon = Launcher.TargetIcon;
         PrepareBinding();
      }
      [DllImport("user32.dll")]
      [return: MarshalAs(UnmanagedType.Bool)]
      static extern bool SetForegroundWindow(IntPtr hWnd);
      private async void cmdOpenDefaultConfig_Click(object sender, EventArgs e) {
         Enabled = false;
         await Task.Run(() => {
            var customExe = Process.Start(Path.Combine(Launcher.TargetDirectory, "custom.exe"));
            customExe.WaitForExit();
         });
         Enabled = true;
         SetForegroundWindow(Handle);
      }

      private void lnkToMyHome_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
         => Process.Start("https://github.com/Meigyoku-Thmn/Touhou-Dotnet-Patches");

      class Schema {
         public IBindableComponent TargetControl;
         public string TargetField;
         public object DataStore;
         public string DataField;
         public DataSourceUpdateMode UpdateMode;
         public ConvertEventHandler Parser;
         public ConvertEventHandler Formatter;
         public Schema(IBindableComponent targetControl, string targetField, object dataStore, string dataField, DataSourceUpdateMode updateMode = DataSourceUpdateMode.OnPropertyChanged, ConvertEventHandler formatter = null, ConvertEventHandler parser = null) {
            TargetControl = targetControl; TargetField = targetField; DataStore = dataStore; DataField = dataField; UpdateMode = updateMode; Parser = parser; Formatter = formatter;
         }
      }

      private void PrepareBinding() {
         var Config = Resource.Config;
         var AlterTextCfg = Config.AlterTextCfg;
         var AlterFontCfg = Config.AlterTextCfg.AlterFontCfg;
         ConvertEventHandler FontFormatter = (s, e) => {
            var font = (Font)e.Value;
            var baseFont = ((Binding)s).Control.Font;
            e.Value = new Font(font.FontFamily, baseFont.Size, font.Style);
         };
         var OneWay = DataSourceUpdateMode.Never;
         var schemas = new Schema[] {
            new Schema(chkAlterText, "Checked", AlterTextCfg, nameof(AlterTextCfg.Enabled)),
            new Schema(grpAlterText, "Enabled", AlterTextCfg, nameof(AlterTextCfg.Enabled), OneWay),

            new Schema(chkUseBorder, "Checked", AlterTextCfg, nameof(AlterTextCfg.UseBlackBorder)),

            new Schema(chkCustomFont, "Checked", AlterFontCfg, nameof(AlterFontCfg.Enabled)),
            new Schema(grpAlterFont, "Enabled", AlterFontCfg, nameof(AlterFontCfg.Enabled), OneWay),

            new Schema(chkAlterFPSFont, "Checked", AlterFontCfg.FPSFont, nameof(AlterFontCfg.FPSFont.Enabled)),
            new Schema(chkAlterSpellNameFont, "Checked", AlterFontCfg.SpellNameFont, nameof(AlterFontCfg.SpellNameFont.Enabled)),
            new Schema(chkAlterSpellStatisticsFont, "Checked", AlterFontCfg.SpellStatFont, nameof(AlterFontCfg.SpellStatFont.Enabled)),
            new Schema(chkAlterRestFont, "Checked", AlterFontCfg.RestFont, nameof(AlterFontCfg.RestFont.Enabled)),

            new Schema(lblFPSFont, "Enabled", AlterFontCfg.FPSFont, nameof(AlterFontCfg.FPSFont.Enabled), OneWay),
            new Schema(lblSpellFont, "Enabled", AlterFontCfg.SpellNameFont, nameof(AlterFontCfg.SpellNameFont.Enabled), OneWay),
            new Schema(lblSpellStatisticsFont, "Enabled", AlterFontCfg.SpellStatFont, nameof(AlterFontCfg.SpellStatFont.Enabled), OneWay),
            new Schema(lblRestFont, "Enabled", AlterFontCfg.RestFont, nameof(AlterFontCfg.RestFont.Enabled), OneWay),

            new Schema(lblFPSFont, "Text", AlterFontCfg.FPSFont, nameof(AlterFontCfg.FPSFont.FontI), OneWay),
            new Schema(lblRestFont, "Text", AlterFontCfg.RestFont, nameof(AlterFontCfg.RestFont.FontI), OneWay),
            new Schema(lblSpellFont, "Text", AlterFontCfg.SpellNameFont, nameof(AlterFontCfg.SpellNameFont.FontI), OneWay),
            new Schema(lblSpellStatisticsFont, "Text", AlterFontCfg.SpellStatFont, nameof(AlterFontCfg.SpellStatFont.FontI), OneWay),

            new Schema(lblFPSFont, "Font", AlterFontCfg.FPSFont, nameof(AlterFontCfg.FPSFont.FontI), OneWay, FontFormatter),
            new Schema(lblRestFont, "Font", AlterFontCfg.RestFont, nameof(AlterFontCfg.RestFont.FontI), OneWay, FontFormatter),
            new Schema(lblSpellFont, "Font", AlterFontCfg.SpellNameFont, nameof(AlterFontCfg.SpellNameFont.FontI), OneWay, FontFormatter),
            new Schema(lblSpellStatisticsFont, "Font", AlterFontCfg.SpellStatFont, nameof(AlterFontCfg.SpellStatFont.FontI), OneWay, FontFormatter),


            new Schema(chkProtagonistLineColor, "Checked", AlterFontCfg.ProtagonistLineColor, nameof(AlterFontCfg.ProtagonistLineColor.Enabled)),
            new Schema(chkAntagonistLineColor, "Checked", AlterFontCfg.AntagonistLineColor, nameof(AlterFontCfg.AntagonistLineColor.Enabled)),

            new Schema(lblProtagonistLineColor, "Enabled", AlterFontCfg.ProtagonistLineColor, nameof(AlterFontCfg.ProtagonistLineColor.Enabled), OneWay),
            new Schema(lblAntagonistLineColor, "Enabled", AlterFontCfg.AntagonistLineColor, nameof(AlterFontCfg.AntagonistLineColor.Enabled), OneWay),

            new Schema(lblProtagonistLineColor, "Text", AlterFontCfg.ProtagonistLineColor, nameof(AlterFontCfg.ProtagonistLineColor.ColorI), OneWay),
            new Schema(lblAntagonistLineColor, "Text", AlterFontCfg.AntagonistLineColor, nameof(AlterFontCfg.AntagonistLineColor.ColorI), OneWay),
            new Schema(lblProtagonistLineColor, "ForeColor", AlterFontCfg.ProtagonistLineColor, nameof(AlterFontCfg.ProtagonistLineColor.ColorI), OneWay),
            new Schema(lblAntagonistLineColor, "ForeColor", AlterFontCfg.AntagonistLineColor, nameof(AlterFontCfg.AntagonistLineColor.ColorI), OneWay),

            new Schema(chkTachieOnTop, "Checked", Config, nameof(Config.TachieOnTop)),
            new Schema(chkUseSkip, "Checked", Config, nameof(Config.UseSkipPatch)),
         };
         foreach (var schema in schemas) {
            var binding = new Binding(
               schema.TargetField, schema.DataStore, schema.DataField, true, schema.UpdateMode);
            binding.Parse += schema.Parser;
            binding.Format += schema.Formatter;
            schema.TargetControl.DataBindings.Add(binding);
         }
      }

      private void cmdStart_Click(object sender, EventArgs e) {
         FormClosing -= Custom_FormClosing;
         DialogResult = DialogResult.OK;
         Resource.Config.Save();
         Close();
      }

      private void Custom_FormClosing(object sender, FormClosingEventArgs e) {
         var rs = MessageBox.Show("Bạn có muốn lưu thiết lập không?", "Thống báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
         if (rs == DialogResult.Yes) Resource.Config.Save();
         DialogResult = DialogResult.Cancel;
      }

      private void envChangeFont(object sender, EventArgs e) {
         var config = Resource.Config;
         var index = ((Control)sender).Tag.ToString();
         Font font = null;
         dynamic FontObj = null;
         switch (index) {
            case "fps":
               FontObj = config.AlterTextCfg.AlterFontCfg.FPSFont;
               font = FontObj.FontI; break;
            case "spellcard":
               FontObj = config.AlterTextCfg.AlterFontCfg.SpellNameFont;
               font = FontObj.FontI; break;
            case "statistics":
               FontObj = config.AlterTextCfg.AlterFontCfg.SpellStatFont;
               font = FontObj.FontI; break;
            case "rest":
               FontObj = config.AlterTextCfg.AlterFontCfg.RestFont;
               font = FontObj.FontI; break;
         }
         var newFont = Helper.OpenFontDialog(new Font(font.FontFamily, font.Size, font.Style));
         if (newFont == null) return;
         FontObj.FontI = newFont;
      }

      private void FontNameLabel_MouseEnter(object sender, EventArgs e)
         => ((Control)sender).BackColor = Color.Gainsboro;
      private void FontNameLabel_MouseLeave(object sender, EventArgs e)
         => ((Control)sender).BackColor = BackColor;

      private void lblProtagonistLineColor_Click(object sender, EventArgs e) {
         var config = Resource.Config;
         dynamic ColorObj = config.AlterTextCfg.AlterFontCfg.ProtagonistLineColor;
         Color color = ColorObj.ColorI;
         colorDlg.Color = color;
         DialogResult result = colorDlg.ShowDialog();
         if (result != DialogResult.OK) return;
         ColorObj.ColorI = colorDlg.Color;
      }

      private void lblAntagonistLineColor_Click(object sender, EventArgs e) {
         var config = Resource.Config;
         dynamic ColorObj = config.AlterTextCfg.AlterFontCfg.AntagonistLineColor;
         Color color = ColorObj.ColorI;
         colorDlg.Color = color;
         DialogResult result = colorDlg.ShowDialog();
         if (result != DialogResult.OK) return;
         ColorObj.ColorI = colorDlg.Color;
      }

      private void cmdAbout_Click(object sender, EventArgs e) {

      }

      private void cmdGlossary_Click(object sender, EventArgs e) {
      }

      private void cmdOpenOmake_Click(object sender, EventArgs e) 
         => Process.Start(Path.Combine(Resource.CurrentDirPath, "omake_vn.html"));
   }
}

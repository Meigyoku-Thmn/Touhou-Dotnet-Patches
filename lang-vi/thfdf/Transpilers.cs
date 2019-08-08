using HarmonyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReflectionMagic;
using RuntimePatcher;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using static Constructors;
using static Fields;
using static Methods;
using static Resource;
using static RuntimePatcher.Helper;
using static Types;

//css_import Transpilers.1.04sc.cs
namespace DotnetPatching {
   using System;
   using System.Diagnostics;
   using static RuntimePatcher.PatchInfo;
   using CodeInstructionMap = IDictionary<int, CodeInstructionWrapper>;
   using CodeInstructions = IEnumerable<CodeInstruction>;
   using TranspilerTuple = RuntimePatcher.PatchInfo;
#if _1_05_en
   using XnaColor = Microsoft.Xna.Framework.Color;
#elif _1_04_sc
   using XnaColor = Microsoft.Xna.Framework.Graphics.Color;
#endif
   using XnaRectangle = Microsoft.Xna.Framework.Rectangle;
   partial class Transpilers {
      public static List<TranspilerTuple> OnSetup() {
         return new TranspilerTuple[] {
            PM(BoardDrawM, DrawMethodOfBoardH),
             //5 lines of description in music room
            PM(EntranceLoadM, LoadMethodOfEntranceTranspilerH),
            // bless image fullscreen
            PM(GameInitM, InitMethodOfGameTranspilerH),
            // bless image fullscreen
             //5 lines of description in music room, align for more space
            PM(EntranceDrawM, DrawMethodOfEntranceTranspilerH),
            // The MeasureString method is horrible for trailing space character (bigger width than real width)
            PM(addTexM, AddTexTranspilerH),
             //Fix The Line Break Algorithm in Achievement Screen
            PM(SPECIALDrawM, DrawMethodOfSPECIALTranspilerH),
         }.Concat(!Resource.Config.TachieOnTop ? new TranspilerTuple[0] : new TranspilerTuple[] {
            // Make the character's portraits on top of ui frame
            PM(GameSDrawM, SDrawMethodOfGameTranspilerH),
         }).Concat(new TranspilerTuple[] {
            // BossList.xna hack to enlarge canvases
            PM(BonusDrawM, DrawMethodOfBonusH),
            PM(StageclearDrawM, DrawMethodOfStageclearH),
            // Achivrate position and sourceRectangle
            PM(SPECIALTextureM, TextureMethodOfSPECIALH),
            // Playdata position and sourceRectangle
            PM(PLAYDATAC, PLAYDATAConstructorH),
            // Translate 终 in Replay Saving Screen and Record Saving Screen
            PM(RecordSaveC, RecordSaveConstructorH),
            PM(ReplaySaveC, ReplaySaveConstructor1H),
            PM(ReplaySaveC2, ReplaySaveConstructor2H),
            // Adjust stagetitle metrics
            PM(TitleDrawM, DrawMethodOfTitleH),
            // Adjust 31.xna metrics
            PM(EDDrawM, DrawMethodOfEDH),
            // Adjust spellcard name in gameplay for more pleasant to the eyes
            PM(CardDisplayC, CardDisplayConstructorH),
            PM(DialogDrawM, DrawMethodOfDialogH),
         })
#if _1_04_sc
         .Concat(OnSetup_1_04_sc())
#endif
         // stroke border for text, instead of drop shadow
         .Concat(DrawFuncs.Select(func => PM(func, DrawMethodsH)))
         .ToList()
         ;
      }
      public static CodeInstructions DrawMethods(CodeInstructions instructions) {
         foreach (var instr in instructions) {
            if (!(instr.opcode == OpCodes.Call || instr.opcode == OpCodes.Callvirt)) continue;
            if (ReferenceEquals(instr.operand, SPXDraw1)) {
               instr.operand = Draw1H; instr.opcode = OpCodes.Call;
            }
            else if (ReferenceEquals(instr.operand, SPXDraw2)) {
               instr.operand = Draw2H; instr.opcode = OpCodes.Call;
            }
            else if (ReferenceEquals(instr.operand, SPXDraw3)) {
               instr.operand = Draw3H; instr.opcode = OpCodes.Call;
            }
            else if (ReferenceEquals(instr.operand, SPXDraw4)) {
               instr.operand = Draw4H; instr.opcode = OpCodes.Call;
            }
         }
         return instructions;
      }

      public static Vector2 Draw1(SpriteFontX __inst, SpriteBatch sb, string str, Vector2 position, XnaColor color) {
         return Draw4(__inst, sb, str.ToCharArray(), position, new Vector2(float.MaxValue, float.MaxValue), Vector2.One, color);
      }
      public static Vector2 Draw2(SpriteFontX __inst, SpriteBatch sb, char[] str, Vector2 position, XnaColor color) {
         return Draw4(__inst, sb, str, position, new Vector2(float.MaxValue, float.MaxValue), Vector2.One, color);
      }
      public static Vector2 Draw3(SpriteFontX __inst, SpriteBatch sb, string str, Vector2 position, Vector2 maxBound, Vector2 scale, XnaColor color) {
         return Draw4(__inst, sb, str.ToCharArray(), position, maxBound, scale, color);
      }
      static float borderSize = 1.5f;
      static public bool TestColor(XnaColor input, XnaColor solid) {
         return input.R == solid.R && input.G == solid.G && input.B == solid.B;
      }
      public static Vector2 Draw4(SpriteFontX __inst, SpriteBatch sb, char[] str, Vector2 position, Vector2 maxBound, Vector2 scale, XnaColor color) {
         Func<Vector2, Vector2> DrawSp = (newPos) => __inst.Draw(sb, str, newPos, maxBound, scale, color);
         var config = Resource.Config;
         var alterTextCfg = config.AlterTextCfg;
         var alterFontCfg = alterTextCfg.AlterFontCfg;
         var protagonistLineColor = alterFontCfg.ProtagonistLineColor;
         var antagonistLineColor = alterFontCfg.AntagonistLineColor;
         if (TestColor(color, XnaColor.Black)) {
            if (alterTextCfg.Enabled && alterTextCfg.UseBlackBorder) {
               position.X--; position.Y--;
               float X = position.X, Y = position.Y;
               position = new Vector2(X - borderSize, Y - borderSize);
               DrawSp(position);
               position = new Vector2(X + borderSize, Y - borderSize);
               DrawSp(position);
               position = new Vector2(X + borderSize, Y + borderSize);
               DrawSp(position);
               position = new Vector2(X - borderSize, Y + borderSize);
               DrawSp(position);
               position = new Vector2(X, Y + borderSize);
               DrawSp(position);
               position = new Vector2(X, Y - borderSize);
               DrawSp(position);
               position = new Vector2(X - borderSize, Y);
               DrawSp(position);
               position = new Vector2(X + borderSize, Y);
               return DrawSp(position);
            }
         }
         return DrawSp(position);
      }

      public static CodeInstructions DrawMethodOfDialog(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         var instrs = instructions as List<CodeInstruction>;
         // RGB
#if _1_05_en
         const int OFFSET1 = 0x041D, OFFSET2 = 0x0422, OFFSET3 = 0x0427, 
            OFFSET4 = 0x0458, OFFSET5 = 0x045D, OFFSET6 = 0x0462;
         var alterTextCfg = Resource.Config.AlterTextCfg;
         var alterFontCfg = alterTextCfg.AlterFontCfg;
         var protagonistLineColor = alterFontCfg.ProtagonistLineColor;
         var antagonistLineColor = alterFontCfg.AntagonistLineColor;
         if (alterTextCfg.Enabled && alterFontCfg.Enabled) {
            if (protagonistLineColor.Enabled) {
               var color = protagonistLineColor.ColorI;
               instrByOffsets[OFFSET1].cdInstr.operand = (float)color.R / 255;
               instrByOffsets[OFFSET2].cdInstr.operand = (float)color.G / 255;
               instrByOffsets[OFFSET3].cdInstr.operand = (float)color.B / 255;
            }
            if (antagonistLineColor.Enabled) {
               var color = antagonistLineColor.ColorI;
               instrByOffsets[OFFSET4].cdInstr.operand = (float)color.R / 255;
               instrByOffsets[OFFSET5].cdInstr.operand = (float)color.G / 255;
               instrByOffsets[OFFSET6].cdInstr.operand = (float)color.B / 255;
            }
         }
#endif
#if _1_04_sc
         var begin = instrByOffsets[0x02F8];
         var end = instrByOffsets[0x0425];
         instrs.RemoveRange(begin.index, end.index - begin.index + 1);
         var drawDialogM = AccessTools.Method(typeof(Transpilers), nameof(DrawDialog));
         instrs.InsertRange(begin.index, new CodeInstruction[] {
            new CodeInstruction(OpCodes.Ldarg_0),
            new CodeInstruction(OpCodes.Ldarg_1),
            new CodeInstruction(OpCodes.Call, drawDialogM),
         });
         instrByOffsets[0x042A].cdInstr.labels = new List<Label>();
#endif
         return instrs;
      }
      public static CodeInstructions DrawMethodOfBoard(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
#if _1_04_sc
         instrByOffsets[0x00F4].cdInstr.operand = GET;
         instrByOffsets[0x01A8].cdInstr.operand = GET;
         instrByOffsets[0x01EE].cdInstr.operand = GET;
         // board metrics
         instrByOffsets[0x000F].cdInstr.operand = 443;
         instrByOffsets[0x0080].cdInstr.operand = 444;
         instrByOffsets[0x0114].cdInstr.operand = 350f;
#endif
#if _1_05_en
         const int OFFSET1 = 0x00CF, OFFSET2 = 0x01fa, OFFSET3 = 0x01b2, OFFSET4 = 0x0184, OFFSET5 = 0x0143;
#elif _1_04_sc
         const int OFFSET1 = 0x00CF, OFFSET2 = 0x01f4, OFFSET3 = 0x01ae, OFFSET4 = 0x0182, OFFSET5 = 0x0141;
#endif
         var instrs = instructions as List<CodeInstruction>;
         var target1 = instrByOffsets[OFFSET1];
         target1.cdInstr.opcode = OpCodes.Ldstr;
         target1.cdInstr.operand = GET;
         instrs[target1.index + 1].opcode = OpCodes.Nop;
         var target2 = instrByOffsets[OFFSET2];
         var posPlusLen = instrs.Skip(target2.index).Take(2).ToList();
         instrs.RemoveRange(target2.index, 2);
         instrs.RemoveRange(instrByOffsets[OFFSET3].index, 2);
         instrs.InsertRange(instrByOffsets[OFFSET4].index, posPlusLen.Select(e => e.Clone()));
         instrs.InsertRange(instrByOffsets[OFFSET5].index, posPlusLen.Select(e => e.Clone()));

         return instrs;
      }

      public static CodeInstructions CardDisplayConstructor(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         var instrs = instructions as List<CodeInstruction>;
#if _1_04_sc
         var target = instrByOffsets[0x00D4];
         var labels = target.cdInstr.labels;
         instrs.RemoveRange(target.index, 7);
#elif _1_05_en
         var target = instrByOffsets[0x00D4];
         var labels = target.cdInstr.labels;
         var end = instrByOffsets[0x012B];
         instrs.RemoveRange(target.index, end.index - target.index + 1);
         instrByOffsets[0x012D].cdInstr.labels = new List<Label>();
#endif
         instrs.InsertRange(target.index, new CodeInstruction[] {
            new CodeInstruction(OpCodes.Ldarg_0),
            new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Transpilers), nameof(CalculateSpellNameMetrics))),
         });
         instrs[target.index].labels = labels;
         return instrs;
      }
      static readonly dynamic fontpos = new Vector2(418f, 301f);
      static readonly dynamic defaultSpacing = Vector2.Zero;
      static readonly float maxSpellWidth = 280.0f;
      // I provide a faster and more accurate spellname metrics calculating method
      static void CalculateSpellNameMetrics(object __instance) {
         // you can't assign to Spacing.X (Spacing is in struct type), this might be a bug in Dotnet Runtime's Dynamic Type
         scfont.Spacing = defaultSpacing;
         var _this = __instance.AsDynamic();
         _this.fontpos = fontpos;
         var originalSpellWidth = scfont.MeasureString(_this.cardname).X;
         if (originalSpellWidth <= maxSpellWidth) {
            _this.fontpl = originalSpellWidth;
            return;
         }
         scfont.Spacing = new Vector2((280.0f - originalSpellWidth) / (_this.cardname.Length - 1), 0f);
         _this.fontpl = maxSpellWidth;
      }
      public static CodeInstructions DrawMethodOfED(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         // s.Draw(this.text, new Vector2(60f, (float) (175 + 38 * index)), new Rectangle?(new Rectangle(0, 38 * index, 307, 38)), new Color(1f, 1f, 1f, this.textcolor[index]), 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
         // change 60f to 0f
         // change 307 to 640
#if _1_05_en
         const int OFFSET1 = 0x00b1, OFFSET2 = 0x00cb;
#elif _1_04_sc
         const int OFFSET1 = 0x00ba, OFFSET2 = 0x00d4;
#endif
         instrByOffsets[OFFSET1].cdInstr.operand = 0f;
         instrByOffsets[OFFSET2].cdInstr.operand = 640;
         return instructions;
      }
      public static CodeInstructions DrawMethodOfTitle(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         // this.tex.rect = this.stage % 2 != 1 ? new Rectangle(380, (this.stage - 1) / 2 * 120, 380, 93) : new Rectangle(0, this.stage / 2 * 120, 380, 93);
         // this.tex.rect = this.stage % 2 != 1 ? new Rectangle(380, 93 + (this.stage - 1) / 2 * 120, 380, 30) : new Rectangle(0, 93 + this.stage / 2 * 120, 380, 30);
         // change 120 to 143
         // change 30 to 50
#if _1_05_en
         var offsets = new int[] { 0x001a, 0x0045, 0x00fa, 0x0128 };
#elif _1_04_sc
         var offsets = new int[] { 0x001a, 0x0045, 0x00fa, 0x0128 };
#endif
         foreach (var offset in offsets) {
            var instr = instrByOffsets[offset].cdInstr;
            instr.operand = 143; instr.opcode = OpCodes.Ldc_I4;
         }
#if _1_05_en
         offsets = new int[] { 0x0103, 0x0131 };
#elif _1_04_sc
         offsets = new int[] { 0x0103, 0x0131 };
#endif
         foreach (var offset in offsets) {
            var instr = instrByOffsets[offset].cdInstr;
            instr.operand = 50; instr.opcode = OpCodes.Ldc_I4;
         }
         return instructions;
      }
      static readonly string EndString = "Xong";
#if _1_05_en
      static readonly int EndStringOffset = 0x242;
#elif _1_04_sc
      static readonly int EndStringOffset = 0x243;
#endif
      public static CodeInstructions RecordSaveConstructor(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         instrByOffsets[EndStringOffset].cdInstr.operand = EndString;
         return instructions;
      }
      public static CodeInstructions ReplaySaveConstructor1(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         instrByOffsets[EndStringOffset].cdInstr.operand = EndString;
         return instructions;
      }
      public static CodeInstructions ReplaySaveConstructor2(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         instrByOffsets[EndStringOffset].cdInstr.operand = EndString;
         return instructions;
      }
      public static CodeInstructions PLAYDATAConstructor(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         // playdata.position = new Vector2(210f, 408f);
         // change 210f to 0f
#if _1_05_en
         const int OFFSET1 = 0x0061;
#elif _1_04_sc
         const int OFFSET1 = 0x0061;
#endif
         instrByOffsets[OFFSET1].cdInstr.operand = 0f;
         return instructions;
      }
      public static CodeInstructions TextureMethodOfSPECIAL(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         // this.achivrate.position = new Vector2(480f, 125f);
         // change 480f to 0f
#if _1_05_en
         const int OFFSET1 = 0x00ac;
#elif _1_04_sc
         const int OFFSET1 = 0x00b6;
#endif
         instrByOffsets[OFFSET1].cdInstr.operand = 0f;
         return instructions;
      }
      public static CodeInstructions DrawMethodOfStageclear(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         // must modify from largest offset to smallest offset
         var instrs = instructions as List<CodeInstruction>;
         {
            // s.Draw(this.tex, new Vector2(150f, 210f), new Rectangle?(new Rectangle(8, 176, 78, 19)), new Color(1f, 1f, 1f, this.a3));
            // change 8, 176, 78, 19 to values of bombTuple
            var bombTuple = new int[] { 8, 176, 121, 22 };
#if _1_05_en
            var offsets = new int[] { 0x037c, 0x037d, 0x0382, 0x0384 };
            const int OFFSET1 = 0x036d, OFFSET2 = 0x03aa;
#elif _1_04_sc
            var offsets = new int[] { 0x0388, 0x0389, 0x038E, 0x0390 };
            const int OFFSET1 = 0x0379, OFFSET2 = 0x03B6;
#endif
            for (var i = 0; i < offsets.Length; i++) {
               var instr = instrByOffsets[offsets[i]].cdInstr;
               instr.operand = bombTuple[i];
               instr.opcode = OpCodes.Ldc_I4;
            }
            // position
            // change 150f to 219f
            instrByOffsets[OFFSET1].cdInstr.operand = 219f; // x

            // origin
            // change s.Draw to another overloaded method
            // add more arguments
            var target = instrByOffsets[OFFSET2];
            instrs.InsertRange(target.index, new CodeInstruction[] {
               new CodeInstruction(OpCodes.Ldc_R4, 0f),
               new CodeInstruction(OpCodes.Ldc_R4, (float)(bombTuple[2] - 1)),
               new CodeInstruction(OpCodes.Ldc_R4, 2f),
               new CodeInstruction(OpCodes.Newobj, Vector2C),
               new CodeInstruction(OpCodes.Ldc_R4, 1f),
               new CodeInstruction(OpCodes.Ldc_I4_0),
               new CodeInstruction(OpCodes.Ldc_R4, 0.0f),
            });
            target.cdInstr.operand = SBDrawM;
            // final code:
            // s.Draw(this.tex, new Vector2(219f, 210f), new Rectangle?(new Rectangle(8, 176, 121, 22)), new Color(1f, 1f, 1f, this.a3), 0f, new Vector2(bombTuple[2] - 1, 2f), 1f, SpriteEffects.None, 0.0f);
         }
         {
            // s.Draw(this.tex, new Vector2(150f, 185f), new Rectangle?(new Rectangle(8, 154, 78, 19)), new Color(1f, 1f, 1f, this.a2));
            // change 8, 154, 78, 19 to values of lostTuple
            var lostTuple = new int[] { 8, 154, 121, 22 };
#if _1_05_en
            var offsets = new int[] { 0x0081, 0x0082, 0x0087, 0x0089 };
            const int OFFSET1 = 0x0072, OFFSET2 = 0x00af;
#elif _1_04_sc
            var offsets = new int[] { 0x0081, 0x0082, 0x0087, 0x0089 };
            const int OFFSET1 = 0x0072, OFFSET2 = 0x00af;
#endif
            for (var i = 0; i < offsets.Length; i++) {
               var instr = instrByOffsets[offsets[i]].cdInstr;
               instr.operand = lostTuple[i];
               instr.opcode = OpCodes.Ldc_I4;
            }
            // position
            // change 150f to 219f
            instrByOffsets[OFFSET1].cdInstr.operand = 219f; // x

            // origin
            // change s.Draw to another overloaded method
            // add more arguments
            var target = instrByOffsets[OFFSET2];
            instrs.InsertRange(target.index, new CodeInstruction[] {
               new CodeInstruction(OpCodes.Ldc_R4, 0f),
               new CodeInstruction(OpCodes.Ldc_R4, (float)(lostTuple[2] - 1)),
               new CodeInstruction(OpCodes.Ldc_R4, 2f),
               new CodeInstruction(OpCodes.Newobj, Vector2C),
               new CodeInstruction(OpCodes.Ldc_R4, 1f),
               new CodeInstruction(OpCodes.Ldc_I4_0),
               new CodeInstruction(OpCodes.Ldc_R4, 0.0f),
            });
            target.cdInstr.operand = SBDrawM;
            // final code:
            // s.Draw(this.tex, new Vector2(150f, 185f), new Rectangle?(new Rectangle(8, 154, 78, 19)), new Color(1f, 1f, 1f, this.a2), 0f, new Vector2(lostTuple[2] - 1, 2f), 1f, SpriteEffects.None, 0.0f);
         }
         return instructions;
      }
      public static CodeInstructions DrawMethodOfBonus(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         var instrs = instructions as List<CodeInstruction>;
         // sourceRectangle
         var timeTuple = new int[] { 228, 106, 137, 21 };
         // s.Draw(this.tex, new Vector2(159f, this.y3), new Rectangle?(new Rectangle(255, 109, 76, 16)), new Color(1f, 1f, 1f, this.a3), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
         // change 255, 109, 76, 16 to values of timeTuple
#if _1_05_en
         var offsets = new int[] { 0x0222, 0x0227, 0x0229, 0x022b };
         const int OFFSET1 = 0x0212, OFFSET2 = 0x0256;
#elif _1_04_sc
         var offsets = new int[] { 0x021C, 0x0221, 0x0223, 0x0225 };
         const int OFFSET1 = 0x020C, OFFSET2 = 0x0250;
#endif
         for (var i = 0; i < offsets.Length; i++) {
            var instr = instrByOffsets[offsets[i]].cdInstr;
            instr.operand = timeTuple[i];
            instr.opcode = OpCodes.Ldc_I4;
         }
         // position
         // change 159f to 219f
         instrByOffsets[OFFSET1].cdInstr.operand = 219f; // x
         // origin
         // change Vector2.Zero to new Vector2(timeTuple[2] - 1, 2f);
         var target = instrByOffsets[OFFSET2];
         instrs.InsertRange(target.index + 1, new CodeInstruction[] {
            new CodeInstruction(OpCodes.Ldc_R4, (float)(timeTuple[2] - 1)),
            new CodeInstruction(OpCodes.Ldc_R4, 2f),
            new CodeInstruction(OpCodes.Newobj, Vector2C),
         });
         instrs.RemoveAt(target.index);
         return instrs;
      }
      public static CodeInstructions InitMethodOfGameTranspiler(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
#if _1_05_en
         const int OFFSET1 = 0x00b3, OFFSET2 = 0x00b8;
#elif _1_04_sc
         const int OFFSET1 = 0x00b8, OFFSET2 = 0x00bd;
#endif
         // this.gr.bless.position = new Vector2(450f, 420f); // change all arguments to zero
         instrByOffsets[OFFSET1].cdInstr.operand = 0f;
         instrByOffsets[OFFSET2].cdInstr.operand = 0f;
         return instructions;
      }
      public static CodeInstructions LoadMethodOfEntranceTranspiler(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
#if _1_05_en
         const int OFFSET1 = 0x077d, OFFSET2 = 0x07b2, OFFSET3 = 0x07d0, OFFSET4 = 0x07e0;
#elif _1_04_sc
         const int OFFSET1 = 0x076a, OFFSET2 = 0x07a9, OFFSET3 = 0x07c8, OFFSET4 = 0x07d8;
#endif
         // string[] strArray1 = new string[4]; // change array size to 5
         instrByOffsets[OFFSET1].cdInstr.opcode = OpCodes.Ldc_I4_5;
         // string[] strArray2 = new string[4]; // change array size to 5
         instrByOffsets[OFFSET2].cdInstr.opcode = OpCodes.Ldc_I4_5;
         // for (int index2 = 0; index2 < 4; ++index2) // change iteration number from 4 to 5
         instrByOffsets[OFFSET3].cdInstr.opcode = OpCodes.Ldc_I4_5;
         // strArray1 = new string[4]; // change array size to 5
         instrByOffsets[OFFSET4].cdInstr.opcode = OpCodes.Ldc_I4_5;
         return instructions;
      }
      public static CodeInstructions DrawMethodOfEntranceTranspiler(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
#if _1_05_en
         const int OFFSET1 = 0x0053, OFFSET2 = 0x0058, OFFSET3 = 0x0e11;
#elif _1_04_sc
         const int OFFSET1 = 0x004a, OFFSET2 = 0x004f, OFFSET3 = 0x0e20;
#endif
         // s.Draw(this.bless, new Vector2(450f, 420f), new Color(1f, 1f, 1f, this.fade)); // change Vector2 argument to zero
         instrByOffsets[OFFSET1].cdInstr.operand = 0f;
         instrByOffsets[OFFSET2].cdInstr.operand = 0f;
         // for (int index = 0; index < 4; ++index) // change iteration number from 4 to 5
         instrByOffsets[OFFSET3].cdInstr.opcode = OpCodes.Ldc_I4_5;
         return instructions;
      }
      public static CodeInstructions AddTexTranspiler(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
#if _1_05_en
         const int OFFSET1 = 0x002e;
#elif _1_04_sc
         const int OFFSET1 = 0x002e;
#endif
         // this.sizef = SpriteFontX.tempGr.MeasureString(str, this.Font, PointF.Empty, StringFormat.GenericTypographic); // change MeasureString call to our customized MeasureString
         var myMeasureStringMethod = AccessTools.Method(typeof(Transpilers), nameof(MeasureString));
         var targetInstr = instrByOffsets[OFFSET1];
         targetInstr.cdInstr.opcode = OpCodes.Call;
         targetInstr.cdInstr.operand = myMeasureStringMethod;
         return instructions;
      }
      // Graphics.MeasureString's accuracy sucks for space, why StarX hasn't noticed this, I think because it doesn't matter for chinese characters
      public static SizeF MeasureString(Graphics _this, string text, Font font, System.Drawing.PointF origin, StringFormat stringFormat) {
         if (text[0] == ' ') text = " \u200D"; // ZERO-WIDTH JOINER SPACE, trick MeasureString into thinking that text has no trailing space
         return _this.MeasureString(text, font, origin, stringFormat);
      }
      // the line-break algorithm in achievement screen sucks, so I redirect it to the line-break method below
      public static CodeInstructions DrawMethodOfSPECIALTranspiler(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
#if _1_04_sc
         instructions = Transpilers.DrawMethodOfSPECIAL(instructions, instrByOffsets);
#endif
         var InsertLineBreakMethod = AccessTools.Method(typeof(Transpilers), nameof(InsertLineBreak));
         var instrs = instructions as List<CodeInstruction>;

         var challengeScorePos = new { X1 = 431f, X2 = 430f };
#if _1_04_sc
         instrByOffsets[0x04e3].cdInstr.operand = challengeScorePos.X1;
         instrByOffsets[0x051a].cdInstr.operand = challengeScorePos.X2;
         instrByOffsets[0x065E].cdInstr.operand = challengeScorePos.X1;
         instrByOffsets[0x0695].cdInstr.operand = challengeScorePos.X2;
#elif _1_05_en
         instrByOffsets[0x04e4].cdInstr.operand = challengeScorePos.X1;
         instrByOffsets[0x051b].cdInstr.operand = challengeScorePos.X2;
         instrByOffsets[0x065F].cdInstr.operand = challengeScorePos.X1;
         instrByOffsets[0x0696].cdInstr.operand = challengeScorePos.X2;
#endif

#if _1_05_en
         // if ((double) Main.dfont.MeasureString(str).X > 450.0)
         // here's how I patch it:
         /* 
         * prepare the index of str local variable (str_local_index)
         * use it to construct some instructions equivalent to: InsertLineBreak(ref str)
         *  - at the end of the above instructions, I push "" to stack, so "" will fall into Main.dfont.MeasureString(str).X > 450.0 => Main.dfont.MeasureString("").X > 450.0, by that, I skip over the "bad" line-break algorithm
         *  after all of this, str will be line-broken properly
         *  remove the old str ldloc.s
         *  insert my newly created instructions
         *  the final code would be: 
         *    InsertLineBreak(ref str)
         *    if ((double) Main.dfont.MeasureString("").X > 450.0)
         */
         var targetIndex = instrByOffsets[0x0ee7].index;
         var str_local_index = (short)0x15;
         var insertedInstructions = new List<CodeInstruction>() {
            new CodeInstruction(OpCodes.Ldloca, str_local_index),
            new CodeInstruction(OpCodes.Ldc_R4, 450f),
            new CodeInstruction(OpCodes.Call, InsertLineBreakMethod),
            new CodeInstruction(OpCodes.Ldstr, ""),
         };
         instrs.RemoveAt(targetIndex);
         instrs.InsertRange(targetIndex, insertedInstructions);
#elif _1_04_sc
         // Main.dfont.Draw(s, strArray[1], new Vector2(136f, (float) (186 + 60 * index2 + 1)), Vector2.Zero, new Vector2(0.82f, 0.82f), new Color(0.0f, 0.0f, 0.0f, this.wordalpha));
         // in version 1.04, thing got a bit difference, there isn't any line-break algorithm here, strArray[1] is printed out directly!
         // explaination:
         /*
          * create new instructions:
          *    load strArray by arr_local_index
          *    load element 1 ref of strArray
          *    call InsertLineBreak(ref strArray[1])
          * insert these instructions before Main.dfont.Draw then adjust the labels
          */
         var targetInstr = instrByOffsets[0x0ec9];
         var targetIndex = targetInstr.index;
         var arr_local_index = (short)0x10;
         var insertedInstructions = new List<CodeInstruction>() {
            new CodeInstruction(OpCodes.Ldloc, arr_local_index),
            new CodeInstruction(OpCodes.Ldc_I4_1),
            new CodeInstruction(OpCodes.Ldelema, StringT),
            new CodeInstruction(OpCodes.Ldc_R4, 450f),
            new CodeInstruction(OpCodes.Call, InsertLineBreakMethod),
         };
         var targetLabels = targetInstr.cdInstr.labels;
         targetInstr.cdInstr.labels = new List<Label>();
         instrs.InsertRange(targetIndex, insertedInstructions);
         instrs[targetIndex].labels = targetLabels;

         var target2 = instrByOffsets[0x08d2];
         instrs.InsertRange(target2.index, new CodeInstruction[] {
            new CodeInstruction(OpCodes.Ldsfld, dfontF),
            new CodeInstruction(OpCodes.Ldflda, SpacingF),
            new CodeInstruction(OpCodes.Ldc_R4, 0f),
            new CodeInstruction(OpCodes.Stfld, Vector2_XF),
         });

         var target3 = instrByOffsets[0x0169];
         instrs.InsertRange(target3.index, new CodeInstruction[] {
            new CodeInstruction(OpCodes.Ldsfld, dfontF),
            new CodeInstruction(OpCodes.Ldflda, SpacingF),
            new CodeInstruction(OpCodes.Ldc_R4, -1f),
            new CodeInstruction(OpCodes.Stfld, Vector2_XF),
         });
#endif
         return instrs;
      }
      // actually this is just the better line-break method in the game itself, used in character dialogue
      public static void InsertLineBreak(ref string str, float width) {
         var stringList = new List<string>();
         var source = new List<char>();
         foreach (var ch in str) {
            source.Add(ch);
            if (dfont.MeasureString(source.ToArray()).X >= width) {
               int num = source.LastIndexOf(' ');
               List<char> charList = num != -1 ? source.Take(num + 1).ToList() : source;
               stringList.Add(string.Join("", charList));
               source.RemoveRange(0, charList.Count);
            }
         }
         stringList.Add(string.Join<char>("", source));
         str = string.Join("\r\n", stringList);
      }
      /*
       * Move ui drawing instructions before character drawing call
       * Move grazebox drawing instructions before character drawing call
       * Move enemy indicator drawing instruction befor character drawing call
       * TODO: viết hướng dẫn chính xác và tường minh cho chỗ này
       */
      public static CodeInstructions SDrawMethodOfGameTranspiler(CodeInstructions instructions, ILGenerator adderIL, CodeInstructionMap instrByOffsets) {
         // must modify from largest offset to smallest offset
         var instrs = instructions as List<CodeInstruction>;
#if _1_05_en
         const int OFFSET1 = 0x0507, OFFSET3 = 0x03b3, OFFSET4 = 0x0385, OFFSET5 = 0x02ce, OFFSET6 = 0x0535, OFFSET7 = 0x0618;
#elif _1_04_sc
         const int OFFSET1 = 0x05B3, OFFSET3 = 0x045F, OFFSET4 = 0x0431, OFFSET5 = 0x037E, OFFSET6 = 0x05E1, OFFSET7 = 0x06C4;
#endif
         var DrEM = AccessTools.Method(typeof(Transpilers), nameof(DrawEnemyIndicator));


         // if (this.stm.IsBossed()) [the final IsBossed check]
         var enemyIndicatorTarget = instrByOffsets[OFFSET6]; // [2] for deleting all of what in this scope
         var enemyIndicatorTargetEnd = instrByOffsets[OFFSET7];

         // this.gr.ui.Draw(s, SpriteEffects.None, 0f); 
         var uiDrawTarget = instrByOffsets[OFFSET1]; // [1] for moving

         // this.Special.ChangeAlpha(this.Actor.Position()); 
         // this.Special.Draw(s);
         var grazeboxDrawTarget = instrByOffsets[OFFSET3]; // [3] for moving

         // if (this.Drawevents != null) [we concern about the second Drawevents checking, not the first one]
         var testDraweventsTarget = instrByOffsets[OFFSET4]; // [4] for modifying

         // if (!this.Pause)
         var testPauseTarget = instrByOffsets[OFFSET5]; // [5] for modifying

         //-----------COPYING------------

         // copy [1] to [31c] (copied ui drawing instructions)
         var uiDrawAllIsts = instrs.Skip(uiDrawTarget.index).Take(7).Select(instr => instr.Clone()).ToList();

         // copy another [1] to [1cc] (another copied ui drawing instructions)
         var uiDrawIsts = uiDrawAllIsts.Select(instr => instr.Clone()).ToList();

         // copy [3] to [3c] (copied grazebox drawing instructions)
         var grazeboxDrawIsts = instrs.Skip(grazeboxDrawTarget.index).Take(10).Select(instr => instr.Clone()).ToList();

         // [31c] = [3c] + [1c] (copied ui and grazebox drawing instructions), we don't use [3c] directly
         uiDrawAllIsts.AddRange(grazeboxDrawIsts);
         uiDrawAllIsts.AddRange(new CodeInstruction[] {
            new CodeInstruction(OpCodes.Ldarg_0),
            new CodeInstruction(OpCodes.Ldarg_1),
            new CodeInstruction(OpCodes.Call, DrEM),
         });

         // copy [5] to [5c] (copied Pause flag checking instructions)
         var ifPauseIsts = instrs.Skip(testPauseTarget.index).Take(3).Select(instr => instr.Clone()).ToList();

         //---------REMOVING----------
         // correct label
         instrs[enemyIndicatorTargetEnd.index + 1].labels = new List<Label>();
         // remove [2]
         instrs.RemoveRange(enemyIndicatorTarget.index, enemyIndicatorTargetEnd.index - enemyIndicatorTarget.index + 1);

         // nopify [1] (remove old ui drawing instructions)
         uiDrawTarget.cdInstr.opcode = OpCodes.Nop;
         instrs.RemoveRange(uiDrawTarget.index + 1, uiDrawIsts.Count - 1);
         uiDrawIsts.AddRange(new CodeInstruction[] {
            new CodeInstruction(OpCodes.Ldarg_0),
            new CodeInstruction(OpCodes.Ldarg_1),
            new CodeInstruction(OpCodes.Call, DrEM),
         });

         // notify [3] (remove old grazebox drawing instructions)
         grazeboxDrawTarget.cdInstr.opcode = OpCodes.Nop;
         instrs.RemoveRange(grazeboxDrawTarget.index + 1, grazeboxDrawIsts.Count - 1);


         //---------ADDING------------
         // move [4]'s label to [31c] (adjust label)
         uiDrawAllIsts[0].labels = testDraweventsTarget.cdInstr.labels;
         testDraweventsTarget.cdInstr.labels = new List<Label>();

         // insert [31c] (new ui and grazebox drawing instruction) before [5] 
         instrs.InsertRange(testDraweventsTarget.index, uiDrawAllIsts);


         // modify [5c] Brtrue to Brfalse
         ifPauseIsts[2].opcode = OpCodes.Brfalse;

         // move [5]'s label to [5c] (adjust label)
         ifPauseIsts[0].labels = testPauseTarget.cdInstr.labels;
         testPauseTarget.cdInstr.labels = new List<Label>();

         // create new label (L) target for [5c]
         var label = adderIL.DefineLabel();
         ifPauseIsts[2].operand = label;


         // insert [5c] before [5]
         instrs.InsertRange(testPauseTarget.index, ifPauseIsts);

         // add (L) (new label) to [5]
         instrs[testPauseTarget.index + ifPauseIsts.Count].labels.Add(label);

         // insert [1cc] before [5]
         instrs.InsertRange(testPauseTarget.index + ifPauseIsts.Count, uiDrawIsts);

         return instrs;
      }
      static readonly dynamic origin = new Vector2(45f, 25f);
      static readonly dynamic none = new EnumConverter(SpriteEffectsT).ConvertFromString("None");
      static readonly dynamic srcRec = new XnaRectangle?(new XnaRectangle(280, 15, 89, 25));
      static void DrawEnemyIndicator(THMHJ.Game __instance, SpriteBatch spriteBatch) {
         var _this = __instance.AsDynamic();
         if (_this.stm.IsBossed()) {
            float x = _this.stm.GetBosspos().X;
            float a = 0.0f;
            if (x >= 50.0f && x <= 390.0f) a = 1f;
            if (x < 50.0f) a = x >= 0.0f ? x / 50f : 0.0f;
            else if (x > 390.0f) a = x <= 440.0f ? (x - 390.0f) / 50.0f : 0.0f;
            Vector2 position = new Vector2(x, 482f);
            var color = new XnaColor(1f, 1f, 1f, a);
            spriteBatch.Draw(_this.gr.bosslist.RealObject, position, srcRec, color, 0.0f, origin, 1f, none, 0.0f);
         }
      }
   }
}
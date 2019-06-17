using Harmony;
using System;
using System.Collections.Generic;
using static Types;
using static Assemblies;
using static Constructors;
using static Methods;
using static Launcher.Helper;
using static Launcher.Launcher;
using System.Reflection.Emit;
using System.Drawing;
using System.Linq;
using System.Diagnostics;

namespace DotnetPatching {
   using TranspilerTuple = ValueTuple<Type, string, Type[], Type, string, Type[]>;
   using CodeInstructions = IEnumerable<CodeInstruction>;
   using CodeInstructionMap = IDictionary<int, CodeInstructionWrapper>;
   class Transpilers {
      public static List<TranspilerTuple> OnSetup() {
         return new List<TranspilerTuple>() {
            // 5 lines of description in music room
            (EntranceT, "Load", null, typeof(Transpilers), nameof(LoadMethodOfEntranceTranspiler), null),
            // bless image fullscreen
            (GameT, "Init", null, typeof(Transpilers), nameof(InitMethodOfGameTranspiler), null),
            // bless image fullscreen
            // 5 lines of description in music room, align for more space
            (EntranceT, "Draw", null, typeof(Transpilers), nameof(DrawMethodOfEntranceTranspiler), null),
            // The MeasureString method is horrible for trailing space character (bigger width than real width)
            (SpriteFontXT, "addTex", null, typeof(Transpilers), nameof(AddTexTranspiler), null),
            // Fix The Line Break Algorithm in Achievement Screen
            (SPECIALT, "Draw", null, typeof(Transpilers), nameof(DrawMethodOfSPECIALTranspiler), null),
            // Make the character's portraits on top of ui frame
            (GameT, "SDraw", null, typeof(Transpilers), nameof(SDrawMethodOfGameTranspiler), null),
            // BossList.xna hack to enlarge canvases
            (BonusT, "Draw", null, typeof(Transpilers), nameof(DrawMethodOfBonus), null),
            (StageclearT, "Draw", null, typeof(Transpilers), nameof(DrawMethodOfStageclear), null),
            // Achivrate position and sourceRectangle
            (SPECIALT, "Texture", null, typeof(Transpilers), nameof(TextureMethodOfSPECIAL), null),
            // Playdata position and sourceRectangle
            (PLAYDATAT, ".ctor", TypeL(Texture2DT, SpriteT, SpriteT, SpriteT, SpriteT), typeof(Transpilers), nameof(PLAYDATAConstructor), null),
            // Translate 终 in Replay Saving Screen and Record Saving Screen
            (RecordSaveT, ".ctor", TypeL(PlayDataT, IntT, LongT), typeof(Transpilers), nameof(RecordSaveConstructor), null),
            (ReplaySaveT, ".ctor", TypeL(RecordManagerT, IntT, LongT, IntT, IntT, IntT), typeof(Transpilers), nameof(ReplaySaveConstructor1), null),
            (ReplaySaveT, ".ctor", TypeL(RecordManagerT, IntT, LongT, IntT, IntT, IntT, IntT), typeof(Transpilers), nameof(ReplaySaveConstructor2), null),
            // Adjust stagetitle metrics
            (TitleT, "Draw", null, typeof(Transpilers), nameof(DrawMethodOfTitle), null),
            // Adjust 31.xna metrics
            (EDT, "Draw", null, typeof(Transpilers), nameof(DrawMethodOfED), null),
         };
      }
      static CodeInstructions DrawMethodOfED(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         // s.Draw(this.text, new Vector2(60f, (float) (175 + 38 * index)), new Rectangle?(new Rectangle(0, 38 * index, 307, 38)), new Color(1f, 1f, 1f, this.textcolor[index]), 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
         // change 60f to 0f
         // change 307 to 640
#if   _1_05_en
         const int OFFSET1 = 0x00b1, OFFSET2 = 0x00cb;
#elif _1_04_sc
         const int OFFSET1 = 0x00ba, OFFSET2 = 0x00d4;
#endif
         instrByOffsets[OFFSET1].cdInstr.operand = 0f;
         instrByOffsets[OFFSET2].cdInstr.operand = 640;
         return instructions;
      }
      static CodeInstructions DrawMethodOfTitle(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         // this.tex.rect = this.stage % 2 != 1 ? new Rectangle(380, (this.stage - 1) / 2 * 120, 380, 93) : new Rectangle(0, this.stage / 2 * 120, 380, 93);
         // this.tex.rect = this.stage % 2 != 1 ? new Rectangle(380, 93 + (this.stage - 1) / 2 * 120, 380, 30) : new Rectangle(0, 93 + this.stage / 2 * 120, 380, 30);
         // change 120 to 143
         // change 30 to 50
#if   _1_05_en
         var offsets = new int[] { 0x001a, 0x0045, 0x00fa, 0x0128 };
#elif _1_04_sc
         var offsets = new int[] { 0x001a, 0x0045, 0x00fa, 0x0128 };
#endif
         foreach (var offset in offsets) {
            var instr = instrByOffsets[offset].cdInstr;
            instr.operand = 143; instr.opcode = OpCodes.Ldc_I4;
         }
#if   _1_05_en
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
      static CodeInstructions RecordSaveConstructor(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         instrByOffsets[EndStringOffset].cdInstr.operand = EndString;
         return instructions;
      }
      static CodeInstructions ReplaySaveConstructor1(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         instrByOffsets[EndStringOffset].cdInstr.operand = EndString;
         return instructions;
      }
      static CodeInstructions ReplaySaveConstructor2(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         instrByOffsets[EndStringOffset].cdInstr.operand = EndString;
         return instructions;
      }
      static CodeInstructions PLAYDATAConstructor(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
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
      static CodeInstructions TextureMethodOfSPECIAL(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
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
      static CodeInstructions DrawMethodOfStageclear(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
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
      static CodeInstructions DrawMethodOfBonus(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
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
      static CodeInstructions InitMethodOfGameTranspiler(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
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
      static CodeInstructions LoadMethodOfEntranceTranspiler(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
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
      static CodeInstructions DrawMethodOfEntranceTranspiler(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
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
      static CodeInstructions AddTexTranspiler(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
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
      public static SizeF MeasureString(Graphics _this, string text, Font font, PointF origin, StringFormat stringFormat) {
         if (text[0] == ' ') text = " \u200D"; // ZERO-WIDTH JOINER SPACE, trick MeasureString into thinking that text has no trailing space
         return _this.MeasureString(text, font, origin, stringFormat);
      }
      // the line-break algorithm in achievement screen sucks, so I redirect it to the line-break method below
      static CodeInstructions DrawMethodOfSPECIALTranspiler(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         var InsertLineBreakMethod = AccessTools.Method(typeof(Transpilers), nameof(InsertLineBreak));
         var instrs = instructions as List<CodeInstruction>;
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
            new CodeInstruction(OpCodes.Ldc_I4_0),
            new CodeInstruction(OpCodes.Ldelema),
            new CodeInstruction(OpCodes.Call, InsertLineBreakMethod),
         };
         var targetLabels = targetInstr.cdInstr.labels;
         targetInstr.cdInstr.labels = new List<Label>();
         instrs.InsertRange(targetIndex, insertedInstructions);
         instrs[targetIndex].labels = targetLabels;
#endif
         return instrs;
      }
      // actually this is just the better line-break method in the game itself, used in character dialogue
      public static void InsertLineBreak(ref string str) {
         var stringList = new List<string>();
         var source = new List<char>();
         dynamic dfont = Traverse.Create(MainT).Field("dfont").GetValue();
         foreach (var ch in str) {
            source.Add(ch);
            if (dfont.MeasureString(source.ToArray()).X >= 450.0) {
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
       * Move character drawing instructions up
       * Move grazebox drawing instructions up
       * TODO: viết hướng dẫn chính xác và tường minh cho chỗ này
       */
      static CodeInstructions SDrawMethodOfGameTranspiler(CodeInstructions instructions, ILGenerator adderIL, CodeInstructionMap instrByOffsets) {
         // must modify from largest offset to smallest offset
         var instrs = instructions as List<CodeInstruction>;
#if _1_05_en
         const int OFFSET1 = 0x0507, OFFSET3 = 0x03b3, OFFSET4 = 0x0385, OFFSET5 = 0x02ce;
#elif _1_04_sc
         const int OFFSET1 = 0x05B3, OFFSET3 = 0x045F, OFFSET4 = 0x0431, OFFSET5 = 0x037E;
#endif

         // this.gr.ui.Draw(s, SpriteEffects.None, 0f); 
         var characterDrawTarget = instrByOffsets[OFFSET1]; // [1] for moving

         // this.Special.ChangeAlpha(this.Actor.Position()); 
         // this.Special.Draw(s);
         var grazeboxDrawTarget = instrByOffsets[OFFSET3]; // [3] for moving

         // if (this.Drawevents != null) [we concern about the second Drawevents checking, not the first one]
         var testDraweventsTarget = instrByOffsets[OFFSET4]; // [4] for modifying

         // if (!this.Pause)
         var testPauseTarget = instrByOffsets[OFFSET5]; // [5] for modifying

         //-----------COPYING------------
         // copy [1] to [31c] (copied character drawing instructions)
         var uiDrawAllIsts = instrs.Skip(characterDrawTarget.index).Take(7).Select(instr => instr.Clone()).ToList();

         // copy another [1] to [1cc] (another copied character drawing instructions)
         var uiDrawIsts = uiDrawAllIsts.Select(instr => instr.Clone()).ToList();

         // copy [3] to [3c] (copied grazebox drawing instructions)
         var grazeboxDrawIsts = instrs.Skip(grazeboxDrawTarget.index).Take(10).Select(instr => instr.Clone()).ToList();

         // [31c] = [3c] + [1c] (copied character and grazebox drawing instructions), we don't use [3c] directly
         uiDrawAllIsts.AddRange(grazeboxDrawIsts);

         // copy [5] to [5c] (copied Pause flag checking instructions)
         var ifPauseIsts = instrs.Skip(testPauseTarget.index).Take(3).Select(instr => instr.Clone()).ToList();

         //---------REMOVING----------
         // nopify [1] (remove old character drawing instructions)
         characterDrawTarget.cdInstr.opcode = OpCodes.Nop;
         instrs.RemoveRange(characterDrawTarget.index + 1, uiDrawIsts.Count - 1);

         // notify [3] (remove old grazebox drawing instructions)
         grazeboxDrawTarget.cdInstr.opcode = OpCodes.Nop;
         instrs.RemoveRange(grazeboxDrawTarget.index + 1, grazeboxDrawIsts.Count - 1);


         //---------ADDING------------
         // move [4]'s label to [31c] (adjust label)
         uiDrawAllIsts[0].labels = testDraweventsTarget.cdInstr.labels;
         testDraweventsTarget.cdInstr.labels = new List<Label>();

         // insert [31c] (new character and grazebox drawing instruction) before [5] 
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
   }
}
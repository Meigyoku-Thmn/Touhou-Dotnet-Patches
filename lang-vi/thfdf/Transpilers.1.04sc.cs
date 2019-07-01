using HarmonyLib;
using ReflectionMagic;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using static Fields;
using static Resource;
using static RuntimePatcher.Helper;
using static Types;
using static Delegates;

namespace DotnetPatching {
   using CodeInstructionMap = IDictionary<int, CodeInstructionWrapper>;
   using CodeInstructions = IEnumerable<CodeInstruction>;
   using TranspilerTuple = ValueTuple<Type, string, Type[], Type, string, Type[]>;
   class Transpilers_1_04_sc {
      public static List<TranspilerTuple> OnSetup() {
         return new List<TranspilerTuple>() {
            (BoardT, ".ctor", TypeL(Texture2DT, StringT), typeof(Transpilers_1_04_sc), nameof(BoardConstructor), null),
            (BlackHoleAchiveT, "Check", null, typeof(Transpilers_1_04_sc), nameof(BlackHoleAchiveCheck), null),
            (MissAtAllAchiveT, "Check", null, typeof(Transpilers_1_04_sc), nameof(MissAtAllAchiveCheck), null),
            (PotentialAchiveT, "Check", null, typeof(Transpilers_1_04_sc), nameof(PotentialAchiveCheck), null),
            (NothingLeftAchiveT, "Check", null, typeof(Transpilers_1_04_sc), nameof(NothingLeftAchiveCheck), null),
            (SurvivalAchiveT, "Check", null, typeof(Transpilers_1_04_sc), nameof(SurvivalAchiveCheck), null),
            (DialogT, "Draw", null, typeof(Transpilers_1_04_sc), nameof(DrawMethodOfDialog), null),
            (CardDisplayT, "Update", null, typeof(Transpilers_1_04_sc), nameof(UpdateOfCardDisplay), null),
         };
      }
      static CodeInstructions UpdateOfCardDisplay(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         instrByOffsets[0x0082].cdInstr.operand = 400f;
         return instructions;
      }
      static CodeInstructions BoardConstructor(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         // board metrics
         instrByOffsets[0x0015].cdInstr.operand = 98f;
         return instructions;
      }
      internal static CodeInstructions DrawMethodOfSPECIAL(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         instrByOffsets[0x0F78].cdInstr.operand = ACHIEVED_DIFFICULTY_LEVEL;
         instrByOffsets[0x075C].cdInstr.operand = NEED_UNLOCK_ALL_SPELLS;
         instrByOffsets[0x0796].cdInstr.operand = NEED_UNLOCK_ALL_SPELLS;
         instrByOffsets[0x07D3].cdInstr.operand = NEED_TO_PASS_THIS_STAGE;
         instrByOffsets[0x080D].cdInstr.operand = NEED_TO_PASS_THIS_STAGE;
         return instructions;
      }
      static CodeInstructions BlackHoleAchiveCheck(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         instrByOffsets[0x0010].cdInstr.operand = BLACK_HOLE_HEART;
         instrByOffsets[0x002C].cdInstr.operand = BLACK_HOLE_CATACLYSM;
         return instructions;
      }
      static CodeInstructions MissAtAllAchiveCheck(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         instrByOffsets[0x0010].cdInstr.operand = ICE_SIGN_NO_MISS;
         return instructions;
      }
      static CodeInstructions PotentialAchiveCheck(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         instrByOffsets[0x0010].cdInstr.operand = AMERGADDON;
         return instructions;
      }
      static CodeInstructions NothingLeftAchiveCheck(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         instrByOffsets[0x0010].cdInstr.operand = MURDER_INTENTION;
         return instructions;
      }
      static CodeInstructions SurvivalAchiveCheck(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         instrByOffsets[0x0010].cdInstr.operand = KILLING_VORTEX;
         return instructions;
      }
      static CodeInstructions DrawMethodOfDialog(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         var begin = instrByOffsets[0x02F8];
         var end = instrByOffsets[0x0425];
         var instrs = instructions as List<CodeInstruction>;
         instrs.RemoveRange(begin.index, end.index - begin.index + 1);
         var drawDialogM = AccessTools.Method(typeof(Transpilers_1_04_sc), nameof(DrawDialog));
         instrs.InsertRange(begin.index, new CodeInstruction[] {
            new CodeInstruction(OpCodes.Ldarg_0),
            new CodeInstruction(OpCodes.Ldarg_1),
            new CodeInstruction(OpCodes.Call, drawDialogM),
         });
         instrByOffsets[0x042A].cdInstr.labels = new List<Label>();
         return instrs;
      }
      static dynamic shadowPosition = NewVector2(null, 55f, 334f);
      static dynamic position = NewVector2(null, 54f, 333f);
      static void DrawDialog(object __instance, dynamic spriteBatch) {
         var _this = __instance.AsDynamic();
         var text = ((string)(_this.n)).Split(']')[1];
         Transpilers.InsertLineBreak(ref text, 340f);
         dynamic shadowColor = NewColor(null, 0f, 0f, 0f, _this.dalpha);
         dfont.Draw(spriteBatch, text, shadowPosition, shadowColor);
         if ((int)_this.now2 == -1) {
            dynamic color = NewColor(null, 0f, 1f, 0f, _this.dalpha);
            dfont.Draw(spriteBatch, text, position, color);
         }
         else {
            dynamic color = NewColor(null, 1f, 0f, 0f, _this.dalpha);
            dfont.Draw(spriteBatch, text, position, color);
         }
      }
   }
}
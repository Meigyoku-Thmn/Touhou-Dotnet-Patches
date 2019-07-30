using HarmonyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReflectionMagic;
using System.Collections.Generic;
using System.Reflection.Emit;
using static Constructors;
using static Methods;
using static Resource;

namespace DotnetPatching {
   using System.Diagnostics;
   using static RuntimePatcher.PatchInfo;
   using CodeInstructionMap = IDictionary<int, CodeInstructionWrapper>;
   using CodeInstructions = IEnumerable<CodeInstruction>;
   using TranspilerTuple = RuntimePatcher.PatchInfo;
   partial class Transpilers {
      public static List<TranspilerTuple> OnSetup_1_04_sc() {
         return new List<TranspilerTuple>() {
            PM(BoardC, BoardConstructorH),
            PM(BlackHoleAchiveCheckM, BlackHoleAchiveCheckH),
            PM(MissAtAllAchiveM, MissAtAllAchiveCheckH),
            PM(PotentialAchiveM, PotentialAchiveCheckH),
            PM(NothingLeftAchiveM, NothingLeftAchiveCheckH),
            PM(SurvivalAchiveM, SurvivalAchiveCheckH),
            PM(CardDisplayUpdateM, UpdateOfCardDisplayH),
         };
      }
      public static CodeInstructions UpdateOfCardDisplay(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         instrByOffsets[0x0082].cdInstr.operand = 400f;
         return instructions;
      }
      public static CodeInstructions BoardConstructor(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         // board metrics
         instrByOffsets[0x0015].cdInstr.operand = 98f;
         return instructions;
      }
      public static CodeInstructions DrawMethodOfSPECIAL(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         instrByOffsets[0x0F78].cdInstr.operand = ACHIEVED_DIFFICULTY_LEVEL;
         instrByOffsets[0x075C].cdInstr.operand = NEED_UNLOCK_ALL_SPELLS;
         instrByOffsets[0x0796].cdInstr.operand = NEED_UNLOCK_ALL_SPELLS;
         instrByOffsets[0x07D3].cdInstr.operand = NEED_TO_PASS_THIS_STAGE;
         instrByOffsets[0x080D].cdInstr.operand = NEED_TO_PASS_THIS_STAGE;
         return instructions;
      }
      public static CodeInstructions BlackHoleAchiveCheck(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         instrByOffsets[0x0010].cdInstr.operand = BLACK_HOLE_HEART;
         instrByOffsets[0x002C].cdInstr.operand = BLACK_HOLE_CATACLYSM;
         return instructions;
      }
      public static CodeInstructions MissAtAllAchiveCheck(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         instrByOffsets[0x0010].cdInstr.operand = ICE_SIGN_NO_MISS;
         return instructions;
      }
      public static CodeInstructions PotentialAchiveCheck(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         instrByOffsets[0x0010].cdInstr.operand = AMERGADDON;
         return instructions;
      }
      public static CodeInstructions NothingLeftAchiveCheck(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         instrByOffsets[0x0010].cdInstr.operand = MURDER_INTENTION;
         return instructions;
      }
      public static CodeInstructions SurvivalAchiveCheck(CodeInstructions instructions, CodeInstructionMap instrByOffsets) {
         instrByOffsets[0x0010].cdInstr.operand = KILLING_VORTEX;
         return instructions;
      }
      static dynamic shadowPosition = new Vector2(55f, 334f);
      static dynamic position = new Vector2(54f, 333f);
      public static void DrawDialog(object __instance, SpriteBatch spriteBatch) {
         var _this = __instance.AsDynamic();
         var text = ((string)(_this.n)).Split(']')[1];
         InsertLineBreak(ref text, 340f);
         Color shadowColor = new Color(0f, 0f, 0f, _this.dalpha);
         dfont.Draw(spriteBatch, text, shadowPosition, shadowColor);
         var alterTextCfg = Resource.Config.AlterTextCfg;
         var alterFontCfg = alterTextCfg.AlterFontCfg;
         var protagonistLineColor = alterFontCfg.ProtagonistLineColor;
         var antagonistLineColor = alterFontCfg.AntagonistLineColor;
         var proColor = protagonistLineColor.ColorI;
         var antColor = antagonistLineColor.ColorI;
         if ((int)_this.now2 == -1) {
            Color color;
            if (alterTextCfg.Enabled && alterFontCfg.Enabled && protagonistLineColor.Enabled)
               color = new Color(proColor.R, proColor.G, proColor.B, (byte)(_this.dalpha * 255));
            else color = new Color(0f, 1f, 0f, _this.dalpha);
            dfont.Draw(spriteBatch, text, position, color);
         }
         else {
            Color color;
            if (alterTextCfg.Enabled && alterFontCfg.Enabled && antagonistLineColor.Enabled)
               color = new Color(antColor.R, antColor.G, antColor.B, (byte)(_this.dalpha * 255));
            else color = new Color(1f, 0f, 0f, _this.dalpha);
            dfont.Draw(spriteBatch, text, position, color);
         }
      }
   }
}
﻿using ReflectionMagic;
using RuntimePatcher;
using System;
using System.Collections.Generic;
using THMHJ;
using static Delegates;
using static Methods;
using static RuntimePatcher.Launcher;
using static Types;

namespace DotnetPatching {
   using static PatchInfo;
   using PatchTuple = PatchInfo;
   partial class Detours {
      public static List<PatchTuple> OnSetup_1_04_sc() {
         return new List<PatchTuple>() {
            PM(AchiveManInitializeM, AchieveInitializeH),
         };
      }
      private static string[][] achivClassNames = new string[][] {
         new string[] { "感谢支持", "初次见面", "一周目", "全机体通关", "全难度通关", "幻想乡制霸", },
         new string[] { "见证银河的消失", "百发不中", "底力爆发", "恶魔的疯狂", "廿亿分突破", "无计可施", "毫发无伤", "绝好的时机", "轻飘飘", "条件反射", "不惧风雨", "风暴幸存者", "炸平幻想乡", "自信满满", "满载而归", "挑战符卡全收集", },
         new string[] { "可怕的毅力", "不屈不挠", "走投无路", "一个也不落", "擦弹有快感", "弹幕痴狂", "音乐就是生命", "铁杵磨成针", },
      };
      public static void AchieveInitialize(AchievementManager __instance) {
         dynamic _this = __instance.AsDynamic();
         _this.achivs = Array.CreateInstance(AchievementBaseT, 30);
         dynamic achivs = _this.achivs.RealObject;
         dynamic achivName = R_AchivName();
         dynamic achivIndex = R_AchivIndex();
         for (int i = 0; i < achivName.Length; i++) {
            for (int j = 0; j < achivName[i].Length; j++) {
               if (achivIndex[i] + j < achivs.Length) {
                  achivs[achivIndex[i] + j] =
                     (dynamic)TargetAssembly.CreateInstance("THMHJ.Achievements." + achivClassNames[i][j]);
               }
            }
         }
         dynamic specialData = _this.LoadSpecialData().RealObject;
         for (int k = 0; k < specialData.ach.Length; k++) {
            for (int l = 0; l < specialData.ach[k].achiv.Length; l++) {
               achivs[achivIndex[k] + l].get = specialData.ach[k].achiv[l].get;
               for (int m = 0; m < achivs[achivIndex[k] + l].finishedlevel.Length; m++) {
                  achivs[achivIndex[k] + l].finishedlevel[m] = specialData.ach[k].achiv[l].level[m];
               }
            }
         }
      }
   }
}

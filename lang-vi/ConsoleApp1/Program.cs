using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace ConsoleApp1 {
   class Program {
      public void Draw(Program sb, char[] str, Point position, Point maxBound, Point scale, Color color) {

      }
      public Program a;
      static void XDraw(Program __instance, Program sb, char[] str, Point position, Point maxBound, Point scale, Color color) {
         __instance.a = __instance; // debugger cannot read __instance
         Console.WriteLine(__instance.a.a.ToString()); // crash
      }
      static bool Prefix() => false;
      static void Main(string[] args) {
         var HarmonyInst = new Harmony("Just a Test");
         var original = AccessTools.Method(typeof(Program), "Draw");
         var prefix = AccessTools.Method(typeof(Program), "Prefix");
         var postfix = AccessTools.Method(typeof(Program), "XDraw");
         HarmonyInst.Patch(original, new HarmonyMethod(prefix), new HarmonyMethod(postfix));
         new Program().Draw(new Program(), new char[0], new Point(), new Point(), new Point(), new Color());
      }
   }
}

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class Decipher
{
   public static string Key = "NYLilMS35bt1RuSa47uRvO1FCYgPVq";
   public static string Vector = "KU4Tn93FEoYca";
   private static byte[][] Keys()
   {
      return new byte[4][] {
       new byte[16] { 202, 144, 216, 211, 188, 214, 244, 180, 174, 194, 188, 101, 40, 22, 189, 183 },
       new byte[16] { 61, 88, 123, 71, 96, 236, 242, 152, 50, 42, 213, 5, 171, 156, 175, 190 },
       new byte[16] { 77, 60, 63, 80, 18, 58, 69, 119, 137, 140, 117, 143, 126, 158, 17, 135 },
       new byte[16] { 242, 165, 41, 132, 231, 150, 148, 35, 220, 80, 204, 233, 36, 174, 251, 77 }
     };
   }
   public static byte[] Encry(string FileName, int type)
   {
      byte[] inputBuffer = File.ReadAllBytes(FileName);
      TripleDESCryptoServiceProvider cryptoServiceProvider1 = new TripleDESCryptoServiceProvider();
      MD5CryptoServiceProvider cryptoServiceProvider2 = new MD5CryptoServiceProvider();
      cryptoServiceProvider1.Key = Keys()[type];
      cryptoServiceProvider1.Mode = CipherMode.ECB;
      byte[] buffer = cryptoServiceProvider1.CreateEncryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
      FileStream fileStream = type == 3 ? File.Create(Cuts(FileName, ".", 1) + ".rpy") : File.Create(Cuts(FileName, ".", 1) + ".xna");
      fileStream.Write(buffer, 0, buffer.GetLength(0));
      fileStream.Close();
      return buffer;
   }
   public static string Cuts(string word, string num, int array)
   {
      char[] charArray = num.ToCharArray();
      return word.Split(charArray)[array - 1];
   }
   public static Stream Decry(string FileName, int type)
   {
      byte[] numArray = File.ReadAllBytes(FileName);
      TripleDESCryptoServiceProvider cryptoServiceProvider1 = new TripleDESCryptoServiceProvider();
      MD5CryptoServiceProvider cryptoServiceProvider2 = new MD5CryptoServiceProvider();
      cryptoServiceProvider1.Key = Keys()[type];
      cryptoServiceProvider1.Mode = CipherMode.ECB;
      ICryptoTransform decryptor = cryptoServiceProvider1.CreateDecryptor();
      byte[] inputBuffer = numArray;
      return new MemoryStream(decryptor.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
   }
   public static Stream Decry(string FileName)
   {
      byte[] numArray = File.ReadAllBytes(FileName);
      TripleDESCryptoServiceProvider cryptoServiceProvider1 = new TripleDESCryptoServiceProvider();
      MD5CryptoServiceProvider cryptoServiceProvider2 = new MD5CryptoServiceProvider();
      cryptoServiceProvider1.Key = Keys()[0];
      cryptoServiceProvider1.Mode = CipherMode.ECB;
      ICryptoTransform decryptor = cryptoServiceProvider1.CreateDecryptor();
      byte[] inputBuffer = numArray;
      return new MemoryStream(decryptor.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
   }
   public static byte[] Encrypt(byte[] Data)
   {
      byte[] rgbKey = new byte[32];
      Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(rgbKey.Length)), rgbKey, rgbKey.Length);
      byte[] rgbIV = new byte[16];
      Array.Copy(Encoding.UTF8.GetBytes(Vector.PadRight(rgbIV.Length)), rgbIV, rgbIV.Length);
      Rijndael rijndael = Rijndael.Create();
      using (MemoryStream memoryStream = new MemoryStream())
      {
         using (CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndael.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write))
         {
            cryptoStream.Write(Data, 0, Data.Length);
            cryptoStream.FlushFinalBlock();
            return memoryStream.ToArray();
         }
      }
   }
   public static byte[] Decrypt(byte[] Data)
   {
      byte[] rgbKey = new byte[32];
      Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(rgbKey.Length)), rgbKey, rgbKey.Length);
      byte[] rgbIV = new byte[16];
      Array.Copy(Encoding.UTF8.GetBytes(Vector.PadRight(rgbIV.Length)), rgbIV, rgbIV.Length);
      Rijndael rijndael = Rijndael.Create();
      using (MemoryStream memoryStream1 = new MemoryStream(Data))
      {
         using (CryptoStream cryptoStream = new CryptoStream(memoryStream1, rijndael.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Read))
         {
            using (MemoryStream memoryStream2 = new MemoryStream())
            {
               byte[] buffer = new byte[1024];
               int count;
               while ((count = cryptoStream.Read(buffer, 0, buffer.Length)) > 0)
                  memoryStream2.Write(buffer, 0, count);
               return memoryStream2.ToArray();
            }
         }
      }
   }
}
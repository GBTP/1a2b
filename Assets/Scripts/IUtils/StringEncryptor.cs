using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace IUtils
{
    public static class StringEncryptor
    {
        private const string Key = "inokanazqr233333";
        private const string Iv = "woyaotanlianaiaa";

        public static string Encrypt(string plainText)
        {
            //if (plainText.IsInvalid()) return plainText;

            byte[] encrypted;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.ASCII.GetBytes(Key);
                aesAlg.IV = Encoding.ASCII.GetBytes(Iv);
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt, Encoding.UTF8))
                        {
                            swEncrypt.Write(plainText);
                        }

                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        public static string Decrypt(string cipherStr)
        {
            //if (cipherStr.IsInvalid()) return cipherStr;

            var cipherText = Convert.FromBase64String(cipherStr);
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException(nameof(cipherStr));

            string plaintext = null;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.ASCII.GetBytes(Key);
                aesAlg.IV = Encoding.ASCII.GetBytes(Iv);
                ICryptoTransform descriptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, descriptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt, Encoding.UTF8))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}
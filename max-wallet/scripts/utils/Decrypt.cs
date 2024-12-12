using System;
using System.IO;
using System.Security.Cryptography;

namespace MaxWallet.scripts.utils
{
    public class Decrypt
    {
        public static string DecryptWalletData(byte[] encryptedData, string password)
        {
            using (Aes aesAlg = Aes.Create())
            {
                using (var ms = new MemoryStream(encryptedData))
                {
                    byte[] salt = new byte[16];
                    ms.Read(salt, 0, salt.Length);

                    byte[] iv = new byte[16];
                    ms.Read(iv, 0, iv.Length);

                    var key = new Rfc2898DeriveBytes(password, salt, 500000);
                    aesAlg.Key = key.GetBytes(32);
                    aesAlg.IV = iv;  

                    using (var cs = new CryptoStream(ms, aesAlg.CreateDecryptor(), CryptoStreamMode.Read))
                    using (var sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }

    }
}
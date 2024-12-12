using System.IO;
using System.Security.Cryptography;

namespace MaxWallet.scripts.utils
{
    public class Encrypt
    {
        public static byte[] EncryptData(string walletData, string password)
        {
            using (Aes aesAlg = Aes.Create())
            {
                byte[] salt = new byte[16];
                RandomNumberGenerator.Fill(salt);

                var key = new Rfc2898DeriveBytes(password, salt, 500000);
                aesAlg.Key = key.GetBytes(32);
                
                aesAlg.IV = new byte[16];
                RandomNumberGenerator.Fill(aesAlg.IV);

                using (var ms = new MemoryStream())
                {
                    ms.Write(salt, 0, salt.Length);
                    ms.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                    using (var cs = new CryptoStream(ms, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(walletData);
                    }

                    return ms.ToArray();
                }
            }
        }

    }
}


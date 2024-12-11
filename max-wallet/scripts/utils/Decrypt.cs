using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MaxWallet.scripts.utils
{
    public class Decrypt
    {
        public static string DecryptWalletData(byte[] encryptedData, string password)
{
    using (var aesAlg = Aes.Create())
    {
        // Extrair o salt dos primeiros 16 bytes
        byte[] salt = new byte[16];
        Array.Copy(encryptedData, 0, salt, 0, salt.Length);

        var key = new Rfc2898DeriveBytes(password, salt, 10000);
        aesAlg.Key = key.GetBytes(32);  // 32 bytes para a chave
        aesAlg.IV = key.GetBytes(16);   // 16 bytes para o vetor de inicialização (IV)

        using (var ms = new MemoryStream(encryptedData, 16, encryptedData.Length - 16))  // Ignora os primeiros 16 bytes (salt)
        using (var cs = new CryptoStream(ms, aesAlg.CreateDecryptor(), CryptoStreamMode.Read))
        using (var sr = new StreamReader(cs))
        {
            return sr.ReadToEnd();
        }
    }
}

    }
}
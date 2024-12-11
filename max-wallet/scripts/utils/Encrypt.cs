using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MaxWallet.scripts.utils
{
    public class Encrypt
    {
        public static byte[] EncryptData(string walletData, string password)
        {
    using (Aes aesAlg = Aes.Create())
    {
        byte[] salt = new byte[16];
        RandomNumberGenerator.Fill(salt);  // Gerando o salt aleatório

        var key = new Rfc2898DeriveBytes(password, salt, 10000);
        aesAlg.Key = key.GetBytes(32);  // 32 bytes para a chave
        aesAlg.IV = key.GetBytes(16);   // 16 bytes para o vetor de inicialização (IV)

        using (var ms = new MemoryStream())
        {
            // Escreve o salt no começo do arquivo criptografado
            ms.Write(salt, 0, salt.Length);

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
using System.Text;
using System.IO;
using System.Security.Cryptography;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using static System.Net.Mime.MediaTypeNames;
using EcommerceApi.Models;

namespace EcommerceApi.Helpers
{
    public class CryptographyHelper
    {
        private readonly IConfiguration _configuration;
        private readonly CryptographyKeys _cryptographyKeys;

        public CryptographyHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _cryptographyKeys = _configuration.GetSection("CryptographyKeys").Get<CryptographyKeys>();
        }

        public string Encrypt(string text)
        {
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = StringToByteArray(_cryptographyKeys.SecretKey); //Encoding.UTF8.GetBytes(keyString
                aes.IV = StringToByteArray(_cryptographyKeys.SecretIv);

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(text);
                        }
                    }

                    array = memoryStream.ToArray();
                }
            }

            return Convert.ToBase64String(array);
        }

        public string Decrypt(string cipherText)
        {

            byte[] buffer = Convert.FromBase64String(cipherText);

            // Set up the encryption objects
            using (Aes aes = Aes.Create())
            {
                aes.Key = StringToByteArray(_cryptographyKeys.SecretKey);
                aes.IV = StringToByteArray(_cryptographyKeys.SecretIv);
  
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        public string GenerateSalt()
        {
            Random random = new Random();
            string salt = "";

            for (int i = 0; i < 27; i++)
            {
                int ascii = random.Next(65, 90);
                char character = Convert.ToChar(ascii);
                salt += character;
            }
            return salt;
        }

        private static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}

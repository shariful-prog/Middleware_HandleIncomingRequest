using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HandleEncryptParamRequest.Encrypt
{
    public class AESEncryption
    {
        public const string Secretkey = "shGDHoiplKDHdasE";
        public const string ivSecret = "mkjoijmNGYBmsjLR";

     public static  string AES_CBC_EncryptionToBase64(string plainText)
        {
            byte[] cipherData;
            Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(Secretkey);
            var iv = new byte[16];
            iv = Encoding.ASCII.GetBytes(ivSecret);
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            ICryptoTransform cipher = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, cipher, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                }

                cipherData = ms.ToArray();
            }
            string cTxt = Convert.ToBase64String(cipherData);
            return cTxt;
        }


        public static string DecryptFromHexa(string encryptValue)
        {
            string plainText;
            //byte[] combinedData = Convert.FromBase64String(combinedString);
            byte[] cipherText = AESEncryption.HexadecimalStringToByteArray(encryptValue);
            Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(Secretkey);
            var iv = new byte[16];
            iv = Encoding.ASCII.GetBytes(ivSecret);
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            ICryptoTransform decipher = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream(cipherText))
            {
                using (CryptoStream cs = new CryptoStream(ms, decipher, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        plainText = sr.ReadToEnd();
                    }
                }

                return plainText;
            }
        }

        public static string DecryptFromBase64(string encryptValue)
        {
            string plainText;
            byte[] cipherText = Convert.FromBase64String(encryptValue);
            Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(Secretkey);
            var iv = new byte[16];
            iv = Encoding.ASCII.GetBytes(ivSecret);
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            ICryptoTransform decipher = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream(cipherText))
            {
                using (CryptoStream cs = new CryptoStream(ms, decipher, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        plainText = sr.ReadToEnd();
                    }
                }

                return plainText;
            }
        }


        public static byte[] HexadecimalStringToByteArray(string input)
        {
            var outputLength = input.Length / 2;
            var output = new byte[outputLength];
            using (var sr = new StringReader(input))
            {
                for (var i = 0; i < outputLength; i++)
                    output[i] = Convert.ToByte(new string(new char[2] { (char)sr.Read(), (char)sr.Read() }), 16);
            }
            return output;
        }
    }
}

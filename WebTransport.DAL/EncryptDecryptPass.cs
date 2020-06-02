using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace WebTransport.DAL
{
    public class EncryptDecryptPass
    {
        /// <summary>

        /// Thsi method retrieve the string to encrypt from the Presentation Layer

        /// And return the Encrypted String

        /// </summary>

        /// <param name="str"></param>

        /// <returns></returns>

        public static string encryptPassword(string strText)
        {

            return Encrypt(strText, "&%#@?,:*");

        }

        /// <summary>

        /// This method retrieve the encrypted string to decrypt from the Presentation Layer

        /// And return the decrypted string

        /// </summary>

        /// <param name="str"></param>

        /// <returns></returns>

        public static string decryptPassword(string str)
        {

            return Decrypt(str, "&%#@?,:*");

        }

        /// <summary>

        /// This method has been used to get the Encrypetd string for the

        /// passed string

        /// </summary>

        /// <param name="strText"></param>

        /// <param name="strEncrypt"></param>

        /// <returns></returns>

        private static string Encrypt(string strText, string strEncrypt)
        {

            byte[] byKey = new byte[20];

            byte[] dv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            try
            {

                byKey = System.Text.Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                byte[] inputArray = System.Text.Encoding.UTF8.GetBytes(strText);

                MemoryStream ms = new MemoryStream();

                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, dv), CryptoStreamMode.Write);

                cs.Write(inputArray, 0, inputArray.Length);

                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());

            }

            catch (Exception ex)
            {

                throw ex;

            }

        }

        /// <summary>

        /// This method has been used to Decrypt the Encrypted String

        /// </summary>

        /// <param name="strText"></param>

        /// http://www.hanusoftware.com

        /// <param name="strEncrypt"></param>

        /// <returns></returns>

        private static string Decrypt(string strText, string strEncrypt)
        {

            byte[] bKey = new byte[20];

            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            try
            {

                bKey = System.Text.Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                Byte[] inputByteArray = inputByteArray = Convert.FromBase64String(strText);

                MemoryStream ms = new MemoryStream();

                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(bKey, IV), CryptoStreamMode.Write);

                cs.Write(inputByteArray, 0, inputByteArray.Length);

                cs.FlushFinalBlock();

                System.Text.Encoding encoding = System.Text.Encoding.UTF8;

                return encoding.GetString(ms.ToArray());

            }

            catch (Exception ex)
            {

                throw ex;

            }

        }

    }
}

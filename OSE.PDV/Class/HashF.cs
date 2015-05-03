﻿#region USING

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

#endregion
namespace OSE.PDV.Class
{
    //-----------------------------------------------------------------------
    // <copyright file="HashF.cs" company="OSE Solution Inc.">
    //     Copyright (c) OSE Solution Inc.  All rights reserved.
    // </copyright>
    // <summary>Contains the HashF class.</summary>
    //-----------------------------------------------------------------------
    public static class HashF
    {
        public static string Chave = "!@#";
        public static string Codifica(string clearText)
        {
            string encryptionKey = Chave;
            Byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[]
                {
                    0x49,
			        0x76,
        			0x61,
		        	0x6e,
			        0x20,
			        0x4d,
			        0x65,
			        0x64,
			        0x76,
			        0x65,
			        0x64,
			        0x65,
			        0x76
                });
                if (encryptor != null)
                {
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        clearText = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            return clearText;
        }
        public static string Decodifica(string cipherText)
        {
            var encryptionKey = Chave;
            Byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[]
                    {
                        0x49,
			            0x76,
        			    0x61,
		        	    0x6e,
			            0x20,
			            0x4d,
			            0x65,
			            0x64,
			            0x76,
			            0x65,
			            0x64,
			            0x65,
			            0x76
                    });
                if (encryptor != null)
                {
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                ;
            }
            return cipherText;
        }
    }
}

using System;
using System.Security.Cryptography;
using System.Text;

namespace SerialKey.Manager;

public class SerialKey
{    
    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    /// <summary>
    /// Generates a serial key in the format XXXXX-XXXXX-XXXXX-XXXXX
    /// </summary>
    /// <returns></returns>
    public static string GenerateKey()
    {
        var random = new Random(); 
        var segments = new StringBuilder(); 

        for (int i = 0; i < 4; i++)
        {
            if (i > 0)
                segments.Append('-'); 

            for (int j = 0; j < 5; j++)
            {
                segments.Append(chars[random.Next(chars.Length)]);
            }
        }

        return segments.ToString(); 
    }

    /// <summary>
    /// Sign the serial key using the RSA private key
    /// </summary>
    /// <param name="privateKey"></param>
    /// <param name="productKey"></param>
    /// <returns></returns>
    public static byte[] SignKey(RSA privateKey, string productKey)
    {
        var hash = GetHash(productKey); 
        return privateKey.SignHash(hash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1); 
    }

    /// <summary>
    /// Verify the serial key using the RSA public key
    /// </summary>
    /// <param name="publicKey"></param>
    /// <param name="productKey"></param>
    /// <param name="signature"></param>
    /// <returns></returns>
    public static bool VerifyKey(RSA publicKey, string productKey, byte[] signature)
    { 
        var hash = GetHash(productKey);
        return publicKey.VerifyHash(hash, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    }

    private static byte[] GetHash(string productKey)
    {
        var productKeyBytes = Encoding.UTF8.GetBytes(productKey); 
        var sha256 = SHA256.Create(); 
        return sha256.ComputeHash(productKeyBytes); 
    }
}

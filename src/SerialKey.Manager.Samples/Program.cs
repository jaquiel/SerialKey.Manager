using System.Security.Cryptography;

namespace SerialKey.Manager.Samples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create a new instance of RSA
            using (RSA rsa = RSA.Create(2048))
            {
                // Export the public key
                RSAParameters publicKeyParams = rsa.ExportParameters(false);
                RSA publicKey = RSA.Create();
                publicKey.ImportParameters(publicKeyParams);

                // Generate a product key
                string productKey = SerialKey.GenerateKey();

                // Sign the product key using the private key
                byte[] signature = SerialKey.SignKey(rsa, productKey);

                // Verify the product key using the public key
                bool isValid = SerialKey.VerifyKey(publicKey, productKey, signature);
                Console.WriteLine("Is the product key valid? " + isValid);

                // Test with a different key
                string invalidKey = "ABCDE-FGHIJ-KLMNO-PQRST";
                bool isInvalidKeyValid = SerialKey.VerifyKey(publicKey, invalidKey, signature);
                Console.WriteLine("Is the different product key valid? " + isInvalidKeyValid);
            }
        }
    }
}

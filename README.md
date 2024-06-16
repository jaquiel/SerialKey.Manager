# SerialKey.Manager

SerialKey.Manager is a simple library for generating, signing, and verifying product keys using RSA cryptography.

## Features

- **Product Key Generation**: Generates product keys in the format `XXXXX-XXXXX-XXXXX-XXXXX`.
- **Product Key Signing**: Signs product keys using an RSA private key.
- **Product Key Verification**: Verifies the authenticity of product keys using an RSA public key.

## Installation

You can install the SerialKey.Manager package via the NuGet Package Manager Console:

```bash
Install-Package SerialKey.Manager
```

## Usage

### Serial Key Generation
 
The `GenerateKey` method generates a product key in the format `XXXXX-XXXXX-XXXXX-XXXXX`, where each `X` is an alphanumeric character (A-Z and 0-9).

```
// Generates a product key in the format XXXXX-XXXXX-XXXXX-XXXXX
string productKey = SerialKeyManager.GenerateKey();
//4L2GU-OGHMU-0XXQ2-882FD
```

### Serial Key Signing

The `SignKey` method signs the product key using an RSA private key. The product key is first converted to bytes, then hashed using SHA-256. This hash is then signed using the RSA private key with PKCS#1 v1.5 padding.

```
// Create a new instance of RSA
using (RSA rsa = RSA.Create(2048))
{
    // Sign the product key using the private key
    byte[] signature = SerialKeyManager.SignKey(rsa, productKey);
    // Convert the signature to base64
    string signatureBase64 = Convert.ToBase64String(signature);
    //oqBcnLlMg/BlQ427Yy6e5tPx2ePPPQVejCLVoVmeMca/vDYvaatROAsBAoXpsF9Z01GSiSyjAJcey8xRKHs1FeYM2wNEkvS89xKzBLym50aQo+T7ja8x1YAXZ3/+aQd5082oA4pxZRy9DYB4rdt+oWSQDidBMBNsinyoxdvrEz85h0Mj+qRvP35KRUQb/BmwpdoHvHzMFmwpXJf44YOBsNZVF1wdAMUKUYe68NHceeVBTu1EhFEP9baC641w4QG03vQfKBOGjKyHma0SSugwCy/LpmGPJqRQwHIB8T4uyVZixcOx6AaN3Wz3dKaRoWyKFPsat7XNAdi3hkCEk6ewMw==
}
```

### Serial Key Verification

The `VerifyKey` method verifies the authenticity of the product key using an RSA public key. The product key hash is recalculated and compared to the provided signature using the RSA public key with PKCS#1 v1.5 padding.

```
// Create a new RSA public key instance  
RSA publicKey = RSA.Create(); 
// Import the public key parameters
publicKey.ImportParameters(publicKeyParams); 

// Verify the product key using the public key 
bool isValid = SerialKeyManager.VerifyKey(publicKey, productKey, signature);
// True

// Test with a different key string invalidKey = "ABCDE-FGHIJ-KLMNO-PQRST"; bool isInvalidKeyValid = SerialKeyManager.VerifyKey(publicKey, invalidKey, signature);
//False
```

## Contribution

If you would like to contribute to the project, feel free to open an issue or submit a pull request.

## License

This project is licensed under the terms of the MIT license.
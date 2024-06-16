using System.Security.Cryptography;

namespace SerialKey.Manager.Tests
{
    public class SerialKeyTests
    {
        //create a unit test for the GenerateKey method

        [Fact]
        public void GenerateKey_Returns_Valid_Key()
        {
            // Arrange
            var key = SerialKey.GenerateKey();

            // Act
            var keyParts = key.Split('-');

            // Assert
            Assert.Equal(4, keyParts.Length);
            Assert.Equal(5, keyParts[0].Length);
            Assert.Equal(5, keyParts[1].Length);
            Assert.Equal(5, keyParts[2].Length);
            Assert.Equal(5, keyParts[3].Length);
        }

        //create a unit test for the SignKey method
        [Fact]
        public void SignKey_Returns_Valid_Signature()
        {
            // Arrange
            using (RSA rsa = RSA.Create(2048))
            {
                var key = SerialKey.GenerateKey();

                // Act
                var signature = SerialKey.SignKey(rsa, key);

                // Assert
                Assert.NotNull(signature);
                Assert.NotEmpty(signature);
            }
        }

        //create a unit test for the VerifyKey method
        [Fact]
        public void VerifyKey_Returns_True_For_Valid_Key()
        {
            // Arrange
            using RSA rsa = RSA.Create(2048);
            var key = SerialKey.GenerateKey();
            var signature = SerialKey.SignKey(rsa, key);

            // Act
            var result = SerialKey.VerifyKey(rsa, key, signature);

            // Assert
            Assert.True(result);
        }


    }
}
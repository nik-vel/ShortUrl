using ShortUrlApi.Shortener;

namespace UnitTests
{
    internal class SimpleShortenerTests
    {
        private IShortener shortener;

        [SetUp]
        public void Setup()
        {
            shortener = new SimpleShortener();
        }

        [Test]
        public void Encode_ReturnsShortString()
        {
            // Arrange
            string longUrl = "http://base-test-url.com/test/page1";

            // Act
            string shortUrl = shortener.Encode(longUrl);

            // Assert
            Assert.IsNotNull(shortUrl);
            Assert.AreEqual(6, shortUrl.Length);
        }

        /// <summary>
        /// SimpleShortener doesn't depend on the base string that's why multiple calls will give different results
        /// </summary>
        [Test]
        public void Encode_ReturnsDifferentStringsForDifferentCalls()
        {
            // Arrange
            string longUrl = "http://base-test-url.com/test/page1";

            // Act
            string shortUrl1 = shortener.Encode(longUrl);
            string shortUrl2 = shortener.Encode(longUrl);

            // Assert
            Assert.AreNotEqual(shortUrl1, shortUrl2);
        }
    }
}
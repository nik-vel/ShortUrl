using ShortUrlApi.Shortener;
using ShortUrlApi.Storage;
using ShortUrlApi;

namespace UnitTests
{
    internal class UrlManagerTests
    {
        private Mock<IUrlStorage> _urlStorageMock;
        private Mock<IShortener> _shortenerMock;

        private UrlManager _urlManager;

        [SetUp]
        public void SetUp()
        {
            _urlStorageMock = new Mock<IUrlStorage>();
            _shortenerMock = new Mock<IShortener>();

            _urlManager = new UrlManager(_urlStorageMock.Object, _shortenerMock.Object);
        }

        [Test]
        public async Task GetShortUrl_WhenShortUrlKeyExists_ReturnsShortUrl()
        {
            // Arrange
            string baseUrl = "http://base-test-url.com/";
            string shortUrlTemplate = "http://short.com/{0}";
            string shortUrlKey = "abcd12";

            _urlStorageMock.Setup(mock => mock.GetShortUrlKey(baseUrl)).ReturnsAsync(shortUrlKey);

            // Act
            string result = await _urlManager.GetShortUrl(baseUrl, shortUrlTemplate);

            // Assert
            string expected = string.Format(shortUrlTemplate, shortUrlKey);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public async Task GetShortUrl_WhenShortUrlKeyDoesNotExist_GeneratesShortUrl()
        {
            // Arrange
            string baseUrl = "http://base-test-url.com/";
            string shortUrlTemplate = "http://short.com/{0}";
            string shortUrlKey = "abcd12";
            string generatedShortUrl = "http://short.com/" + shortUrlKey;

            _urlStorageMock.Setup(mock => mock.GetShortUrlKey(baseUrl)).ReturnsAsync("");
            _urlStorageMock.Setup(mock => mock.SaveUrlMap(baseUrl, shortUrlKey)).Returns(Task.CompletedTask);

            _shortenerMock.Setup(mock => mock.Encode(baseUrl)).Returns(shortUrlKey);
            
            // Act
            string result = await _urlManager.GetShortUrl(baseUrl, shortUrlTemplate);

            // Assert
            Assert.AreEqual(generatedShortUrl, result);
        }

        [Test]
        public async Task GetBaseUrl_WhenShortUrlKeyExists_ReturnsBaseUrl()
        {
            // Arrange
            string shortUrlKey = "abcd12";
            string baseUrl = "http://base-test-url.com/";

            _urlStorageMock.Setup(mock => mock.GetBaseUrl(shortUrlKey)).ReturnsAsync(baseUrl);

            // Act
            string result = await _urlManager.GetBaseUrl(shortUrlKey);

            // Assert
            Assert.AreEqual(baseUrl, result);
        }

        [Test]
        public async Task GetBaseUrl_WhenShortUrlKeyDoesNotExist_ReturnsNull()
        {
            // Arrange
            string shortUrlKey = "abcd12";

            _urlStorageMock.Setup(mock => mock.GetBaseUrl(shortUrlKey)).ReturnsAsync("");

            // Act
            string result = await _urlManager.GetBaseUrl(shortUrlKey);

            // Assert
            Assert.IsNull(result);
        }
    }
}

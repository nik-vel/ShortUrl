using ShortUrlApi.Validators;

namespace UnitTests
{
    internal class UrlValidatorTests
    {
        private IUrlValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new UrlValidator();
        }

        [Test]
        public void IsValidUrl_ValidUrl_ReturnsTrue()
        {
            // Arrange
            string url = "http://base-test-url.com/";

            // Act
            bool isValid = _validator.IsValidUrl(url);

            // Assert
            Assert.IsTrue(isValid);
        }

        [Test]
        public void IsValidUrl_InvalidUrl_ReturnsFalse()
        {
            // Arrange
            string url = "not_a_url";

            // Act
            bool isValid = _validator.IsValidUrl(url);

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}

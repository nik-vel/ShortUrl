namespace ShortUrlApi.Shortener
{
    /// <summary>
    /// Simple implementation of IShortener.
    /// Length of the short string declared as a const.
    /// Encoding does't depend on the base string.
    /// </summary>
    public class SimpleShortener : IShortener
    {
        private const int SHORT_STR_LENGTH = 6;

        /// <inheritdoc />
        public string Encode(string _)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();

            string shortUrl = new string(Enumerable.Repeat(chars, SHORT_STR_LENGTH)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return shortUrl;
        }
    }
}

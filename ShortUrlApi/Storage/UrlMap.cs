namespace ShortUrlApi.Storage
{
    public class UrlMap
    {
        public int Id { get; set; }

        /// <summary>
        /// URL to be shortened
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Encoded URL
        /// Store it without the host to save space
        /// </summary>
        public string ShortUrlKey { get; set; }
    }
}

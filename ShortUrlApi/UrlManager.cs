using ShortUrlApi.Storage;
using ShortUrlApi.Shortener;

namespace ShortUrlApi
{
    public class UrlManager : IUrlManager
    {
        private readonly IUrlStorage _urlStorage;
        private readonly IShortener _shortener;

        public UrlManager(IUrlStorage urlStorage, IShortener shortener)
        {
            _urlStorage = urlStorage;
            _shortener = shortener;
        }

        public async Task<string> GetShortUrl(string baseUrl, string shortUrlTemplate)
        {
            string shortUrlKey = await _urlStorage.GetShortUrlKey(baseUrl);

            if (string.IsNullOrEmpty(shortUrlKey))
            {
                shortUrlKey = _shortener.Encode(baseUrl);
                await _urlStorage.SaveUrlMap(baseUrl, shortUrlKey);
            }

            return string.Format(shortUrlTemplate, shortUrlKey);
        }

        public async Task<string> GetBaseUrl(string shortUrlKey)
        {
            var baseUrl = await _urlStorage.GetBaseUrl(shortUrlKey);

            return string.IsNullOrEmpty(baseUrl) ? null : baseUrl;
        }
    }
}

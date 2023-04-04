namespace ShortUrlApi
{
    public interface IUrlManager
    {
        Task<string> GetShortUrl(string baseUrl, string shortUrlTemplate);

        Task<string> GetBaseUrl(string shortUrlKey);
    }
}
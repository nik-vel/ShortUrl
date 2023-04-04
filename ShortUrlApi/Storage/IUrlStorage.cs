namespace ShortUrlApi.Storage
{
    /// <summary>
    /// Api for data access
    /// </summary>
    public interface IUrlStorage
    {
        Task<string> GetShortUrlKey(string baseUrl);

        Task SaveUrlMap(string baseUrl, string shortUrlKey);

        Task<string> GetBaseUrl(string shortUrlKey);
    }
}

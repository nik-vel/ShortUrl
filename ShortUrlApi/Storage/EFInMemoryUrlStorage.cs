using Microsoft.EntityFrameworkCore;

namespace ShortUrlApi.Storage
{
    /// <summary>
    /// I use EntityFrameworkCore In-memory database only for demonstration purposes. 
    /// Microsoft doesn't recommend to use it for production: 
    ///     https://learn.microsoft.com/en-us/ef/core/providers/in-memory/
    /// </summary>
    public class EFInMemoryUrlStorage : DbContext, IUrlStorage
    {
        public DbSet<UrlMap> UrlMaps { get; set; }

        public EFInMemoryUrlStorage(DbContextOptions<EFInMemoryUrlStorage> options)
            : base(options)
        {
        }

        public async Task<string> GetShortUrlKey(string baseUrl)
        {
            var urlMap = await UrlMaps.FirstOrDefaultAsync(x => x.BaseUrl == baseUrl);
            if (urlMap != null)
            {
                return urlMap.ShortUrlKey;
            }

            return null;
        }

        public async Task SaveUrlMap(string baseUrl, string shortUrlKey)
        {
            await UrlMaps.AddAsync(new UrlMap { BaseUrl = baseUrl, ShortUrlKey = shortUrlKey });
            await SaveChangesAsync();
        }

        public async Task<string> GetBaseUrl(string shortUrlKey)
        {
            var urlMap = await UrlMaps.FirstOrDefaultAsync(x => x.ShortUrlKey == shortUrlKey);
            if (urlMap != null)
            {
                return urlMap.BaseUrl;
            }

            return null;
        }
    }
}

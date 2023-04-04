using Microsoft.EntityFrameworkCore;
using ShortUrlApi;
using ShortUrlApi.Shortener;
using ShortUrlApi.Storage;
using ShortUrlApi.Validators;

namespace Microsoft.AspNetCore.Builder
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddDbContext<EFInMemoryUrlStorage>(opt => opt.UseInMemoryDatabase("in-memory-db"), ServiceLifetime.Singleton);
            services.AddSingleton<IUrlStorage, EFInMemoryUrlStorage>();

            services.AddSingleton<IShortener, SimpleShortener>();

            services.AddSingleton<IUrlManager, UrlManager>();
            services.AddSingleton<IUrlValidator, UrlValidator>();
        }

    }
}

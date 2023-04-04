namespace ShortUrlApi.Validators
{
    public class UrlValidator : IUrlValidator
    {
        public bool IsValidUrl(string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }
    }
}

namespace ShortUrlApi.Shortener
{
    /// <summary>
    /// Encoder which create short string from the base string
    /// </summary>
    public interface IShortener
    {
        /// <summary>
        /// Create short string from the base string
        /// </summary>
        string Encode(string baseString);
    }
}

using Microsoft.AspNetCore.Mvc;
using ShortUrlApi.Validators;

namespace ShortUrlApi.Controllers
{
    [ApiController]
    [Route("url")]
    public class UrlController : ControllerBase
    {
        private readonly IUrlManager _urlManager;
        private readonly IUrlValidator _urlValidator;
        private readonly ILogger<UrlController> _logger;

        public UrlController(IUrlManager urlManager, IUrlValidator urlValidator, ILogger<UrlController> logger)
        {
            _urlManager = urlManager;
            _urlValidator = urlValidator;
            _logger = logger;
        }


        [HttpPost()]
        public async Task<IActionResult> Shorten(ShortenUrlDto shortenUrl)
        {
            try
            {
                if (shortenUrl == null || !_urlValidator.IsValidUrl(shortenUrl.BaseUrl))
                {
                    return BadRequest("Invalid url format");
                }

                string urlTemplate = $"{Request.Scheme}://{Request.Host}/{ControllerContext.RouteData.Values["controller"]}/{{0}}";
                var shortUrl = await _urlManager.GetShortUrl(shortenUrl.BaseUrl, urlTemplate);

                if (string.IsNullOrEmpty(shortUrl))
                {
                    _logger.LogError($"Unable to create a short url for {shortenUrl.BaseUrl}");
                    return StatusCode(500); //Internal Server Error
                }

                return Ok(shortUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return StatusCode(500); //Internal Server Error
            }

        }

        [HttpGet("{shortUrlKey}")]
        public async Task<IActionResult> OpenUrl(string shortUrlKey)
        {
            try
            {
                var baseUrl = await _urlManager.GetBaseUrl(shortUrlKey);

                if (string.IsNullOrEmpty(baseUrl))
                {
                    _logger.LogWarning($"Base url for the key {shortUrlKey} wasn't found");
                    return NotFound();
                }

                return Redirect(baseUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return StatusCode(500); //Internal Server Error
            }
        }

    }
}

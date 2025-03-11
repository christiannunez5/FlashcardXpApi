namespace FlashcardXpApi.Features.Auth
{
    public class CookieService : ICookieService
    {

        private readonly IHttpContextAccessor _contextAccessor;

        public CookieService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string? Get(string key)
        {
            if (_contextAccessor.HttpContext?.Request.Cookies.TryGetValue(key, out var value) == true)
            {
                return value;
            }
            return null;
        }

        public void Remove(string key)
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete(key);
        }

        public void Store(string key, string token, DateTime lifetime)
        {

            _contextAccessor.HttpContext?.Response.Cookies.Append(key, token,
            new CookieOptions
            {
                Expires = lifetime,
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                IsEssential = true,
            });
        }
    }
}

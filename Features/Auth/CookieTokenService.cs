namespace FlashcardXpApi.Features.Auth
{
    public class CookieTokenService : ITokenService
    {

        private readonly IHttpContextAccessor _contextAccessor;

        public CookieTokenService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void StoreToken(string token)
        {

            _contextAccessor.HttpContext?.Response.Cookies.Append("accessToken", token,
            new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddMinutes(30),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                IsEssential = true,
            });
        }
    }
}

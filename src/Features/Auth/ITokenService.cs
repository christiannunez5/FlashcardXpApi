namespace FlashcardXpApi.Features.Auth
{
    public interface ITokenService
    {
        void StoreToken(string token);
    }
}

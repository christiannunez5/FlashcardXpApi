namespace FlashcardXpApi.Features.Auth
{
    public interface ICookieService
    {
        void Store(string key, string value, DateTime lifetime);
        void Remove(string key);

        string? Get(string key);
    }
}

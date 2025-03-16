namespace FlashcardXpApi.Application.Common.Interfaces
{
    public interface ICookieService
    {
        string? Get(string key);

        void Remove(string key);

        void Store(string key, string value, DateTime lifeTime);
    }
}

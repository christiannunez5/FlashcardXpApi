namespace Application.Common.Abstraction;

public interface ICookieService
{
    string? Get(string key);
    
    void Store(string key, string value, DateTime lifeTime);
    
    void Remove(string key);

}
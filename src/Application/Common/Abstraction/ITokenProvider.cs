using Domain.Entities.Auth;

namespace Application.Common.Abstraction;

public interface ITokenProvider
{
    string CreateToken(User user);
    string GenerateRefreshToken();
}
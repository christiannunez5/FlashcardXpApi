using Domain.Entities.Auth;
using Domain.Entities.Users;

namespace Application.Common.Abstraction;

public interface ITokenProvider
{
    string CreateToken(User user);
    string GenerateRefreshToken();
}
using Domain.Entities.Auth;

namespace Application.Common.Abstraction;

public interface IUserContext
{
    string UserId();
}
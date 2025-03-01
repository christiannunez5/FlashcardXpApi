using System.ComponentModel.DataAnnotations;

namespace FlashcardXpApi.Features.Users
{
    public record UserDto(string Id, string Username, string Email, string ProfilePicUrl);
}

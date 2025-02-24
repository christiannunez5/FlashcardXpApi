using System.ComponentModel.DataAnnotations;

namespace FlashcardXpApi.Users
{
    public record UserDto(string Id, string Username, string Email, string ProfilePicUrl);
}

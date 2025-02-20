using System.ComponentModel.DataAnnotations;

namespace FlashcardXpApi.Users
{
    public record UserDto(int Id, string Username, string Email, string ProfilePicUrl);
}

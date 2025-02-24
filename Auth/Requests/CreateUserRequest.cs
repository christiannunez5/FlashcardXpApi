using FlashcardXpApi.Users;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace FlashcardXpApi.Auth.Requests
{
    public record CreateUserRequest(
        string Email , 
        string Username, 
        string Password, 
        string? ProfilePicUrl);
}

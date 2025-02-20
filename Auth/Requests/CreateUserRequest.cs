using FlashcardXpApi.Users;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace FlashcardXpApi.Auth.Requests
{
    public record CreateUserRequest(
        string Email , 
        string Username, 
        string Password, 
        string? ProfilePicUrl)
    {   
        public static User ToUser(CreateUserRequest request)
        {
            return new User()
            {
                Email = request.Email,
                Username = request.Username,
                Password = request.Password,
                ProfilePicUrl = request.ProfilePicUrl
            };
        }
    }
}

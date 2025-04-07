namespace FlashcardXpApi.Application.Contracts.Auth
{
    public record CreateUserRequest(string Email, string Username, 
        string Password, string? ProfilePicUrl);
}

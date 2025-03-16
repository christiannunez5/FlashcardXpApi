namespace FlashcardXpApi.Application.Contracts
{
    public record CreateUserRequest(string Email, string Username, 
        string Password, string? ProfilePicUrl);
}

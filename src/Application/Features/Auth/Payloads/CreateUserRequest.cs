namespace Application.Features.Auth.Payloads;

public record CreateUserRequest(string Email, string Username, string Password, string ProfilePicUrl = "");
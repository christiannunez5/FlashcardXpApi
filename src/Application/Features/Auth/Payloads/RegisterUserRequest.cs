namespace Application.Features.Auth.Payloads;


public record RegisterUserRequest(string Email, string Username, string Password, string ProfilePicUrl = "");
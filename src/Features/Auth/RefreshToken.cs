using FlashcardXpApi.Features.Users;

namespace FlashcardXpApi.Features.Auth
{
    public class RefreshToken
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Token { get; set; }

        public string? UserId { get; set; }
        public required User User { get; set; }

        public DateTime ExpiresOnUtc { get; set; }
    }
}

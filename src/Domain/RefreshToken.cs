namespace FlashcardXpApi.Domain
{
    public class RefreshToken
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Token { get; set; }

        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }

        public DateTime ExpiresOnUtc { get; set; }
    }
}

namespace FlashcardXpApi.Domain
{
    public class Quest
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Title { get; set; }
        public required string Description { get; set; }
        public int XpReward { get; set; }
    }
}

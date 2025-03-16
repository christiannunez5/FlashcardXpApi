namespace FlashcardXpApi.Domain
{
    public class UserQuest
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string QuestId { get; set; } = string.Empty;
        public Quest? Quest { get; set; }
        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }

        public bool isCompleted { get; set; }
       
    }
}

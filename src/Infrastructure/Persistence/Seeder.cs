using Application.Common.Abstraction;
using Domain.Entities.Quests;

namespace Infrastructure.Persistence;

public static class Seeder
{
    public static async Task Initialize(IApplicationDbContext context, CancellationToken cancellationToken)
    {
        if (!context.Quests.Any())
        {
            var quests = new List<Quest>
            {
                new Quest
                {
                    Title = "Flashcard Initiate",
                    Description = "Complete 1 flashcard today.",
                    XpReward = 25
                },
                new Quest
                {
                    Title = "Daily Grinder",
                    Description = "Complete 4 flashcards today.",
                    XpReward = 50
                },
                new Quest
                {
                    Title = "Focused Learner",
                    Description = "Complete 10 flashcards in a single session.",
                    XpReward = 75
                },
                new Quest
                {
                    Title = "No Card Left Behind",
                    Description = "Complete all flashcards in a study set.",
                    XpReward = 100
                },
                new Quest
                {
                    Title = "Flashcard Finisher",
                    Description = "Complete 50 flashcards total.",
                    XpReward = 150
                },
                new Quest
                {
                    Title = "Century Scholar",
                    Description = "Complete 100 flashcards total.",
                    XpReward = 200
                },
                new Quest
                {
                    Title = "All In One Go",
                    Description = "Complete 20 flashcards without exiting the session.",
                    XpReward = 175
                },
                new Quest
                {
                    Title = "Ultimate Recall",
                    Description = "Complete 300 flashcards in total.",
                    XpReward = 500
                }
            };

            await context.Quests.AddRangeAsync(quests);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
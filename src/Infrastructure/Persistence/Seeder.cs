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
                    Title = "Lightning Learner",
                    Description = "Answer 5 flashcards",
                    XpReward = 150,
                    IconUrl = "https://cdn-icons-png.flaticon.com/512/169/169367.png",
                    Goal = 5
                },
                new Quest
                {
                    Title = "Marathon mind",
                    Description = "Answer 10 flashcards",
                    XpReward = 200,
                    IconUrl = "https://cdn-icons-png.flaticon.com/512/169/169362.png",
                    Goal = 10
                },
                new Quest
                {
                    Title = "Brainiac Burst",
                    Description = "Answer 15 flashcards",
                    XpReward = 250,
                    IconUrl = "https://cdn-icons-png.flaticon.com/512/1055/1055646.png",
                    Goal = 15
                },
                new Quest
                {
                    Title = "Daily Dynamo",
                    Description = "Answer 20 flashcards",
                    XpReward = 400,
                    IconUrl = "https://cdn-icons-png.flaticon.com/512/3176/3176297.png",
                    Goal = 20
                },
                new Quest
                {
                    Title = "Shockwave Sprinter",
                    Description = "Answer 50 flashcards",
                    XpReward = 700,
                    IconUrl = "https://cdn-icons-png.flaticon.com/512/169/169374.png",
                    Goal = 50
                },
               
            };

            await context.Quests.AddRangeAsync(quests);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
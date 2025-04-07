using FlashcardXpApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace FlashcardXpApi.Infrastructure.Persistence
{
    public static class Seeder
    {
        public static async Task Initialize(DataContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.Quests.Any())
            {
                var quests = new List<Quest>
                {
                    new Quest
                    {
                        Title = "Flashcard Rookie",
                        Description = "Complete your first flashcard deck.",
                        XpReward = 50
                    },
                    new Quest
                    {
                        Title = "Memory Master",
                        Description = "Successfully recall all flashcards in a deck at least once without mistakes.",
                        XpReward = 100
                    },
                    new Quest
                    {
                        Title = "Speedy Recall",
                        Description = "Complete a flashcard deck in under 2 minutes.",
                        XpReward = 75
                    },
                    new Quest
                    {
                        Title = "Flashcard Expert",
                        Description = "Correctly answer 100 flashcards in a row.",
                        XpReward = 150
                    },
                    new Quest
                    {
                        Title = "Daily Practice",
                        Description = "Practice your flashcards for 5 consecutive days.",
                        XpReward = 120
                    },
                    new Quest
                    {
                        Title = "Deck Creator",
                        Description = "Create and upload your first custom flashcard deck.",
                        XpReward = 80
                    },
                    new Quest
                    {
                        Title = "Master of Categories",
                        Description = "Complete three flashcard decks from different categories.",
                        XpReward = 200
                    },
                    new Quest
                    {
                        Title = "Challenge Seeker",
                        Description = "Complete a flashcard challenge from the leaderboard.",
                        XpReward = 250
                    },
                    new Quest
                    {
                        Title = "Quiz Time",
                        Description = "Take and pass a quiz based on your flashcards with a score of 90% or higher.",
                        XpReward = 180
                    },
                    new Quest
                    {
                        Title = "Flashcard Scholar",
                        Description = "Complete every deck available in the flashcard library.",
                        XpReward = 500
                    }
                };

                await context.Quests.AddRangeAsync(quests);
                await context.SaveChangesAsync();
            }

            var users = await context.Users.ToListAsync();
            var userExperiences = new List<UserExperience>();
            
            foreach (var user in users)
            {
                var isUserExperienceAlreadyAdded = await context
                    .UserExperiences
                    .AnyAsync(ux => ux.UserId == user.Id);

                if (!isUserExperienceAlreadyAdded)
                {
                    userExperiences.Add(new UserExperience
                    {
                        UserId = user.Id
                    });
                }
            }
            await context.UserExperiences.AddRangeAsync(userExperiences);
            await context.SaveChangesAsync();

        }
    }
}

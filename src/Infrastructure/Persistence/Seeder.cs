using Application.Common.Abstraction;
using Domain.Entities.Quests;
using Domain.Entities.Tags;

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
        
        if (!context.Tags.Any())
        {
            var tags = new List<Tag>
            {
                new Tag
                {
                    Name = "Science",
                    ImageUrl = "https://cdn-icons-png.flaticon.com/128/2022/2022299.png"
                },
                new Tag
                {
                    Name = "Mathematics",
                    ImageUrl = "https://cdn-icons-png.flaticon.com/128/1739/1739515.png"
                },
                new Tag
                {
                    Name = "Engineering",
                    ImageUrl = "https://cdn-icons-png.flaticon.com/128/825/825792.png"
                },
                new Tag
                {
                    Name = "Technology",
                    ImageUrl = "https://cdn-icons-png.flaticon.com/128/1087/1087815.png"
                },
                new Tag
                {
                    Name = "Language",
                    ImageUrl = "https://cdn-icons-png.flaticon.com/128/3898/3898082.png"
                },
                new Tag
                {
                    Name = "History",
                    ImageUrl = "https://cdn-icons-png.flaticon.com/128/1800/1800196.png"
                },
                new Tag
                {
                    Name = "Art",
                    ImageUrl = "https://cdn-icons-png.flaticon.com/128/2400/2400603.png"
                },
                new Tag
                {
                    Name = "Geography",
                    ImageUrl = "https://cdn-icons-png.flaticon.com/128/290/290336.png"
                },
                new Tag
                {
                    Name = "Programming",
                    ImageUrl = "https://cdn-icons-png.flaticon.com/128/17359/17359748.png"
                },
                new Tag
                {
                    Name = "Business",
                    ImageUrl = "https://cdn-icons-png.flaticon.com/128/7890/7890493.png"
                }
            };
            
            await context.Tags.AddRangeAsync(tags, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
    
    public static async Task SeedTagStudySets(IApplicationDbContext context, CancellationToken cancellationToken)
    {
        var userId = "cf929597-e823-406e-90f5-31aa2ab9c17a";
        
        
    }
}
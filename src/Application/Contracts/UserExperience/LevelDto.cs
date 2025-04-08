namespace FlashcardXpApi.Application.Contracts.UserExperience;

public enum Level
{
    Herald = 1,
    Guardian = 2,
    Crusader = 3,
    Archon = 4,
    Legend = 5,
    Ancient = 6,
    Divine = 7,
    Immortal = 8,
}
public record LevelDto(int Value, string Title);

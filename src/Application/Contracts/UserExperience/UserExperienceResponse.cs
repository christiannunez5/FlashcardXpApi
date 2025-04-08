using FlashcardXpApi.Application.Contracts.Auth;

namespace FlashcardXpApi.Application.Contracts.UserExperience;

public record UserExperienceResponse(UserResponse User, int CurrentExperience, LevelDto Level, int MaxXp);

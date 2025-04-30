namespace Application.Features.StudySets.Payloads;

public record CreateStudySetRatingRequest(int Rating, string ReviewText = "");
using Domain.Entities.Auth;
using Domain.Entities.Flashcards;
using Domain.Entities.Studysets;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Abstraction;

public interface IApplicationDbContext
{
    DbSet<StudySet> StudySets { get; }
    DbSet<RecentStudySet> RecentStudySets { get; }
    DbSet<Flashcard> Flashcards { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
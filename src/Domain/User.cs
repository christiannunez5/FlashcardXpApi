using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FlashcardXpApi.Domain
{
    [Table("AppUser")]
    public class User : IdentityUser
    {
        public string? ProfilePicUrl { get; set; }

        // navigations
        [JsonIgnore]
        public ICollection<StudySetParticipant> StudySetParticipants { get; set; } =
            new List<StudySetParticipant>();

        public ICollection<StudySet> StudySets { get; set; } = new List<StudySet>();
        
        [JsonIgnore]
        public ICollection<FlashcardsCompleted> FlashcardsCompleted { get; set; } =
            new List<FlashcardsCompleted>();

        public ICollection<RecentStudySet> RecentStudySets { get; set; } =
           new List<RecentStudySet>();
            
        public UserExperience? Experience { get; set; }

    }
}

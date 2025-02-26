using FlashcardXpApi.FlashcardSets;
using FlashcardXpApi.StudySets;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FlashcardXpApi.Users
{
    [Table("AppUser")]
    public class User : IdentityUser
    {
        public string? ProfilePicUrl { get; set; }

        public ICollection<StudySet> StudySets { get; set; } = new List<StudySet>();

        // navigations
        [JsonIgnore]
        public ICollection<StudySetParticipant> StudySetParticipants { get; set; } =
            new List<StudySetParticipant>();
       
    }
}

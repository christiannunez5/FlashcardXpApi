using FlashcardXpApi.FlashcardSets;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlashcardXpApi.Users
{
    [Table("AppUser")]
    public class User : IdentityUser
    {
        public string? ProfilePicUrl { get; set; }

        // navigations
        public ICollection<StudySet> StudySets { get; set; } =
            new List<StudySet>();
       
    }
}

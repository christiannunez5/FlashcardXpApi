using FlashcardXpApi.FlashcardSets;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlashcardXpApi.Users
{
    [Table("User")]
    public class User : IdentityUser<int>
    {

        public required string Username { get; set; }
        public required string Password { get; set; }
        public string? ProfilePicUrl { get; set; }

        // navigations
        public ICollection<StudySet> StudySets { get; set; } =
            new List<StudySet>();
       
    }
}

using FlashcardXpApi.FlashcardSets;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlashcardXpApi.Users
{
    [Table("User")]
    public class User
    {
        public int Id { get; set; }

        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? ProfilePicUrl { get; set; }

        // navigations
        public ICollection<StudySet> StudySets { get; set; } =
            new List<StudySet>();
        
        public static User Create(
            string email,
            string username,
            string password,
            string? profilePicUrl
        )
        {
            return new User()
            {
                Email = email,
                Username = username,
                Password = password,
                ProfilePicUrl = profilePicUrl
            };
        }
    }
}

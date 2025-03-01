using Microsoft.AspNetCore.Identity;

namespace FlashcardXpApi.Features.Auth
{
    public class AppErrorDescriber : IdentityErrorDescriber
    {

        public override IdentityError DuplicateUserName(string userName)
        {
            var error = base.DuplicateUserName(userName);
            error.Description = "Username already taken,";
            return error;
        }


        public override IdentityError DuplicateEmail(string email)
        {
            var error = base.DuplicateEmail(email);
            error.Description = $"Account with email {email} already exists";
            return error;
        }
    }
}

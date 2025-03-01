using System.Net;

namespace FlashcardXpApi.Exceptions
{
    public class NotAuthorizedException : Exception
    {

        public NotAuthorizedException(string message) : base(message)
        {
        }
    }
}

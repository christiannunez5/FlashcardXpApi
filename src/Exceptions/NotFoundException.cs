using System.Net;

namespace FlashcardXpApi.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) 
        {
        }
    }
}

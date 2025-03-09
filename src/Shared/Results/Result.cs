using System.Text.Json.Serialization;

namespace FlashcardXpApi.Common.Results
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        public Error Error { get; }

        private Result()
        {
            IsSuccess = true;
            Error = Error.None;
        }

        private Result(Error error)
        {
            IsSuccess = false;
            Error = error;


        }
        
        public static Result Success => new();
        public static Result Failure(Error error) => new(error);
    }
}

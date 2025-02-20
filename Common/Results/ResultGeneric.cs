using FlashcardXpApi.FlashcardSets;

namespace FlashcardXpApi.Common.Results
{
    public class ResultGeneric<T>
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        public Error Error { get; }

        public T? Data { get; }

        private ResultGeneric(bool isSuccess, T? data, Error error)
        {
            if (isSuccess && error != Error.None)
                throw new ArgumentException("Invalid error", nameof(error));

            if (!isSuccess && error == Error.None)
                throw new ArgumentException("Invalid error", nameof(error));

            IsSuccess = isSuccess;
            Error = error;
            Data = data;
        }

        public static ResultGeneric<T> Success(T data) => new(true, data, Error.None);
        public static ResultGeneric<T> Failure(Error error) => new(false, default, error);

    }
}

namespace FlashcardXpApi.Application.Common
{
    public sealed record Error(string code, string Message)
    {
        public static Error None => new(ErrorTypeConstant.NONE, string.Empty);

    }

}

namespace FlashcardXpApi.Common.Results
{
    public sealed record Error(string Code, string Message)
    {
        public static Error None => new(ErrorTypeConstant.NONE, string.Empty);
    }
   
}

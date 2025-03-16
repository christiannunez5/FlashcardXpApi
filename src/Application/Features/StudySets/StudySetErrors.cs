using FlashcardXpApi.Application.Common;

namespace FlashcardXpApi.Application.Features.StudySets
{
    public class StudySetErrors
    {
        public static Error StudySetNotFoundError =
            new Error(ErrorTypeConstant.NOT_FOUND, "Study set not found.");
    }
}

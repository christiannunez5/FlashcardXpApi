using FlashcardXpApi.Application.Common;

namespace FlashcardXpApi.Application.Features.RecentStudySets
{
    public class RecentStudySetErrors
    {
        public static Error RecentStudySetAlreadyAdded =
            new Error(ErrorTypeConstant.CONFLICT, "Recent study set already added");
    }
}

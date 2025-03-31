using FlashcardXpApi.Application.Common;

namespace FlashcardXpApi.Application.Features.Quests
{
    public class QuestErrors
    {
        public static Error QuestNotFound =
            new Error(ErrorTypeConstant.NOT_FOUND, "Quest not found.");
    }
}

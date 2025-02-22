using AutoMapper;
using FlashcardXpApi.Common.Results;
using FlashcardXpApi.Flashcards.Requests;
using FlashcardXpApi.FlashcardSets;

namespace FlashcardXpApi.Flashcards
{
    public class FlashcardService
    {
        private readonly IFlashcardRepository _flashcardRepo;
        private readonly IStudySetRepository _studySetRepo;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly FlashcardRequestValidator _validator;

        public FlashcardService(
            IFlashcardRepository flashcardRepo, 
            IStudySetRepository studySetRepo, 
            ILogger<FlashcardService> logger, 
            IMapper mapper, 
            FlashcardRequestValidator validator
        )
        {
            _flashcardRepo = flashcardRepo;
            _studySetRepo = studySetRepo;
            _logger = logger;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ResultGeneric<List<FlashcardDto>>> GetAllByStudySet(
            int studySetId
        )
        {
            var studySet = await _studySetRepo.GetByIdAsync(studySetId);

            if (studySet is null)
            {
                return ResultGeneric<List<FlashcardDto>>.Failure(FlashcardErrors.StudySetNotFoundError);
            }

            var flashcards = await _flashcardRepo.GetAllByStudySet(studySet);
            return ResultGeneric<List<FlashcardDto>>.Success(
                _mapper.Map<List<FlashcardDto>>(flashcards)
            );
        }

        public async Task<ResultGeneric<FlashcardDto>> AddNewFlashcard (
            int studySetId,
            FlashcardRequest request
        )
        {
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errorMessage = validationResult.Errors
                    .Select(x => x.ErrorMessage)
                    .First();
                _logger.LogInformation($"Validation error: {errorMessage}");
                return ResultGeneric<FlashcardDto>.Failure(FlashcardErrors.FlashcardValidationError(errorMessage));
            }

            var studySet = await _studySetRepo.GetByIdAsync(studySetId);

            if (studySet is null)
            {
                return ResultGeneric<FlashcardDto>.Failure(FlashcardErrors.StudySetNotFoundError);
            }


            var newFlashcard = _mapper.Map<Flashcard>(request);
            newFlashcard.StudySet = studySet;

            _logger.LogInformation("Adding new flashcard");
            await _flashcardRepo.InsertAsync(newFlashcard);
            _logger.LogInformation("Flashcard added.");

            return ResultGeneric<FlashcardDto>.Success(
                _mapper.Map<FlashcardDto>(newFlashcard)
            );
        }
    }
}

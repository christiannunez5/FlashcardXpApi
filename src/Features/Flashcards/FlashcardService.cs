using AutoMapper;
using FlashcardXpApi.Common.Results;
using FlashcardXpApi.Features.Auth;
using FlashcardXpApi.Features.Flashcards.Requests;
using FlashcardXpApi.Features.StudySets;
using FlashcardXpApi.Features.Users;

namespace FlashcardXpApi.Features.Flashcards
{
    public class FlashcardService
    {
        private readonly IFlashcardRepository _flashcardRepo;
        private readonly IStudySetRepository _studySetRepo;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly FlashcardRequestValidator _validator;
        private readonly ICurrentUserService _currentUserService;

        public FlashcardService(
            IFlashcardRepository flashcardRepo,
            IStudySetRepository studySetRepo,
            ILogger<FlashcardService> logger,
            IMapper mapper,
            FlashcardRequestValidator validator,
            ICurrentUserService currentUserService
        )
        {
            _flashcardRepo = flashcardRepo;
            _studySetRepo = studySetRepo;
            _logger = logger;
            _mapper = mapper;
            _validator = validator;
            _currentUserService = currentUserService;
        }


        public async Task<ResultGeneric<FlashcardsByStudySetDto>> GetAllByStudySet(
            string studySetId
        )
        {
            var user = await _currentUserService.GetCurrentUser();

            var studySet = await _studySetRepo.GetByIdAsync(studySetId);

            if (studySet is null)
            {
                return ResultGeneric<FlashcardsByStudySetDto>.Failure(FlashcardErrors.StudySetNotFoundError);
            }

            var flashcards = await _flashcardRepo.GetAllByStudySet(studySet);

            var response = new FlashcardsByStudySetDto
            (
                studySet.Id,
                studySet.Title,
                studySet.Description,
                _mapper.Map<UserDto>(studySet.CreatedBy),
                _mapper.Map<List<FlashcardDto>>(flashcards)
            );

            return ResultGeneric<FlashcardsByStudySetDto>.Success(
                response
            );
        }

        public async Task<ResultGeneric<List<FlashcardDto>>> AddNewFlashcard(
            string studySetId,
            List<FlashcardRequest> requests
        )
        {
            foreach (var request in requests)
            {
                var validationResult = _validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    var errorMessage = validationResult.Errors
                        .Select(x => x.ErrorMessage)
                        .First();
                    _logger.LogInformation($"Validation error: {errorMessage}");
                    return ResultGeneric<List<FlashcardDto>>.Failure(FlashcardErrors.FlashcardValidationError(errorMessage));
                }
            }

            var studySet = await _studySetRepo.GetByIdAsync(studySetId);

            if (studySet is null)
            {
                return ResultGeneric<List<FlashcardDto>>.Failure(FlashcardErrors.StudySetNotFoundError);
            }

            var user = await _currentUserService.GetCurrentUser();

            if (user is not null && studySet.CreatedById != user.Id)
            {
                return ResultGeneric<List<FlashcardDto>>.Failure(FlashcardErrors.NotAuthorizedError);
            }

            var newFlashcards = _mapper.Map<List<Flashcard>>(requests);
            newFlashcards.ForEach(f => f.StudySet = studySet);

            _logger.LogInformation("Adding new flashcard");
            await _flashcardRepo.InsertAllAsync(newFlashcards);
            _logger.LogInformation("Flashcard added.");

            return ResultGeneric<List<FlashcardDto>>.Success(
                _mapper.Map<List<FlashcardDto>>(newFlashcards)
            );
        }

        public async Task<ResultGeneric<FlashcardDto>> DeleteFlashcard(
            string flashcardId
        )
        {
            var flashcard = await _flashcardRepo.GetByIdAsync(flashcardId);

            if (flashcard is null)
            {
                return ResultGeneric<FlashcardDto>.Failure(FlashcardErrors.FlashcardNotFoundError);
            }

            var user = await _currentUserService.GetCurrentUser();

            if (user is not null && flashcard.StudySet.CreatedById != user.Id)
            {
                return ResultGeneric<FlashcardDto>.Failure(FlashcardErrors.NotAuthorizedError);
            }

            await _flashcardRepo.DeleteAsync(flashcard);

            return ResultGeneric<FlashcardDto>.Success(
                _mapper.Map<FlashcardDto>(flashcard)
            );
        }

    }
}

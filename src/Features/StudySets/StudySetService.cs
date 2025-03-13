using AutoMapper;
using FlashcardXpApi.Common.Results;
using FlashcardXpApi.Features.Auth;
using FlashcardXpApi.Features.Flashcards;
using FlashcardXpApi.Services;

namespace FlashcardXpApi.Features.StudySets
{
    public class StudySetService : IStudySetService
    {
        private readonly IStudySetRepository _studySetRepo;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly StudySetRequestValidator
            _validator;
        private readonly IFlashcardRepository _flashcardRepository;
        public StudySetService(IStudySetRepository studySetRepo,
                               ILogger<AuthService> logger,
                               IMapper mapper,
                               StudySetRequestValidator validator,
                               ICurrentUserService currentUserService,
                               IFlashcardRepository flashcardRepository)
        {
            _studySetRepo = studySetRepo;
            _logger = logger;
            _mapper = mapper;
            _validator = validator;
            _currentUserService = currentUserService;
            _flashcardRepository = flashcardRepository;
        }


        public async Task<ResultGeneric<List<StudySetDto>>> GetStudySetsByUser()
        {
            var user = await _currentUserService.GetCurrentUser();

            if (user is null)
            {
                return ResultGeneric<List<StudySetDto>>.Failure(
                    StudySetErrors.AuthenticationRequiredError
                );
            };

            var studySets = await _studySetRepo.GetAllByUser(user);

            return ResultGeneric<List<StudySetDto>>.Success(_mapper.Map<List<StudySetDto>>(studySets));

        }

       
        public async Task<ResultGeneric<string>> UpdateStudySet(
            string studySetId, 
            StudySetWithFlashcardsRequest request
        )
        {
            var user = await _currentUserService.GetCurrentUser();

            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errorMessage = validationResult.Errors
                    .Select(x => x.ErrorMessage)
                    .First();

                return ResultGeneric<string>.Failure(
                    StudySetErrors.ValidationError(errorMessage)
                );
            }

            var studySet = await _studySetRepo.GetByIdAsync(studySetId);

            if (studySet is null)
            {
                return ResultGeneric<string>.Failure(StudySetErrors.StudySetNotFoundError);
            }

            studySet.Title = request.Title;
            studySet.Description = request.Description;

            await _studySetRepo.UpdateAsync(studySet);

            foreach ( var flashcard in request.Flashcards )
            {

                var currentFlashcard = await _flashcardRepository.GetByIdAsync(flashcard.Id);

                if (currentFlashcard is not null)
                {
                    currentFlashcard.Definition = flashcard.Definition;
                    currentFlashcard.Term = flashcard.Term;
                    await _flashcardRepository.UpdateAsync(currentFlashcard);
                }

                else
                {
                    var newFlashcard = new Flashcard
                    {
                        Term = flashcard.Term,
                        Definition = flashcard.Definition,
                        StudySet = studySet
                    };
                    await _flashcardRepository.InsertAsync(newFlashcard);
                }
                
            }
            
            return ResultGeneric<string>.Success(studySet.Id);

        }


        public async Task<ResultGeneric<StudySetDto>> DeleteStudySet(string studySetId)
        {
            var user = await _currentUserService.GetCurrentUser();

            if (user is null)
            {
                return ResultGeneric<StudySetDto>.Failure(StudySetErrors.AuthenticationRequiredError);
            }

            var studySet = await _studySetRepo.GetByIdAsync(studySetId);

            if (studySet is null)
            {
                return ResultGeneric<StudySetDto>.Failure(StudySetErrors.StudySetNotFoundError);
            }


            if (studySet.CreatedById != user.Id)
            {
                return ResultGeneric<StudySetDto>.Failure(StudySetErrors.AuthorizationFailedError);
            }

            await _studySetRepo.DeleteAsync(studySet);


            return ResultGeneric<StudySetDto>.Success(
                _mapper.Map<StudySetDto>(studySet)
            );
        }

        public async Task<ResultGeneric<string>> AddEmptyStudySet()
        {
            var user = await _currentUserService.GetCurrentUser();

            if (user is null)
            {
                return ResultGeneric<string>.Failure(AuthErrors.AuthenticationRequiredError);
            }

            var newStudySet = new StudySet() { CreatedById = user.Id,
                Title = "",
                Description = ""
            };
            
            await _studySetRepo.InsertAsync(newStudySet);

            var newFlashcards = new List<Flashcard>
            {
                new Flashcard { Definition = "", Term = "", StudySet = newStudySet },
                new Flashcard { Definition = "", Term = "", StudySet = newStudySet },
                new Flashcard { Definition = "", Term = "", StudySet = newStudySet },
                new Flashcard { Definition = "", Term = "", StudySet = newStudySet },
            };

            await _flashcardRepository.InsertAllAsync(newFlashcards);

            return ResultGeneric<string>.Success(newStudySet.Id);
        }
    }
}

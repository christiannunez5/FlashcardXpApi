using AutoMapper;
using FlashcardXpApi.Common.Results;
using FlashcardXpApi.StudySets.Requests;
using FlashcardXpApi.Users;
using FluentValidation;

namespace FlashcardXpApi.FlashcardSets
{
    public class StudySetService
    {
        private readonly IStudySetRepository _studySetRepo;
        private readonly IUserRepository _userRepo;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly StudySetRequestValidator 
            _validator;

        public StudySetService(IStudySetRepository flashcardSetRepo, 
            IUserRepository userRepo,
            StudySetRequestValidator validator,
            ILogger<StudySetService> logger, IMapper mapper)
        {
            _studySetRepo = flashcardSetRepo;
            _userRepo = userRepo;
            _logger = logger;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ResultGeneric<List<StudySetDto>>> GetAllByUserId(int userId)
        {
            var user = await _userRepo.GetById(userId);

            if (user is null)
            {
                _logger.LogError($"Can't find user with an id of {userId}");
                return ResultGeneric<List<StudySetDto>>.Failure(StudySetError.UserNotFoundError);
            }

            var flashcardSetsDto = _mapper.Map<List<StudySetDto>>(
                await _studySetRepo.GetAllByUserId(userId)
            );   

            return ResultGeneric<List<StudySetDto>>.Success(flashcardSetsDto);
        }

        public async Task<ResultGeneric<StudySetDto>> AddNewStudySet(
            int userId, StudySetRequest request
        )
        {

            _logger.LogInformation($"New flashcard request: {request}");

            var validationResult = _validator.Validate( request );

            if (!validationResult.IsValid)
            {
                var errorMessage = validationResult.Errors
                    .Select(x => x.ErrorMessage)
                    .First();

                return ResultGeneric<StudySetDto>.Failure(
                    StudySetError.ValidationError(errorMessage)
                );
            }

            var user = await _userRepo.GetById(userId);

            if (user is null )
            {
                _logger.LogInformation($"User with id {userId} does not exist.");
                return ResultGeneric<StudySetDto>.Failure(
                    StudySetError.UserNotFoundError
                );
            }

            var newStudySet = new StudySet()
            {
                Title = request.Title,
                Description = request.Description,
                CreatedBy = user
            };

            _logger.LogInformation($"Adding new flashcard");
            await _studySetRepo.InsertAsync( newStudySet );
            _logger.LogInformation($"Successfully new flashcard");

            return ResultGeneric<StudySetDto>.Success(
                _mapper.Map<StudySetDto>(newStudySet)
            );
        }

        public async Task<ResultGeneric<StudySetDto>> UpdateStudySet(
            int userId, 
            int studySetId,
            StudySetRequest request)
        {

            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errorMessage = validationResult.Errors
                    .Select(x => x.ErrorMessage)
                    .First();

                return ResultGeneric<StudySetDto>.Failure(
                    StudySetError.ValidationError(errorMessage)
                );
            }

            var user = await _userRepo.GetById(userId);

            if (user is null)
            {
                _logger.LogInformation($"User with id {userId} does not exist.");
                return ResultGeneric<StudySetDto>.Failure(
                    StudySetError.UserNotFoundError
                );
            }

            var studySet = await _studySetRepo.GetByIdAsync(studySetId);

            if (studySet is null )
            {
                _logger.LogInformation($"Study set with id {studySetId} does not exist.");
                return ResultGeneric<StudySetDto>.Failure(
                    StudySetError.StudySetNotFoundError
                );
            }

            if (studySet.CreatedById != userId)
            {
                _logger.LogInformation($"User {user.Username} is not allowed to perform this action.");
                return ResultGeneric<StudySetDto>.Failure(
                    StudySetError.StudySetAccessDeniedError
                );
            }

            studySet.Title = request.Title;
            studySet.Description = 
                request.Description is not null ? request.Description : studySet.Description;

            _logger.LogInformation("Updating study set....");
            await _studySetRepo.UpdateAsync(studySet);
            _logger.LogInformation("Study set updated.");

            return ResultGeneric<StudySetDto>.Success(_mapper.Map<StudySetDto>(studySet));

        }

        public async Task<ResultGeneric<StudySetDto>> DeleteStudySet(
            int userId,
            int studySetId
        )
        {
            var user = await _userRepo.GetById(userId);

            if (user is null)
            {
                _logger.LogInformation($"User with id {userId} does not exist.");
                return ResultGeneric<StudySetDto>.Failure(
                    StudySetError.UserNotFoundError
                );
            }

            var studySet = await _studySetRepo.GetByIdAsync(studySetId);

            if (studySet is null)
            {
                _logger.LogInformation($"Study set with id {studySetId} does not exist.");
                return ResultGeneric<StudySetDto>.Failure(
                    StudySetError.StudySetNotFoundError
                );
            }

            if (studySet.CreatedById != userId)
            {
                _logger.LogInformation($"User {user.Username} is not allowed to perform this action.");
                return ResultGeneric<StudySetDto>.Failure(
                    StudySetError.StudySetAccessDeniedError
                );
            }

            _logger.LogInformation("Deleting study set....");
            await _studySetRepo.DeleteAsync(studySet);
            _logger.LogInformation("Study set deleted.");

            return ResultGeneric<StudySetDto>.Success(
                _mapper.Map<StudySetDto>(studySet)
            );

        }

        public async Task<ResultGeneric<StudySetDto>> GetStudySet(int id)
        {
            var studySet = await _studySetRepo.GetByIdAsync(id);

            if (studySet is null)
            {
                return ResultGeneric<StudySetDto>.Failure(
                    StudySetError.StudySetNotFoundError
                );
            }

            return ResultGeneric<StudySetDto>.Success(
                _mapper.Map<StudySetDto>(studySet)
            );
        }
    }
}

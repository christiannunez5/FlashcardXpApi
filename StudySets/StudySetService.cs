using AutoMapper;
using FlashcardXpApi.Auth;
using FlashcardXpApi.Common.Results;
using FlashcardXpApi.StudySets.Requests;
using FlashcardXpApi.Users;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace FlashcardXpApi.FlashcardSets
{
    public class StudySetService
    {
        private readonly IStudySetRepository _studySetRepo;
        private readonly AuthService _authService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly StudySetRequestValidator 
            _validator;
        private readonly UserManager<User> _userManager;

        public StudySetService(IStudySetRepository studySetRepo,
                               AuthService authService,
                               ILogger<AuthService> logger,
                               IMapper mapper,
                               StudySetRequestValidator validator,
                               UserManager<User> userManager)
        {
            _studySetRepo = studySetRepo;
            _authService = authService;
            _logger = logger;
            _mapper = mapper;
            _validator = validator;
            _userManager = userManager;
        }

        public async Task<ResultGeneric<List<StudySetDto>>> GetAllByUser()
        {
            var user = await _authService.GetLoggedInUser();

            if (user is null)
            {
                return ResultGeneric<List<StudySetDto>>.Failure(
                    StudySetErrors.StudySetAccessDeniedError
                );
            };

            var studySets = await _studySetRepo.GetAllByUser(user);

            return ResultGeneric<List<StudySetDto>>.Success(
                _mapper.Map<List<StudySetDto>>(studySets)
            );

        }
        
        public async Task<ResultGeneric<StudySetDto>> AddNewStudySet(
            StudySetRequest request
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
                    StudySetErrors.ValidationError(errorMessage)
                );
            }

            var user = await _authService.GetLoggedInUser();

            if (user is null)
            {
                return ResultGeneric<StudySetDto>.Failure(
                    StudySetErrors.StudySetAccessDeniedError
                );
            };

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
       
        
        public async Task<ResultGeneric<StudySetDto>> UpdateStudySet (
            string studySetId,
            StudySetRequest request)
        {

            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errorMessage = validationResult.Errors
                    .Select(x => x.ErrorMessage)
                    .First();

                return ResultGeneric<StudySetDto>.Failure(
                    StudySetErrors.ValidationError(errorMessage)
                );
            }

            var user = await _authService.GetLoggedInUser();

            if (user is null)
            {
                return ResultGeneric<StudySetDto>.Failure(
                    StudySetErrors.StudySetAccessDeniedError
                );
            };

            var studySet = await _studySetRepo.GetByIdAsync(studySetId);

            if (studySet is null)
            {
                _logger.LogInformation($"Study set with id {studySetId} does not exist.");
                return ResultGeneric<StudySetDto>.Failure(
                    StudySetErrors.StudySetNotFoundError
                );
            }
             
            if (!studySet.IsParticipant(user.Id))
            {
                _logger.LogInformation($"User {user.UserName} is not authorized to perform this action.");
                return ResultGeneric<StudySetDto>.Failure(
                    StudySetErrors.StudySetAccessDeniedError
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


        public async Task<Result> AddUserToStudySet(string studySetId, string userId)
        {
            var studySet = await _studySetRepo.GetByIdAsync(studySetId);

            if (studySet is null)
            {
                return Result.Failure(StudySetErrors.StudySetNotFoundError);
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
            {
                return Result.Failure(StudySetErrors.UserNotFoundError);
            }

            if (studySet.IsParticipant(userId))
            {
                return Result.Failure(StudySetErrors.UserIsAlreadyParticipant);
            }

            studySet.AddParticipant(user);
            await _studySetRepo.UpdateAsync(studySet);

            return Result.Success;
        }


        public async Task<Result> DeleteStudySet(string studySetId)
        {
            var studySet = await _studySetRepo.GetByIdAsync(studySetId);

            if (studySet is null)
            {
                return Result.Failure(StudySetErrors.StudySetNotFoundError);
            }

            await _studySetRepo.DeleteAsync(studySet);

            return Result.Success;
        }

        /*
        public async Task<ResultGeneric<StudySetDto>> DeleteStudySet(
            string userId,
            int studySetId
        )
        {
            var user = await _userRepo.GetById(userId);

            if (user is null)
            {
                _logger.LogInformation($"User with id {userId} does not exist.");
                return ResultGeneric<StudySetDto>.Failure(
                    StudySetErrors.UserNotFoundError
                );
            }

            var studySet = await _studySetRepo.GetByIdAsync(studySetId);

            if (studySet is null)
            {
                _logger.LogInformation($"Study set with id {studySetId} does not exist.");
                return ResultGeneric<StudySetDto>.Failure(
                    StudySetErrors.StudySetNotFoundError
                );
            }

            if (studySet.CreatedById != userId)
            {
                _logger.LogInformation($"User {user.UserName} is not allowed to perform this action.");
                return ResultGeneric<StudySetDto>.Failure(
                    StudySetErrors.StudySetAccessDeniedError
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
                    StudySetErrors.StudySetNotFoundError
                );
            }

            return ResultGeneric<StudySetDto>.Success(
                _mapper.Map<StudySetDto>(studySet)
            );
        }

        */
    }
}

using AutoMapper;
using FlashcardXpApi.Auth;
using FlashcardXpApi.Auth.Interfaces;
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

        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly StudySetRequestValidator 
            _validator;
        private readonly UserManager<User> _userManager;

        public StudySetService(IStudySetRepository studySetRepo,
                               ILogger<AuthService> logger,
                               IMapper mapper,
                               StudySetRequestValidator validator,
                               UserManager<User> userManager,
                               ICurrentUserService currentUserService)
        {
            _studySetRepo = studySetRepo;
            _logger = logger;
            _mapper = mapper;
            _validator = validator;
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<ResultGeneric<List<StudySetDto>>> GetAllByUser()
        {
            var user = await _currentUserService.GetCurrentUser();

            if (user is null)
            {
                return ResultGeneric<List<StudySetDto>>.Failure(
                    StudySetErrors.AuthenticationRequiredError
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

            var user = await _currentUserService.GetCurrentUser();

            if (user is null)
            {
                return ResultGeneric<StudySetDto>.Failure(
                    StudySetErrors.AuthenticationRequiredError
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

            var user = await _currentUserService.GetCurrentUser();

            if (user is null)
            {
                return ResultGeneric<StudySetDto>.Failure(
                    StudySetErrors.AuthenticationRequiredError
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

            if (studySet.CreatedById != user.Id)
            {
                _logger.LogInformation($"User {user.UserName} is not authorized to perform this action.");
                return ResultGeneric<StudySetDto>.Failure(
                    StudySetErrors.AuthorizationFailedError
                );
            }
                               
            studySet.Title = request.Title;
            studySet.IsPublic = request.IsPublic;
            studySet.Description = 
                request.Description is not null ? request.Description : studySet.Description;

            _logger.LogInformation("Updating study set....");
            await _studySetRepo.UpdateAsync(studySet);
            _logger.LogInformation("Study set updated.");

            return ResultGeneric<StudySetDto>.Success(_mapper.Map<StudySetDto>(studySet));

        }


        
        public Task<Result> AddUserToStudySet(string studySetId, string userId)
        {
            /*
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
            */

            throw new NotImplementedException();
        }

        public async Task<Result> DeleteStudySet(string studySetId)
        {
            var user = await _currentUserService.GetCurrentUser();

            if (user is null)
            {
                return Result.Failure(StudySetErrors.AuthenticationRequiredError);
            }

            var studySet = await _studySetRepo.GetByIdAsync(studySetId);

            if (studySet is null)
            {
                return Result.Failure(StudySetErrors.StudySetNotFoundError);
            }


            if (studySet.CreatedById != user.Id)
            {
                return Result.Failure(StudySetErrors.AuthorizationFailedError);
            }

            await _studySetRepo.DeleteAsync(studySet);

            return Result.Success;
        }
      
    }
}

using AutoMapper;
using FlashcardXpApi.Common.Results;
using FlashcardXpApi.Features.Auth;
using FlashcardXpApi.Features.Users;
using FlashcardXpApi.Services;
using Microsoft.AspNetCore.Identity;

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

        public StudySetService(IStudySetRepository studySetRepo,
                               ILogger<AuthService> logger,
                               IMapper mapper,
                               StudySetRequestValidator validator,
                               ICurrentUserService currentUserService)
        {
            _studySetRepo = studySetRepo;
            _logger = logger;
            _mapper = mapper;
            _validator = validator;
            _currentUserService = currentUserService;
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

        public async Task<ResultGeneric<StudySetDto>> AddNewStudySet(StudySetRequest request)
        {

            var user = await _currentUserService.GetCurrentUser();
        
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

            
            var newStudySet = new StudySet()
            {
                Title = request.Title,
                Description = request.Description,
                CreatedBy = user
            };

            await _studySetRepo.InsertAsync(newStudySet);

            return ResultGeneric<StudySetDto>.Success(_mapper.Map<StudySetDto>(newStudySet));
        }


        public async Task<ResultGeneric<StudySetDto>> UpdateStudySet(string studySetId, StudySetRequest request)
        {
            var user = await _currentUserService.GetCurrentUser();

            if (user is null)
            {
                return ResultGeneric<StudySetDto>.Failure(
                    StudySetErrors.AuthenticationRequiredError
                );
            };

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
          
            var studySet = await _studySetRepo.GetByIdAsync(studySetId);

            if (studySet is null)
            {
                return ResultGeneric<StudySetDto>.Failure(StudySetErrors.StudySetNotFoundError);
            }

            if (studySet.CreatedById != user.Id)
            {
                return ResultGeneric<StudySetDto>.Failure(StudySetErrors.AuthorizationFailedError);
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

      
    }
}

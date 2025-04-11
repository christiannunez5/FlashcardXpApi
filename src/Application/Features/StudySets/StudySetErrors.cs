using Application.Common.Models;

namespace Application.Features.StudySets;

public class StudySetErrors
{
    public static Error StudySetNotFound = 
        new Error(ErrorTypeConstant.NOT_FOUND, "Study set not found");

    public static Error NotOwner =
        new Error(ErrorTypeConstant.FORBIDDEN, "Not authorized to perform this action");
}
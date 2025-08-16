using Application.Common.Models;

namespace Application.Features.StudySets;

public class StudySetErrors
{
    public static Error StudySetNotFound = 
        new Error(ErrorTypeConstant.NOT_FOUND, "Study set not found");

    public static Error UserAlreadyStudied =
        new Error(ErrorTypeConstant.BAD_REQUEST, "User already studied this studyset.");
    
    public static Error NotOwner =
        new Error(ErrorTypeConstant.FORBIDDEN, "Not authorized to perform this action");
    
    public static Error TagAlreadyAdded =
        new Error(ErrorTypeConstant.BAD_REQUEST, "Tag is already added");

    public static Error InvalidFileTypePdf  =
        new Error(ErrorTypeConstant.BAD_REQUEST, "Upload a pdf file");
}
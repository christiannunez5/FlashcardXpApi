using Application.Common.Models;

namespace Application.Features.Tags;

public class TagErrors
{
    public static Error TagNotFound = 
        new Error(ErrorTypeConstant.NOT_FOUND, "Tag not found");
}
using Application.Common.Models;

namespace Application.Features.Folders;

public class FolderErrors
{
    public static Error FolderNotFound =
        new Error(ErrorTypeConstant.NOT_FOUND, "Folder not found");
    public static Error NotOwner = 
        new Error(ErrorTypeConstant.AUTHORIZATION_ERROR, "You are not authorized to perform this action");
}
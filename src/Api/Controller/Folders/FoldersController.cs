using Application.Extensions;
using Application.Features.Folders.Commands;
using Application.Features.Folders.Payloads;
using Application.Features.Folders.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Folders;

[Authorize]
public class FoldersController : ApiControllerBase
{
    [HttpGet]
    public async Task<IResult> GetUserFolders()
    {
        var query = new GetCurrentUserFolders.Query();
        var response = await Mediator.Send(query);
        return response.ToHttpResponse();
    }
    
    [HttpGet("{id}")]
    public async Task<IResult> GetFolder(string id)
    {
        var query = new GetFolderById.Query
        {
            FolderId = id
        };
        var response = await Mediator.Send(query);
        return response.ToHttpResponse();
    }
    
    [HttpGet("{id}/study-sets")]
    public async Task<IResult> GetStudySetsFolder(string id)
    {
        var query = new GetStudySetsByFolder.Query
        {
            FolderId = id
        };
        var response = await Mediator.Send(query);
        return response.ToHttpResponse();
    }
    
    
    [HttpPost]
    public async Task<IResult> AddNewFolder([FromBody] CreateFolderRequest request)
    {
        var query = new CreateFolder.Command
        {
            FolderId = request.FolderId,
            Name = request.Name,
        };
        var response = await Mediator.Send(query);
        return response.ToHttpResponse();
    }
    
    [HttpPatch("{id}")]
    public async Task<IResult> UpdateFolder(string id, [FromBody] RenameFolderRequest request)
    {
        var query = new RenameFolderById.Command
        {
            FolderId = id,
            Name = request.Name,
        };
        var response = await Mediator.Send(query);
        return response.ToHttpResponse();
    }

    
    
    [HttpDelete("{id}")]
    public async Task<IResult> DeleteFolder(string id)
    {
        var query = new DeleteFolderById.Command
        {
            FolderId = id
        };
        var response = await Mediator.Send(query);
        return response.ToHttpResponse();
    }
    
    
}
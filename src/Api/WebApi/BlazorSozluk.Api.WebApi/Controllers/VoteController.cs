using BlazorSozluk.Application.Features.Commands.Entry.DeleteVote;
using BlazorSozluk.Application.Features.Commands.EntryCommand.DeleteVote;
using BlazorSozluk.Common.Models;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSozluk.Api.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VoteController: BaseController
{
    private readonly IMediator _mediator;

    public VoteController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// bir entrye ait vote tepkisi oluşturur.
    /// </summary>
    /// <param name="entryId"></param>
    /// <param name="voteType"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Entry/{entryId}")]
    public async Task<IActionResult> CreateEntryVote(Guid entryId, VoteTypes voteType = VoteTypes.UpVote)
    {
        var result = await _mediator.Send(new CreateEntryVoteCommand(entryId, voteType, UserId.Value));

        return Ok(result);
    }

    /// <summary>
    /// bir entrycomment'e ait vote tepkisi oluşturur.
    /// </summary>
    /// <param name="entryCommentId"></param>
    /// <param name="voteType"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("EntryComment/{entryCommentId}")]
    public async Task<IActionResult> CreateEntryCommentVote(Guid entryCommentId, VoteTypes voteType = VoteTypes.UpVote)
    {
        var result = await _mediator.Send(new CreateEntryCommentVoteCommand(entryCommentId, voteType, UserId.Value));

        return Ok(result);
    }

    /// <summary>
    /// bir entrye ait vote tepkisini siler.
    /// </summary>
    /// <param name="entryId"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("DeleteEntryVote/{entryId}")]
    public async Task<IActionResult> DeleteEntryVote(Guid entryId)
    {
        await _mediator.Send(new DeleteEntryVoteCommand(entryId, UserId.Value));

        return Ok();
    }

    /// <summary>
    /// bir entry commente ait vote tepkisini siler.
    /// </summary>
    /// <param name="entryCommentId"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("DeleteEntryCommentVote/{entryId}")]
    public async Task<IActionResult> DeleteEntryCommentVote(Guid entryCommentId)
    {
        await _mediator.Send(new DeleteEntryCommentVoteCommand(entryCommentId, UserId.Value));

        return Ok();
    }
}
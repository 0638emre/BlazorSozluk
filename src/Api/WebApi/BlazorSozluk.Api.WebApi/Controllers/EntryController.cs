using BlazorSozluk.Application.Features.Commands.EntryCommand.Create;
using BlazorSozluk.Application.Features.Queries.Entry.GetEntries;
using BlazorSozluk.Application.Features.Queries.Entry.GetEntryComments;
using BlazorSozluk.Application.Features.Queries.Entry.GetEntryDetail;
using BlazorSozluk.Application.Features.Queries.Entry.GetMainPageEntries;
using BlazorSozluk.Application.Features.Queries.Entry.GetUserEntries;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSozluk.Api.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EntryController : BaseController
{
    private readonly IMediator _mediator;

    public EntryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Bütün entryleri getirir rastgele olarak
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetEntries([FromQuery] GetEntriesQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    /// <summary>
    /// Bir entrynin detaylarını verir.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetEntryDetailQuery(id, UserId));
        
        return Ok(result);
    }
    
    /// <summary>
    /// bir entrye ait yapılan yorumları getirir.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("Comments/{id}")]
    public async Task<IActionResult> GetEntryComments(Guid id, int page, int pageSize)
    {
        var result = await _mediator.Send(new GetEntryCommentsQuery(id, UserId, page, pageSize));
        
        return Ok(result);
    }
    
    /// <summary>
    /// bir kullanıcıya ait tüm entryleri getirir.
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="userId"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("UserEntries")]
    public async Task<IActionResult> GetUserEntries(string userName, Guid userId, int page, int pageSize)
    {
        var result = await _mediator.Send(new GetUserEntriesQuery(UserId, userName, page, pageSize));
        return Ok(result);
    }

    /// <summary>
    /// ana sayfadaki gösterilecek tüm entryleri getirir.
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("MainPageEntries")]
    public async Task<IActionResult> GetMainPageEntries(int page, int pageSize)
    {
        var result = await _mediator.Send(new GetMainPageEntriesQuery(UserId, page, pageSize));
        return Ok(result);
    }

    /// <summary>
    /// Sözlük uygulamamızda Bir ENTRY oluşturan api servisimiz.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("CreateEntry")]
    public async Task<IActionResult> CreateEntry([FromBody] CreateEntryCommand command)
    {
        if (!command.CreatedById.HasValue)
            command.CreatedById = UserId;
        
        var result = await _mediator.Send(command);

        return Ok(result);
    }

    /// <summary>
    /// Sözlük uygulamamızda Bir ENTRY COMMENT oluşturan api servisimiz.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("CreateEntryComment")]
    public async Task<IActionResult> CreateEntryComment([FromBody] CreateEntryCommentCommand command)
    {
        if (!command.CreatedById.HasValue)
            command.CreatedById = UserId;

        var result = await _mediator.Send(command);

        return Ok(result);
    }

    /// <summary>
    /// arama motoruna yazılacak olan search text
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("Search")]
    public async Task<IActionResult> Search([FromQuery] SearchEntryQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
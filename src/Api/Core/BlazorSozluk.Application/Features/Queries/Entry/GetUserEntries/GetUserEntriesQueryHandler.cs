using BlazorSozluk.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Infrastructure.Extensions;
using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Application.Features.Queries.Entry.GetUserEntries;

public class
    GetUserEntriesQueryHandler : IRequestHandler<GetUserEntriesQuery, PageViewModel<GetUserEntriesDetailViewModel>>
{
    private readonly IEntryRepository _entryRepository;

    public GetUserEntriesQueryHandler(IEntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    public async Task<PageViewModel<GetUserEntriesDetailViewModel>> Handle(GetUserEntriesQuery request,
        CancellationToken cancellationToken)
    {
        var query = _entryRepository.AsQueryable();

        if (request != null && request.UserId.HasValue && request.UserId != Guid.Empty)
        {
            query = query.Where(x => x.CreatedById == request.UserId.Value);
        }
        else if (!string.IsNullOrEmpty(request.UserName))
        {
            query = query.Where(i => i.CreatedBy.UserName == request.UserName);
        }
        else return null;

        query = query.Include(i => i.EntryFavorites)
            .Include(i => i.CreatedBy);

        var list = query.Select(i => new GetUserEntriesDetailViewModel
        {
            Id = i.Id,
            Subject = i.Subject,
            Content = i.Content,
            IsFavorited = false,
            FavoritedCount = i.EntryFavorites.Count,
            CreatedDate = i.CreatedDate,
            CreatedByUserName = i.CreatedBy.UserName
        });

        var entries = await list.GetPaged(request.Page, request.PageSize);

        return entries;
    }
}
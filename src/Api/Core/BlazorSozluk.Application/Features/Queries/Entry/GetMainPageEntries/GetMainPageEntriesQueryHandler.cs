﻿using BlazorSozluk.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Infrastructure.Extensions;
using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Application.Features.Queries.Entry.GetMainPageEntries;

public class GetMainPageEntriesQueryHandler : IRequestHandler<GetMainPageEntriesQuery, PageViewModel<GetEntryDetailViewModel>>
{
    private readonly IEntryRepository _entryRepository;

    public GetMainPageEntriesQueryHandler(IEntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }
    public async Task<PageViewModel<GetEntryDetailViewModel>> Handle(GetMainPageEntriesQuery request, CancellationToken cancellationToken)
    {
        var query = _entryRepository.AsQueryable();
        
        query = query.Include(i => i.EntryFavorites)
            .Include(i => i.CreatedBy)
            .Include(i => i.EntryVotes);
        

        
        var list = query.Select(i => new GetEntryDetailViewModel()
        {
            Id = i.Id,
            Subject = i.Subject,
            Content = i.Content,
            IsFavorited = request.UserId.HasValue && i.EntryFavorites.Any(j => j.CreatedById == request.UserId),
            FavoritedCount = i.EntryFavorites.Count,
            CreatedDate = i.CreatedDate,
            CreatedByUserName = i.CreatedBy.UserName,
            VoteTypes =
                request.UserId.HasValue && i.EntryVotes.Any(j => j.CreatedById == request.UserId)
                    ? i.EntryVotes.FirstOrDefault(j => j.CreatedById == request.UserId).VoteType
                    : Common.Models.VoteTypes.None
        });

        var entries = await list.GetPaged(request.Page, request.PageSize);

        return entries;
    }
}
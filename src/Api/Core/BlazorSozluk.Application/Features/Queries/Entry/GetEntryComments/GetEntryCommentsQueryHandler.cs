using BlazorSozluk.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Infrastructure.Extensions;
using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Application.Features.Queries.Entry.GetEntryComments;

public class GetEntryCommentsQueryHandler : IRequestHandler<GetEntryCommentsQuery, PageViewModel<GetEntryCommentViewModel>>
{
    private readonly IEntryCommentRepository _entryCommentRepository;

    public GetEntryCommentsQueryHandler(IEntryCommentRepository entryCommentRepository)
    {
        _entryCommentRepository = entryCommentRepository;
    }

    public async Task<PageViewModel<GetEntryCommentViewModel>> Handle(GetEntryCommentsQuery request, CancellationToken cancellationToken)
    {
        var query = _entryCommentRepository.AsQueryable();
        
        query = query.Include(i => i.EntryCommentFavorites)
            .Include(i => i.CreatedBy)
            .Include(i => i.EntryCommentVotes)
            .Where(i => i.EntryId == request.EntryId);
        

        
        var list = query.Select(i => new GetEntryCommentViewModel()
        {
            Id = i.Id,
            Content = i.Content,
            IsFavorited = request.UserId.HasValue && i.EntryCommentFavorites.Any(j => j.CreatedById == request.UserId),
            FavoritedCount = i.EntryCommentFavorites.Count,
            CreatedDate = i.CreatedDate,
            CreatedByUserName = i.CreatedBy.UserName,
            VoteTypes =
                request.UserId.HasValue && i.EntryCommentVotes.Any(j => j.CreatedById == request.UserId)
                    ? i.EntryCommentVotes.FirstOrDefault(j => j.CreatedById == request.UserId).VoteType
                    : Common.Models.VoteTypes.None
        });

        var entries = await list.GetPaged(request.Page, request.PageSize);

        return entries;
    }
}
using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using MediatR;

namespace BlazorSozluk.Application.Features.Queries.Entry.GetEntryComments;

public class GetEntryCommentsQuery : BasePageQuery, IRequest<PageViewModel<GetEntryCommentViewModel>>, IRequest<GetEntryCommentViewModel>
{
    public GetEntryCommentsQuery(Guid entryId, Guid? userId, int page, int pageSize) : base(page, pageSize)
    {
        EntryId = entryId;
        UserId = userId;
    }

    public Guid EntryId { get; set; }
    public Guid? UserId { get; set; }
}
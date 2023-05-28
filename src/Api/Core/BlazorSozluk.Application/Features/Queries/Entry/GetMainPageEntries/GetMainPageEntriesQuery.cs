using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using MediatR;

namespace BlazorSozluk.Application.Features.Queries.Entry.GetMainPageEntries;

public class GetMainPageEntriesQuery : BasePageQuery, IRequest<PageViewModel<GetEntryDetailViewModel>>
{    
    public GetMainPageEntriesQuery( Guid? userId, int page, int pageSize) : base(page, pageSize)
    {
        userId = UserId;
    }

    public Guid? UserId { get; set; }


}
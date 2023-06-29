using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using MediatR;

namespace BlazorSozluk.Application.Features.Queries.Entry.GetUserEntries;

public class GetUserEntriesQuery:BasePageQuery, IRequest<PageViewModel<GetUserEntriesDetailViewModel>>
{
    public Guid? UserId { get; set; }
    public string UserName { get; set; }
    public GetUserEntriesQuery(Guid? userId, string userName = null, int page=1, int pageSize=10) : base(page, pageSize)
    {
        UserName = userName;
        UserId = userId;
    }
}
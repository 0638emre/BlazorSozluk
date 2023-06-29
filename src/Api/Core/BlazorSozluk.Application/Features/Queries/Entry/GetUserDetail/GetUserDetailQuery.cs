using BlazorSozluk.Common.Models.Queries;
using MediatR;

namespace BlazorSozluk.Application.Features.Queries.Entry.GetUserDetail;

public class GetUserDetailQuery : IRequest<UserDetailViewModel>
{
    public GetUserDetailQuery(Guid userId, string userName = null)
    {
        UserId = userId;
        UserName = userName;
    }

    public Guid UserId { get; set; }
    public string UserName { get; set; }
    
}
using BlazorSozluk.Common.Models.Queries;
using MediatR;

namespace BlazorSozluk.Application.Features.Queries.Entry.GetEntries;

public class GetEntriesQuery : IRequest<List<GetEntriesViewModel>>
{
    public bool TodaysEntries { get; set; }
    public int Count { get; set; } = 100;
}
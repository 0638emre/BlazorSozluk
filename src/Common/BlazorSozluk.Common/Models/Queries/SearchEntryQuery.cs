using MediatR;

namespace BlazorSozluk.Common.Models.Queries;

public class SearchEntryQuery : IRequest<List<SearchEntryViewModel>>
{
    public SearchEntryQuery(string searchText)
    {
        SearchText = searchText;
    }

    public SearchEntryQuery()
    {
        
    }

    public string SearchText { get; set; }
}
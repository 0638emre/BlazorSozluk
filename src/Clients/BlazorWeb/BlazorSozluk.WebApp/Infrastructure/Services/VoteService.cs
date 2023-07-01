using BlazorSozluk.Common.Models;
using BlazorSozluk.WebApp.Infrastructure.Services.Interfaces;

namespace BlazorSozluk.WebApp.Infrastructure.Services;

public class VoteService : IVoteService
{
    private readonly HttpClient _client;

    public VoteService(HttpClient client)
    {
        _client = client;
    }

    public async Task DeleteEntryVote(Guid entryId)
    {
        var response = await _client.PostAsync($"/api/Vote/DeleteEntryVote/{entryId}", null);
        if (!response.IsSuccessStatusCode)
            throw new Exception("DeleteEntryVote error");
    }

    public async Task DeleteEntryCommentVote(Guid entryCommentId)
    {
        var response = await _client.PostAsync($"/api/Vote/DeleteEntryCommentVote/{entryCommentId}", null);
        if (!response.IsSuccessStatusCode)
            throw new Exception("DeleteEntryCommentVote error");
    }
    
    public async Task CreateEntryUpVote(Guid entryId)
    {
        await CreateEntryVote(entryId, VoteTypes.UpVote);
    }
    
    public async Task CreateEntryDownVote(Guid entryId)
    {
        await CreateEntryVote(entryId, VoteTypes.DownVote);
    }

    private async Task<HttpResponseMessage> CreateEntryVote(Guid entryId, VoteTypes voteTypes = VoteTypes.UpVote)
    {
        var response = await _client.PostAsync($"/api/Vote/entry/{entryId}?voteType={voteTypes}", null);
        
        //TODO : check succeess code

        return response;
    }
    
    public async Task CreateEntryCommentUpVote(Guid entryCommentId)
    {
        await CreateEntryCommentVote(entryCommentId, VoteTypes.UpVote);
    }
    
    public async Task CreateEntryCommentDownVote(Guid entryCommentId)
    {
        await CreateEntryCommentVote(entryCommentId, VoteTypes.DownVote);
    }
    
    private async Task<HttpResponseMessage> CreateEntryCommentVote(Guid entrycommentId, VoteTypes voteTypes = VoteTypes.UpVote)
    {
        var response = await _client.PostAsync($"/api/Vote/entrycomment/{entrycommentId}?voteType={voteTypes}", null);
        
        //TODO : check succeess code

        return response;
    }
}
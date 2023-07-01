namespace BlazorSozluk.WebApp.Infrastructure.Services.Interfaces;

public interface IVoteService
{
    Task DeleteEntryVote(Guid entryId);
    Task DeleteEntryCommentVote(Guid entryCommentId);
    Task CreateEntryDownVote(Guid entryId);
    Task CreateEntryUpVote(Guid entryId);
    Task CreateEntryCommentUpVote(Guid entryCommentId);
    Task CreateEntryCommentDownVote(Guid entryCommentId);
}
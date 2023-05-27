using BlazorSozluk.Common.Models;

namespace BlazorSozluk.Common.Events.EntryComment;

public class CreateEntryCommentVoteEvent
{
    public Guid EntryCommentId { get; set; }
    public VoteTypes VoteTypes { get; set; }
    public Guid CreatedBy { get; set; }
}
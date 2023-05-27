using MediatR;

namespace BlazorSozluk.Common.Models.RequestModels;

public class CreateEntryCommentVoteCommand: IRequest<bool>
{
    public Guid EntryCommentId { get; set; }

    public VoteTypes VoteType { get; set; }

    public Guid CreatedBy { get; set; }

    public CreateEntryCommentVoteCommand()
    {

    }

    public CreateEntryCommentVoteCommand(Guid entryCommentId, VoteTypes voteType, Guid createdBy)
    {
        EntryCommentId = entryCommentId;
        VoteType = voteType;
        CreatedBy = createdBy;
    }
}
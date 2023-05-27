using MediatR;

namespace BlazorSozluk.Common.Models.RequestModels;

public class CreateEntryVoteCommand : IRequest<bool>
{
    public Guid EntryId { get; set; }

    public VoteTypes VoteType { get; set; }

    public Guid CreatedBy { get; set; }

    public CreateEntryVoteCommand(Guid entryId, VoteTypes voteType, Guid createdBy)
    {
        EntryId = entryId;
        VoteType = voteType;
        CreatedBy = createdBy;
    }
}
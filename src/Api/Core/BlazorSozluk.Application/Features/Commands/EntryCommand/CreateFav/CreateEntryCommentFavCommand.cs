using MediatR;

namespace BlazorSozluk.Application.Features.Commands.EntryCommand.CreateFav;

public class CreateEntryCommentFavCommand : IRequest<bool>
{
    public Guid EntryCommentId { get; set; }
    public Guid UserId { get; set; }

    public CreateEntryCommentFavCommand(Guid entryCommentId, Guid userId)
    {
        EntryCommentId = entryCommentId;
        UserId = userId;
    }
}
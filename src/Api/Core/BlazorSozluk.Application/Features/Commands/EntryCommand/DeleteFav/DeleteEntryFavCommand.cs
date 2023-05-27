using MediatR;

namespace BlazorSozluk.Application.Features.Commands.EntryCommand.DeleteFav;

public class DeleteEntryFavCommand : IRequest<bool>
{
    public Guid EntryId { get; set; }

    public Guid UserId { get; set; }

    public DeleteEntryFavCommand(Guid entryId, Guid userId)
    {
        EntryId = entryId;
        UserId = userId;
    }
}
using MediatR;

namespace BlazorSozluk.Application.Features.Commands.EntryCommand.Create;

public class CreateEntryCommentCommand : IRequest<bool>
{
    public Guid? EntryId { get; set; }
    public string Content { get; set; }
    public Guid? CreatedById { get; set; }

    public CreateEntryCommentCommand()
    {
        
    }

    public CreateEntryCommentCommand(Guid? entryId, string content, Guid? createdById)
    {
        EntryId = entryId;
        Content = content;
        CreatedById = createdById;
    }
}
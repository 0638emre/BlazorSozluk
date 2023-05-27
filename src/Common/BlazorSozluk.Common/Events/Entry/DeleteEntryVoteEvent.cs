namespace BlazorSozluk.Common.Events.Entry;

public class DeleteEntryVoteEvent
{
    public Guid CreatedBy { get; set; }
    public Guid EntryId { get; set; }
}
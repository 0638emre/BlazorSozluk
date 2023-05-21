using BlazorSozluk.Common.Models;

namespace BlazorSozluk.Domain.Models
{
    public class EntryVote:BaseEntity
    {
        public Guid EntryId {get; set; }
        public VoteTypes VoteType { get; set; }
        public Guid CreatedById { get; set; }
        public virtual Entry Entry {get; set; }
    }
}

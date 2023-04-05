using BlazorSozluk.Common.ViewModels;

namespace BlazorSozluk.Domain.Models
{
    public class EntryCommentVote : BaseEntity
    {
        public Guid EntryCommentVoteId { get; set; }
        public VoteTypes VoteType { get; set; }
        public Guid CreatedById { get; set; }
        public virtual EntryComment EntryComment {get; set; }
    }
}

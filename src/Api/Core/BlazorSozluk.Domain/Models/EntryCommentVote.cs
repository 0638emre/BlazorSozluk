﻿using BlazorSozluk.Common.Models;

namespace BlazorSozluk.Domain.Models
{
    public class EntryCommentVote : BaseEntity
    {
        public Guid EntryCommentId { get; set; }
        public VoteTypes VoteType { get; set; }
        public Guid CreatedById { get; set; }
        public virtual EntryComment EntryComment {get; set; }
    }
}

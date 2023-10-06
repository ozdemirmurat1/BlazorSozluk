﻿using BlazorSozluk.Common.Models;

namespace BlazorSozluk.Common.Events.Entry
{
    public class CreateEntryVoteEvent
    {
        public Guid EntryId { get; set; }
        public VoteType VoteType { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
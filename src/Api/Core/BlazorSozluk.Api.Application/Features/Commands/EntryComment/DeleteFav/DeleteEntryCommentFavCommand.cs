﻿using MediatR;

namespace BlazorSozluk.Api.Application.Features.Commands.EntryComment.DeleteFav
{
    public class DeleteEntryCommentFavCommand:IRequest<bool>
    {
        public DeleteEntryCommentFavCommand(Guid entryCommentId, Guid userId)
        {
            EntryCommentId = entryCommentId;
            UserId = userId;
        }

        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }
    }
}

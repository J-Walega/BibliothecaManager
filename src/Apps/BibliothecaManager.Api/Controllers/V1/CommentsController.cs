using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Comments.Commands.Create;
using BibliothecaManager.Application.Comments.Commands.Delete;
using BibliothecaManager.Application.Comments.Queries.GetBookComments;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliothecaManager.Api.Controllers.V1;

/// <summary>
/// Comments
/// </summary>
public class CommentsController : BaseApiController
{
    /// <summary>
    /// Gets comments for provided bookId
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<ServiceResult<List<CommentDto>>>> GetBookComments(int id, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetBookCommentsQuery { BookId = id}, cancellationToken));
    }

    /// <summary>
    /// Creates new comment
    /// </summary>
    /// <param name="bookId"></param>
    /// <param name="score"></param>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{bookId}")]
    [Authorize]
    public async Task<ActionResult<ServiceResult<CommentDto>>> PostCommentForBook(int bookId,float score, string content, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new CreateCommentForBookCommand(score, content) { BookId = bookId}, cancellationToken));
    }

    /// <summary>
    /// Deletes comment 
    /// </summary>
    /// <param name="commentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{commentId}")]
    [Authorize(Roles = "Administrator, Employee")]
    public async Task<ActionResult<ServiceResult<CommentDto>>> DeleteComment(int commentId, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new DeleteCommentCommand(commentId), cancellationToken));
    }
}

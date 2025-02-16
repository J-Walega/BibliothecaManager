using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using BibliothecaManager.Application.BookItems.Commands.Create;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using BibliothecaManager.Application.BookItems.Queries;

namespace BibliothecaManager.Api.Controllers.V1;

/// <summary>
/// Book Item Controller
/// </summary>
public class BookItemsController : BaseApiController
{
    /// <summary>
    /// Add book item to specified book
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Administrator, Employee")]
    public async Task<ActionResult<ServiceResult<BookItemDto>>> AddBookItem(CreateBookItemCommand command, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }

    /// <summary>
    /// Gets all book items
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<ServiceResult<List<BookItemDto>>>> GetAllBookItems(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetAllBookItemsQuery(), cancellationToken));
    }

    /// <summary>
    /// Gets statuses of instances of book item by provided book id
    /// </summary>
    /// <param name="bookId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{bookId}")]
    public async Task<ActionResult<ServiceResult<List<BookItemDto>>>> GetBookItemStatuses(int bookId, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetBookItemStatusesQuery { Id = bookId}, cancellationToken));
    }

    [HttpGet("{bookId}/active")]
    public async Task<ActionResult<ServiceResult<BookItemDto>>> GetCurrentBookItemStatus(int bookId, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetBookItemActiveStatusQuery {  BookItemId = bookId}, cancellationToken));
    }
}

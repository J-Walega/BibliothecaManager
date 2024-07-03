using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Borrows.Commands.Create;
using BibliothecaManager.Application.Borrows.Commands.Update;
using BibliothecaManager.Application.Borrows.Queries.GetBorrows;
using BibliothecaManager.Application.Borrows.Queries.GetUserBorrows;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using BibliothecaManager.Domain.Entities.LibraryEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliothecaManager.Api.Controllers.V1;

/// <summary>
/// Borrows
/// </summary>
public class BorrowsController : BaseApiController
{
    /// <summary>
    /// Gets all borrows in database
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles ="Administrator, Employee")]
    public async Task<ActionResult<ServiceResult<List<BorrowDto>>>> GetAllBorrows(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetAllBorrowsQuery(), cancellationToken));
    }

    /// <summary>
    /// Gets user borrows, where id is UserId
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet("{userId}")]
    [Authorize(Roles = "Administrator, Employee")]
    public async Task<ActionResult<ServiceResult<Collection<BorrowDto>>>> GetBorrowsByUserId(string userId, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetBorrowsByUserIdQuery(userId), cancellationToken));
    }

    /// <summary>
    /// Gets user borrows
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("my")]
    [Authorize]
    public async Task<ActionResult<ServiceResult<List<BorrowDto>>>> GetMyBorrows(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetMyBorrowsQuery(), cancellationToken));
    }

    /// <summary>
    /// Creates borrow on provided bookItem to provided user
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="bookItemId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Administrator, Employee")]
    public async Task<ActionResult<ServiceResult<BorrowDto>>> CreateBorrow(int bookItemId, string userId, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new CreateBorrowCommand(bookItemId, userId), cancellationToken));
    }

    /// <summary>
    /// Extends borrow for another month for other users
    /// Admin and Employee only
    /// </summary>
    /// <param name="borrowId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPatch]
    [Authorize(Roles = "Administrator, Employee")]
    public async Task<ActionResult<ServiceResult<BorrowDto>>> ExtendBorrow(int borrowId, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new ExtendBorrowCommand(borrowId), cancellationToken));
    }

    /// <summary>
    /// Extends borrow that belong to requester for another month
    /// </summary>
    /// <param name="borrowId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPatch("{borrowId}")]
    [Authorize(Roles ="Administrator, Employee, User")]
    public async Task<ActionResult<ServiceResult<BorrowDto>>> ExtendMyBorrow(int borrowId, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new ExtendMyBorrowCommand(borrowId), cancellationToken));
    }
}
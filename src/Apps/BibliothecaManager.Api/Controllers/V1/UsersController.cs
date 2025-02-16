using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.ApplicationUsers.User.Commands.Create;
using BibliothecaManager.Application.ApplicationUsers.User.Commands.Delete;
using BibliothecaManager.Application.ApplicationUsers.User.Queries;
using BibliothecaManager.Application.ApplicationUsers.User.Queries.GetAllUsers;
using BibliothecaManager.Application.ApplicationUsers.User.Queries.GetUserInfo;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.ApplicationUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliothecaManager.Api.Controllers.V1;

/// <summary>
/// Users endpoints, requires Administrator or Employee
/// </summary>
public class UsersController : BaseApiController
{
    /// <summary>
    /// Gets information about user with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Administrator, Employee")]
    public async Task<ActionResult<ServiceResult<UserInfoResponse>>> GetInfo(string id, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetUserInfo(id), cancellationToken));
    }

    /// <summary>
    /// Gets current user info
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ServiceResult<ApplicationUserDto>>> GetCurrentUserInfo(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetCurrentUserInfoQuery(), cancellationToken));
    }

    /// <summary>
    /// Creates user with "User" role
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Administrator, Employee")]
    public async Task<ActionResult<ServiceResult<CreateApplicationUserDto>>> CreateUser(CreateUserCommand query, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(query, cancellationToken));
    }

    /// <summary>
    /// Gets all users
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("all")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<ServiceResult<ApplicationUserDto>>> GetAllUsers(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetAllUsersQuery(), cancellationToken));
    }

    /// <summary>
    /// Deletes user with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<ServiceResult<ApplicationUserDto>>> DeleteUser(string id, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new DeleteUserCommand(id), cancellationToken));
    }
}

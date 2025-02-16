using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.ApplicationUsers.User.Queries.GetToken;
using BibliothecaManager.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliothecaManager.Api.Controllers.V1;

/// <summary>
/// Login
/// </summary>
public class LoginController : BaseApiController
{
    /// <summary>
    /// Returns JWT Token on successful login
    /// </summary>
    /// <param name="tokenQuery"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<ServiceResult<LoginResponse>>> Login(GetTokenQuery tokenQuery, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(tokenQuery, cancellationToken));
    }
}

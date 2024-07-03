using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Authors.Commands.Create;
using BibliothecaManager.Application.Authors.Commands.Delete;
using BibliothecaManager.Application.Authors.Commands.Update;
using BibliothecaManager.Application.Authors.Queries;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliothecaManager.Api.Controllers.V1;

/// <summary>
/// Authors
/// </summary>
public class AuthorsController : BaseApiController
{
    /// <summary>
    /// Get all authors
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<ServiceResult<AuthorDto>>> GetAllAuthors(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetAllAuthorsQuery(), cancellationToken));
    }

    /// <summary>
    /// Gets information about author with provided id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResult<AuthorDto>>> GetAuthorById(int id, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetAuthorByIdQuery(), cancellationToken));
    }

    /// <summary>
    /// Create author
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles ="Administrator, Employee")]
    public async Task<ActionResult<ServiceResult<AuthorDto>>> CreateAuthor(CreateAuthorCommand command, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }

    /// <summary>
    /// Delete author with provided id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator, Employee")]
    public async Task<ActionResult<ServiceResult<AuthorDto>>> DeleteAuthor(int id, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new DeleteAuthorCommand { AuthorId = id }, cancellationToken));
    }

    /// <summary>
    /// Update author
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize(Roles = "Administrator, Employee")]
    public async Task<ActionResult<ServiceResult<AuthorDto>>> UpdateAuthor(UpdateAuthorCommand command, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }
}

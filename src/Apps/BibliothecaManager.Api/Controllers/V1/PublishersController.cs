using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using BibliothecaManager.Application.Publishers.Commands.Create;
using BibliothecaManager.Application.Publishers.Commands.Delete;
using BibliothecaManager.Application.Publishers.Commands.Update;
using BibliothecaManager.Application.Publishers.Queries;
using BibliothecaManager.Domain.Entities.LibraryEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliothecaManager.Api.Controllers.V1;

/// <summary>
/// Publisher controller
/// </summary>
public class PublishersController : BaseApiController
{
    /// <summary>
    /// Gets all Publishers
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ServiceResult<PublisherNameDto>>> GetPublishers(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetAllPublishersQuery(),cancellationToken));
    }

    /// <summary>
    /// Creates new Publisher
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles ="Administrator, Employee")]
    public async Task<ActionResult<ServiceResult<PublisherNameDto>>> CreatePublisher(string name, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new CreatePublisherCommand(name),cancellationToken));
    }

    /// <summary>
    /// Deletes publisher with given id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator, Employee")]
    public async Task<ActionResult<ServiceResult<PublisherNameDto>>> DeletePublisher(int id, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new DeletePublisherCommand(id), cancellationToken));
    }
}

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using BibliothecaManager.Application.Genres.Commands;
using BibliothecaManager.Application.Genres.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliothecaManager.Api.Controllers.V1;

/// <summary>
/// Genres
/// </summary>
public class GenresController : BaseApiController
{
    /// <summary>
    /// Adds genre
    /// </summary>
    /// <param name="genreName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Administrator, Employee")]
    public async Task<ActionResult<ServiceResult<GenreDto>>> AddGenre(string genreName, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new CreateGenreCommand(genreName), cancellationToken);
    }

    /// <summary>
    /// Gets genres
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<ServiceResult<List<GenreDto>>>> GetGenres(CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetGenresQuery(), cancellationToken);
    }

    /// <summary>
    /// Deletes genre by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator, Employee")]
    public async Task<ActionResult<ServiceResult<GenreDto>>> DeleteGenreCommand(int id, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new DeleteGenreCommand(id), cancellationToken);
    }
}

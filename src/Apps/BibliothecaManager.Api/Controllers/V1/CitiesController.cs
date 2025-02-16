using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Cities.Queries;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.ApplicationUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliothecaManager.Api.Controllers.V1;

/// <summary>
/// Cities 
/// </summary>
public class CitiesController : BaseApiController
{
    /// <summary>
    /// Gets all cities in database
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = "Administrator, Employee")]
    public async Task<ActionResult<ServiceResult<CityDto>>> GetAllCities(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetAllCitiesQuery(), cancellationToken));
    }
}

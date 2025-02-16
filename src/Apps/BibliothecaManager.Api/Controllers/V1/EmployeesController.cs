using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.ApplicationUsers.Employee.Create;
using BibliothecaManager.Application.ApplicationUsers.Employee.Delete;
using BibliothecaManager.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliothecaManager.Api.Controllers.V1;

/// <summary>
/// Controller with Employee endpoints, requires Administrator or Employee role
/// </summary>
public class EmployeesController : BaseApiController
{
    /// <summary>
    /// Create employee
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Administrator, Employee")]
    public async Task<ActionResult<ServiceResult<CreateEmployeeCommand>>> CreateEmployee(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(request, cancellationToken));
    }

    /// <summary>
    /// Deletes user with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator, Employee")]
    public async Task<ActionResult<ServiceResult<DeleteEmployeeCommand>>> DeleteEmployee(string id, CancellationToken cancellationToken)
    {
        var request = new DeleteEmployeeCommand { Id = id};

        return Ok(await Mediator.Send(request, cancellationToken));
    }
}

using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;

namespace BibliothecaManager.Application.ApplicationUsers.Employee.Delete;
public class DeleteEmployeeCommand : IRequestWrapper<Result>
{
    public string Id { get; set; }
}

public class DeleteEmployeeCommandHandler : IRequestHandlerWrapper<DeleteEmployeeCommand, Result>
{
    private readonly IIdentityService _identityService;

    public DeleteEmployeeCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<ServiceResult<Result>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var result = await _identityService.DeleteUserAsync(request.Id);
        return ServiceResult.Success(result);
    }
}
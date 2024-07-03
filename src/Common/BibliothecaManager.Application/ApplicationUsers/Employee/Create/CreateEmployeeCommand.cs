using System;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.ApplicationUser;
using MapsterMapper;

namespace BibliothecaManager.Application.ApplicationUsers.Employee.Create;
public record CreateEmployeeCommand(CreateApplicationUserDto Request) : IRequestWrapper<CreateApplicationUserDto>;

public class CreateEmployeeCommandHandler : IRequestHandlerWrapper<CreateEmployeeCommand, CreateApplicationUserDto>
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public CreateEmployeeCommandHandler(IIdentityService identityService, IMapper mapper)
    {
        _identityService = identityService;
        _mapper = mapper;
    }

    public async Task<ServiceResult<CreateApplicationUserDto>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<CreateApplicationUserDto>(request);
        var result = await _identityService.CreateEmployeeAsync(user);
        return ServiceResult.Success(_mapper.Map<CreateApplicationUserDto>(result));
    }
}
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.ApplicationUser;
using MapsterMapper;

namespace BibliothecaManager.Application.ApplicationUsers.User.Commands.Create;
public record CreateUserCommand(CreateApplicationUserDto User) : IRequestWrapper<CreateApplicationUserDto>;

public class CreateUserCommandHandler : IRequestHandlerWrapper<CreateUserCommand, CreateApplicationUserDto>
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IIdentityService identityService, IMapper mapper)
    {
        _identityService = identityService;
        _mapper = mapper;           
    }
    public async Task<ServiceResult<CreateApplicationUserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var response = await _identityService.CreateUserAsync(request.User);

        return response.Result.Succeeded == true ? ServiceResult.Success(_mapper.Map<CreateApplicationUserDto>(request.User))
            : ServiceResult.Failed<CreateApplicationUserDto>(ServiceError.CustomMessage(string.Join(", ", response.Result.Errors)));
    }
}
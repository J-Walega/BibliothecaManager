using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.ApplicationUser;
using MapsterMapper;

namespace BibliothecaManager.Application.ApplicationUsers.User.Queries;
public record GetCurrentUserInfoQuery : IRequestWrapper<ApplicationUserDto>;

public class GetCurrentuserInfoHandler : IRequestHandlerWrapper<GetCurrentUserInfoQuery, ApplicationUserDto>
{
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetCurrentuserInfoHandler(IIdentityService identityService, ICurrentUserService userService, IMapper mapper)
    {
        _identityService = identityService;
        _currentUserService = userService;
        _mapper = mapper;
    }

    public async Task<ServiceResult<ApplicationUserDto>> Handle(GetCurrentUserInfoQuery request, CancellationToken cancellationToken)
    {

        var id = _currentUserService.UserId;

        var response = await _identityService.GetAllUserInfoAsync(id);

        return ServiceResult.Success(_mapper.Map<ApplicationUserDto>(response));
    }
}

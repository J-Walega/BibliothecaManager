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

namespace BibliothecaManager.Application.ApplicationUsers.User.Queries.GetAllUsers;
public record GetAllUsersQuery : IRequestWrapper<List<ApplicationUserDto>>;

public class GetAllUsersQueryHandler : IRequestHandlerWrapper<GetAllUsersQuery, List<ApplicationUserDto>>
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IIdentityService identityService, IMapper mapper)
    {
        _identityService = identityService;
        _mapper = mapper;
    }
    public async Task<ServiceResult<List<ApplicationUserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var response = await _identityService.GetAllUsersAsync();

        return response.Count > 0 ? ServiceResult.Success(response) : ServiceResult.Failed<List<ApplicationUserDto>>(ServiceError.NotFound);
    }
}

using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;

namespace BibliothecaManager.Application.ApplicationUsers.User.Queries.GetUserInfo;
public record GetUserInfo(string UserId) : IRequestWrapper<UserInfoResponse>;

public class GetUserInfoHandler : IRequestHandlerWrapper<GetUserInfo, UserInfoResponse>
{
    private readonly IIdentityService _identityService;
    public GetUserInfoHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    public async Task<ServiceResult<UserInfoResponse>> Handle(GetUserInfo request, CancellationToken cancellationToken)
    {
        var user = await _identityService.GetAllUserInfoAsync(request.UserId);

        return user != null
            ? ServiceResult.Success(new UserInfoResponse
                {
                    User = user,
                })
            : ServiceResult.Failed<UserInfoResponse>(ServiceError.NotFound);
    }
}

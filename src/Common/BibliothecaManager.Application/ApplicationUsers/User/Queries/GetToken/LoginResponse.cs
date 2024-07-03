using BibliothecaManager.Application.Dto.ApplicationUser;

namespace BibliothecaManager.Application.ApplicationUsers.User.Queries.GetToken;

public class LoginResponse
{
    public ApplicationUserDto User { get; set; }

    public string Token { get; set; }
}

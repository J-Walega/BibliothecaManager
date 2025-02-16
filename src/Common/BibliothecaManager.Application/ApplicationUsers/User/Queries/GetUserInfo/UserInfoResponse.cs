using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliothecaManager.Application.Dto.ApplicationUser;

namespace BibliothecaManager.Application.ApplicationUsers.User.Queries.GetUserInfo;
public class UserInfoResponse
{
    public ApplicationUserAllDto User { get; set; }
}

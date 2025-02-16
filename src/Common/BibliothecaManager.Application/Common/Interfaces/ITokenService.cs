using System.Collections;
using System.Collections.Generic;

namespace BibliothecaManager.Application.Common.Interfaces;

public interface ITokenService
{
    string CreateJwtSecurityToken(string id, IList<string> roles);
}

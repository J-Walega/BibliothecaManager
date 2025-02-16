using System.Collections.Generic;
using System.Threading.Tasks;
using BibliothecaManager.Application.ApplicationUsers.Employee.Create;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.ApplicationUser;

namespace BibliothecaManager.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string> GetUserNameAsync(string userId);

    Task<ApplicationUserDto> CheckUserPassword(string userName, string password);

    Task<(Result Result, CreateApplicationUserDto)> CreateUserAsync(CreateApplicationUserDto user);
    Task<(Result Result, CreateApplicationUserDto)> CreateEmployeeAsync(CreateApplicationUserDto employee);

    Task<bool> UserIsInRole(string userId, string role);
    Task<bool> ChangeUserPassword(string userId, string oldPassword, string newPassword);

    Task<Result> DeleteUserAsync(string userId);

    Task<ApplicationUserAllDto> GetAllUserInfoAsync(string userId);
    Task<List<ApplicationUserDto>> GetAllUsersAsync();
}

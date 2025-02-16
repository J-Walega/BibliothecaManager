using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Exceptions;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.ApplicationUser;
using BibliothecaManager.Domain.Entities.LibraryEntities;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BibliothecaManager.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public IdentityService(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<string> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

        return user == null ?  "User not found" : $"{user.Name} {user.Surname}";
    }

    public async Task<ApplicationUserDto> CheckUserPassword(string email, string password)
    {
        ApplicationUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (user != null && await _userManager.CheckPasswordAsync(user, password))
        {
            var userDTO = _mapper.Map<ApplicationUserDto>(user);
            userDTO.Roles = await _userManager.GetRolesAsync(user);

            return userDTO;
        }

        return null;
    }

    public async Task<bool> ChangeUserPassword(string userId, string oldPassword, string newPassword)
    {
        ApplicationUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
        await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        return true;
    }

    public async Task<(Result Result, CreateApplicationUserDto)> CreateUserAsync(CreateApplicationUserDto user)
    {
        var newUser = new ApplicationUser
        {
            UserName = user.Email,
            Email = user.Email,
            Name = user.Name,
            Surname = user.Surname,
            City = _mapper.Map<City>(user.City),
            Street = user.Street,
            BuildingNumber = user.BuildingNumber,
            HouseNumber = user.HouseNumber,
            PESEL = user.PESEL,
        };
        string password = user.PESEL.ToString();

        var result = await _userManager.CreateAsync(newUser, password);
        await _userManager.AddToRoleAsync(newUser, "User");

        return (result.ToApplicationResult(), _mapper.Map<CreateApplicationUserDto>(result));
    }

    public async Task<bool> UserIsInRole(string userId, string role)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);

        return await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    private async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    public async Task<ApplicationUserAllDto> GetAllUserInfoAsync(string userId)
    {
        ApplicationUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user != null)
        {
            var userDTO = _mapper.Map<ApplicationUserAllDto>(user);
            userDTO.Roles = await _userManager.GetRolesAsync(user);

            return userDTO;
        }

        return null;
    }

    public async Task<(Result Result, CreateApplicationUserDto)> CreateEmployeeAsync(CreateApplicationUserDto employee)
    {
        var user = new ApplicationUser
        {
            UserName = employee.Email,
            Email = employee.Email,
            Name = employee.Name,
            Surname = employee.Surname,
            City = _mapper.Map<City>(employee.City),
            Street = employee.Street,
            BuildingNumber = employee.BuildingNumber,
            HouseNumber = employee.HouseNumber,
            PESEL = employee.PESEL,
        };

        string password = employee.PESEL.ToString();

        var result = await _userManager.CreateAsync(user, password);
        await _userManager.AddToRoleAsync(user, "Employee");

        return (result.ToApplicationResult(), _mapper.Map<CreateApplicationUserDto>(result));
    }

    public async Task<List<ApplicationUserDto>> GetAllUsersAsync()
    {
        var result = await _userManager.Users.ToListAsync();

        var users = new List<ApplicationUserDto>();
        foreach (var user in result)
        {
            var entity = _mapper.Map<ApplicationUserDto>(user);
            entity.Roles = await _userManager.GetRolesAsync(user);
            users.Add(entity);
        }

        return users;
    }
}

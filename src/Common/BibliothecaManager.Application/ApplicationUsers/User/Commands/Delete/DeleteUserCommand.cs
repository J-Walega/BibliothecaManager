using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Dto.ApplicationUser;

namespace BibliothecaManager.Application.ApplicationUsers.User.Commands.Delete;
public record DeleteUserCommand(string userId) : IRequestWrapper<ApplicationUserDto>;

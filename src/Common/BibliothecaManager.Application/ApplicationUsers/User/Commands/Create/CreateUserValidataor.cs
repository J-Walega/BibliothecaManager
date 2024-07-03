using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace BibliothecaManager.Application.ApplicationUsers.User.Commands.Create;
public class CreateUserValidataor : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidataor()
    {
        RuleFor(x => x.User.Email).NotEmpty();
        RuleFor(x => x.User.PESEL).NotEmpty();
    }
}

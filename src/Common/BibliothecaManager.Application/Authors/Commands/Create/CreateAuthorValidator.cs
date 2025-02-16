using FluentValidation;

namespace BibliothecaManager.Application.Authors.Commands.Create;
public class CreateAuthorValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name can not be empty");
        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Surname can not be empty");
    }
}

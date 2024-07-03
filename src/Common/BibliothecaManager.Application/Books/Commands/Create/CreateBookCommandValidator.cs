using FluentValidation;

namespace BibliothecaManager.Application.Books.Commands.Create;
public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Title can not be empty");
    }

}

using FluentValidation;

namespace BibliothecaManager.Application.Books.Commands.Update;
public class UpdateBookValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title can not be empty");

        RuleFor(x => x.BookId).NotNull();
    }
}

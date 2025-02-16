using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace BibliothecaManager.Application.Books.Queries.GetBooks;
public class GetBooksWithPaginationQueryValidatior : AbstractValidator<GetBooksWithPaginationQuery>
{
    public GetBooksWithPaginationQueryValidatior()
    {
        RuleFor(v => v.Page)
            .NotEmpty().WithMessage("You have to provide page number");

        RuleFor(v => v.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page number cannot be less than 1");
    }
}

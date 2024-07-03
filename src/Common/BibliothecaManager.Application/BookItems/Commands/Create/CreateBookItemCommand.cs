using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Exceptions;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using BibliothecaManager.Domain.Entities.LibraryEntities;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace BibliothecaManager.Application.BookItems.Commands.Create;
public record CreateBookItemCommand(int BookId) : IRequestWrapper<BookItemDto>;

public class CreateBookItemCommandHandler : IRequestHandlerWrapper<CreateBookItemCommand, BookItemDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public CreateBookItemCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<BookItemDto>> Handle(CreateBookItemCommand request, CancellationToken cancellationToken)
    {
        var book = await _context.Books
            .Where(x => x.Id == request.BookId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if(book != null)
        {
            var entity = new BookItem { Book = book, Library = await _context.Libraries
                .FirstOrDefaultAsync(cancellationToken) };

            await _context.BookItems.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var bookStatus = new BookItemStatus { Status = Domain.Enums.BookStatuses.Avilable, BookItemId = entity.BookId };

            await _context.BookItemStatuses.AddAsync(bookStatus, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var response = new BookItemDto { Book = _mapper.Map<BookDto>(book) };
            return ServiceResult.Success(response);
        }

        return ServiceResult.Failed<BookItemDto>(ServiceError.NotFound);
    }
}

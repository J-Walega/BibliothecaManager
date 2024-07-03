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

namespace BibliothecaManager.Application.Books.Commands.Delete;
public class DeleteBookCommand : IRequestWrapper<BookDto>
{
    public int BookId { get; set; }
}

public class DeleteBookCommandHandler : IRequestHandlerWrapper<DeleteBookCommand, BookDto>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public DeleteBookCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ServiceResult<BookDto>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Books
            .Where(x => x.Id == request.BookId)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Book), request.BookId);
        }

        _context.Books.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(_mapper.Map<BookDto>(entity));
    }
}

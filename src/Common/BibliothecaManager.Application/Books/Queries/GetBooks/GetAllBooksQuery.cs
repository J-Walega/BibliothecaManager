using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using BibliothecaManager.Domain.Entities.LibraryEntities;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace BibliothecaManager.Application.Books.Queries.GetBooks;
public record GetAllBooksQuery : IRequestWrapper<List<BookDto>>;

public class GetAllBooksQueryHandler : IRequestHandlerWrapper<GetAllBooksQuery, List<BookDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllBooksQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<BookDto>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        List<Book> list = await _context.Books
            .Include(g => g.Genres)
            .Include(a => a.Authors)
            .Include(c => c.Comments)
            .Include(d => d.Publisher)
            .ToListAsync(cancellationToken: cancellationToken);

        var entities = _mapper.Map<List<BookDto>>(list);

        return entities.Count > 0 ? ServiceResult.Success(entities) : ServiceResult.Failed<List<BookDto>>(ServiceError.NotFound);
    }
}
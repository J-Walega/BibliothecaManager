using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using BibliothecaManager.Domain.Entities.LibraryEntities;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace BibliothecaManager.Application.Books.Queries.GetBook;
public class GetBookByIdQuery : IRequestWrapper<BookDto>
{
    public int BookId { get; set; }
}

public class GetBookByIdHandler : IRequestHandlerWrapper<GetBookByIdQuery, BookDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBookByIdHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ServiceResult<BookDto>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _context.Books
            .Where(x => x.Id == request.BookId)
            .Include(g => g.Genres)
            .Include(a => a.Authors)
            .Include(c => c.Comments)
            .Include(d => d.Publisher)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        var result = _mapper.Map<BookDto>(book);

        return result != null ? ServiceResult.Success(result) : ServiceResult.Failed<BookDto>(ServiceError.NotFound);
    }
}

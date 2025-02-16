using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace BibliothecaManager.Application.Books.Queries.GetBooksByQuery;
public record GetBooksByQuery(string Query) : IRequestWrapper<List<BookDto>>;

public class GetBooksByQueryHandler : IRequestHandlerWrapper<GetBooksByQuery, List<BookDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    public GetBooksByQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ServiceResult<List<BookDto>>> Handle(GetBooksByQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.Books
            .Where(q => q.Title.ToLower().Trim().Contains(request.Query.Trim().ToLower()))
            .Include(g => g.Genres)
            .Include(a => a.Authors)
            .Include(b => b.BookItems)
            .Include(c => c.Comments)
            .ToListAsync(cancellationToken: cancellationToken);

        var result = _mapper.Map<List<BookDto>>(list).ToList();

        return result.Count > 0 ? ServiceResult.Success(result) : ServiceResult.Failed<List<BookDto>>(ServiceError.NotFound);
    }
}
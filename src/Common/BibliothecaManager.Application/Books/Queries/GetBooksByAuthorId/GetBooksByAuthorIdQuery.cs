using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Mapping;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace BibliothecaManager.Application.Books.Queries.GetBooksByAuthorId;
public class GetBooksByAuthorIdQuery : IRequestWrapper<List<BookDto>>
{
    public int AuthorId { get; set; }
}

public class GetBooksByAuthorIdQueryHandler : IRequestHandlerWrapper<GetBooksByAuthorIdQuery, List<BookDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetBooksByAuthorIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ServiceResult<List<BookDto>>> Handle(GetBooksByAuthorIdQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.Books
            .Include(book => book.Authors
                .Where(id => id.Id == request.AuthorId))
            .ProjectToListAsync<BookDto>(_mapper.Config, cancellationToken);

        return list.Count != 0 ? ServiceResult.Success(list) : ServiceResult.Failed<List<BookDto>>(ServiceError.NotFound);
    }
}

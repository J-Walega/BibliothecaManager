using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Mapping;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using BibliothecaManager.Domain.Entities.LibraryEntities;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace BibliothecaManager.Application.Books.Queries.GetBooks;

public class GetBooksWithPaginationQuery : IRequestWrapper<List<BookDto>>
{
    public int Page { get; set; }
}

public class GetBooksWithPaginationQueryHandler : IRequestHandlerWrapper<GetBooksWithPaginationQuery, List<BookDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBooksWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<BookDto>>> Handle(GetBooksWithPaginationQuery request, CancellationToken cancellationToken)
    {
        if (request.Page.Equals(1))
        {
            List<Book> list = await _context.Books
                .Include(a => a.Authors)
                .Include(b => b.Genres)
                .Take(10)
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);

            var entities = _mapper.Map<List<BookDto>>(list).ToList();

            return entities.Count > 0 ? ServiceResult.Success(entities) : ServiceResult.Failed<List<BookDto>>(ServiceError.NotFound);
        }
        else
        {
            List<Book> list = await _context.Books
                .Include(a => a.Authors)
                .Include(b => b.Genres)
                .Skip((request.Page - 1) * 10)
                .Take(10)
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);

            var entities = _mapper.Map<List<BookDto>>(list).ToList();

            return entities.Count > 0 ? ServiceResult.Success(entities) : ServiceResult.Failed<List<BookDto>>(ServiceError.NotFound);
        }

    }
}
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

namespace BibliothecaManager.Application.BookItems.Queries;
public record GetAllBookItemsQuery : IRequestWrapper<List<BookItemDto>>;

public class GetAllBookItemsQueryHandler : IRequestHandlerWrapper<GetAllBookItemsQuery, List<BookItemDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllBookItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<BookItemDto>>> Handle(GetAllBookItemsQuery request, CancellationToken cancellationToken)
    {
        var response = await _context.BookItems
            .Include(b => b.Book)
            .Include(s => s.BookItemStatus)
            .ToListAsync(cancellationToken);

        return response.Count > 0 ? ServiceResult.Success(_mapper.Map<List<BookItemDto>>(response)) : ServiceResult.Failed<List<BookItemDto>>(ServiceError.NotFound);
    }
}

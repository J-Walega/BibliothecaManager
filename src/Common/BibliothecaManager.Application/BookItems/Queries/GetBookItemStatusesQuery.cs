using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace BibliothecaManager.Application.BookItems.Queries;
public record GetBookItemStatusesQuery : IRequestWrapper<List<BookItemStatusDto>>
{
    public int Id { get; set; }
}

public class GetBookItemStatusesCommand : IRequestHandlerWrapper<GetBookItemStatusesQuery, List<BookItemStatusDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBookItemStatusesCommand(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ServiceResult<List<BookItemStatusDto>>> Handle(GetBookItemStatusesQuery request, CancellationToken cancellationToken)
    {
        var response = await _context.BookItemStatuses.Include(x => x.BookItem).Where(x => x.BookItem.BookId == request.Id).ToListAsync(cancellationToken: cancellationToken);

        var statuses = _mapper.Map<List<BookItemStatusDto>>(response);

        return statuses.Count > 0 ? ServiceResult.Success(statuses) : ServiceResult.Failed<List<BookItemStatusDto>>(ServiceError.NotFound);
    }
}
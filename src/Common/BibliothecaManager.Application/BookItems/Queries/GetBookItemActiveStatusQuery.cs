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
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BibliothecaManager.Application.BookItems.Queries;
public record GetBookItemActiveStatusQuery : IRequestWrapper<List<BookItemStatusDto>>
{
    public int BookItemId { get; set; }
}

public class GetBookItemActiveStatusQueryHandler : IRequestHandlerWrapper<GetBookItemActiveStatusQuery, List<BookItemStatusDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBookItemActiveStatusQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<BookItemStatusDto>>> Handle(GetBookItemActiveStatusQuery request, CancellationToken cancellationToken)
    {
        var response = await _context.BookItemStatuses.Include(x => x.BookItem).Where(x => x.BookItem.BookId == request.BookItemId).Where(a => a.Active == true).ToListAsync();

        return response != null ? ServiceResult.Success(_mapper.Map<List<BookItemStatusDto>>(response)) : ServiceResult.Failed<List<BookItemStatusDto>>(ServiceError.NotFound);
    }
}
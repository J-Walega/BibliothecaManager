using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace BibliothecaManager.Application.Borrows.Queries.GetBorrows;
public class GetAllBorrowsQuery : IRequestWrapper<List<BorrowDto>>
{

}

public class GetAllBorrowsQueryHandler : IRequestHandlerWrapper<GetAllBorrowsQuery, List<BorrowDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public GetAllBorrowsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ServiceResult<List<BorrowDto>>> Handle(GetAllBorrowsQuery request, CancellationToken cancellationToken)
    {
        List<BorrowDto> list = await _context.Borrows
        .ProjectToType<BorrowDto>(_mapper.Config)
        .ToListAsync(cancellationToken);

        return list.Count > 0 ? ServiceResult.Success(list) : ServiceResult.Failed<List<BorrowDto>>(ServiceError.NotFound);
    }
}

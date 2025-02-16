using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using BibliothecaManager.Domain.Entities.LibraryEntities;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BibliothecaManager.Application.Borrows.Queries.GetUserBorrows;
public record GetBorrowsByUserIdQuery(string UserId) : IRequestWrapper<List<BorrowDto>>;

public class GetBorrowsByUserIdHandler : IRequestHandlerWrapper<GetBorrowsByUserIdQuery, List<BorrowDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBorrowsByUserIdHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ServiceResult<List<BorrowDto>>> Handle(GetBorrowsByUserIdQuery request, CancellationToken cancellationToken)
    {
        List<Borrow> listResponse = await _context.Borrows.Where(b => b.UserId.ToString() == request.UserId).ToListAsync(cancellationToken);
        var list = _mapper.Map<List<BorrowDto>>(listResponse);

        return list.Count > 0 ? ServiceResult.Success(list) : ServiceResult.Failed<List<BorrowDto>>(ServiceError.NotFound);
    }
}

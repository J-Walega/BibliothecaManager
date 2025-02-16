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

namespace BibliothecaManager.Application.Borrows.Queries.GetUserBorrows;
public record GetMyBorrowsQuery : IRequestWrapper<List<BorrowDto>>;

public class GetMyBorrowsQueryHandler : IRequestHandlerWrapper<GetMyBorrowsQuery, List<BorrowDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetMyBorrowsQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<ServiceResult<List<BorrowDto>>> Handle(GetMyBorrowsQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var borrows = await _context.Borrows
            .Include(a => a.BookItem).ThenInclude(b => b.Book)
            .Where(x => x.UserId.ToString() == userId.ToString())
            .ToListAsync(cancellationToken);
        
        var response = _mapper.Map<List<BorrowDto>>(borrows);

        return response.Count != 0 ? ServiceResult.Success(response) : ServiceResult.Failed<List<BorrowDto>>(ServiceError.NotFound); 

    }
}

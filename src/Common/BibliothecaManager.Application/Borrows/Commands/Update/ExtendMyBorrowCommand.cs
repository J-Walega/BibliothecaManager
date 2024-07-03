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

namespace BibliothecaManager.Application.Borrows.Commands.Update;
public record ExtendMyBorrowCommand(int BorrowId) : IRequestWrapper<BorrowDto>;

public class ExtendMyBorrowCommandHandler : IRequestHandlerWrapper<ExtendMyBorrowCommand, BorrowDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    public ExtendMyBorrowCommandHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;

    }
    public async Task<ServiceResult<BorrowDto>> Handle(ExtendMyBorrowCommand request, CancellationToken cancellationToken)
    {
        var borrow = await _context.Borrows
            .Where(x => x.Id == request.BorrowId)
            .Where(y => y.IsReturned == false)
            .FirstOrDefaultAsync(x => x.Id == request.BorrowId, cancellationToken);

        if (borrow == null)
        {
            return ServiceResult.Failed<BorrowDto>(ServiceError.NotFound);
        }

        if (borrow.ReturnDate < DateTime.UtcNow)
        {
            return ServiceResult.Failed<BorrowDto>(ServiceError.CustomMessageAndCode("Cannot extend borrow which return date has expired", 400));
        }

        if (borrow.UserId.ToString() == _currentUserService.UserId)
        {
            var extensionTime = DateTime.UtcNow.AddMonths(1);
            borrow.ReturnDate = extensionTime;
            _context.Borrows.Update(borrow);
            await _context.SaveChangesAsync(cancellationToken);
            return ServiceResult.Success(_mapper.Map<BorrowDto>(borrow));
        }
        else return ServiceResult.Failed<BorrowDto>(ServiceError.NotFound);
    }
}

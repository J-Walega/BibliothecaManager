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
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BibliothecaManager.Application.Borrows.Commands.Update;
public record ExtendBorrowCommand(int BorrowId) : IRequestWrapper<BorrowDto>;

public class ExtendBorrowCommandHandler : IRequestHandlerWrapper<ExtendBorrowCommand, BorrowDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ExtendBorrowCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ServiceResult<BorrowDto>> Handle(ExtendBorrowCommand request, CancellationToken cancellationToken)
    {
        var borrow = await _context.Borrows
            .Where(x => x.Id == request.BorrowId)
            .Where(y => y.IsReturned == false)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (borrow.ReturnDate > DateTime.UtcNow)
        {
            borrow.ReturnDate.AddMonths(1);
            await _context.SaveChangesAsync(cancellationToken);
            var response = _mapper.Map<BorrowDto>(borrow);
            return ServiceResult.Success(response);
        }
        return ServiceResult.Failed<BorrowDto>(ServiceError.CustomMessageAndCode("Cannot extend borrow which return date has expired", 400));
       
    }
}

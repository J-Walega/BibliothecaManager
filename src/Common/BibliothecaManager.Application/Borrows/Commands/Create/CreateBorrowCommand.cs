using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using BibliothecaManager.Domain.Entities.LibraryEntities;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace BibliothecaManager.Application.Borrows.Commands.Create;

public record CreateBorrowCommand(int BookItemId, string UserId) : IRequestWrapper<BorrowDto>;
public class CreateBorrowCommandHandler : IRequestHandlerWrapper<CreateBorrowCommand, BorrowDto>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;
    public CreateBorrowCommandHandler(IMapper mapper, IApplicationDbContext context, IIdentityService identityService)
    {
        _mapper = mapper;
        _context = context;
        _identityService = identityService;
    }
    public async Task<ServiceResult<BorrowDto>> Handle(CreateBorrowCommand request, CancellationToken cancellationToken)
    {
        var bookItem = await _context.BookItems
            .Where(i => i.Id == request.BookItemId)
            .Include(x => x.BookItemStatus)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        if (bookItem == null)
        {
            return ServiceResult.Failed<BorrowDto>(ServiceError.NotFound);
        }

        var bookItemStatus = bookItem.BookItemStatus.Where(x => x.Active == true).FirstOrDefault();
        
        if(bookItemStatus.Status != Domain.Enums.BookStatuses.Avilable)
        {
            return ServiceResult.Failed<BorrowDto>(ServiceError.CustomMessage($"Book item with {request.BookItemId} is currently not avaiable for borrowing"));
        }


        var user = _identityService.GetAllUserInfoAsync(request.UserId);
        if (user == null)
        {
            return ServiceResult.Failed<BorrowDto>(ServiceError.NotFound);
        }

        var entity = new Borrow { BookItem = bookItem, UserId = request.UserId};
        await _context.Borrows.AddAsync(entity, cancellationToken);

        await _context.BookItemStatuses.AddAsync(new BookItemStatus { BookItemId = request.BookItemId, Status = Domain.Enums.BookStatuses.Borrowed }, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(_mapper.Map<BorrowDto>(entity));
    }
}

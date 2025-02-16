using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Exceptions;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BibliothecaManager.Application.BookItems.Commands.Delete;
public record DeleteBookItemCommand(int BookId) : IRequestWrapper<BookItemDto>;

public class DeleteBookItemCommandHandler : IRequestHandlerWrapper<DeleteBookItemCommand, BookItemDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DeleteBookItemCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<BookItemDto>> Handle(DeleteBookItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.BookItems
            .FirstOrDefaultAsync(i => i.BookId == request.BookId, cancellationToken: cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(request), request.BookId);
        }
        _context.BookItems.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return ServiceResult.Success(_mapper.Map<BookItemDto>(entity));
    }
}

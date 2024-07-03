using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Exceptions;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using BibliothecaManager.Domain.Entities.LibraryEntities;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BibliothecaManager.Application.Publishers.Commands.Delete;
public record DeletePublisherCommand(int PublisherId) : IRequestWrapper<PublisherNameDto>;

public class DeletePublisherCommandHandler : IRequestHandlerWrapper<DeletePublisherCommand, PublisherNameDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DeletePublisherCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ServiceResult<PublisherNameDto>> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Publishers
            .Where(x => x.Id == request.PublisherId)
            .SingleOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(nameof(Publisher), request.PublisherId);
        _context.Publishers.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(_mapper.Map<PublisherNameDto>(entity));
    }
}

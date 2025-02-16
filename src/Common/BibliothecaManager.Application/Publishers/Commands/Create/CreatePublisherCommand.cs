using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using BibliothecaManager.Domain.Entities.LibraryEntities;
using MapsterMapper;
using MediatR;

namespace BibliothecaManager.Application.Publishers.Commands.Create;
public record CreatePublisherCommand(string Name) : IRequestWrapper<PublisherNameDto>;

public class CreatePublisherCommandHandler : IRequestHandlerWrapper<CreatePublisherCommand, PublisherNameDto>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public CreatePublisherCommandHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ServiceResult<PublisherNameDto>> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
    {
        var entity = new Publisher
        {
            Name = request.Name,
        };

        await _context.Publishers.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(_mapper.Map<PublisherNameDto>(entity));
    }
}

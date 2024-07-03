using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Exceptions;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using BibliothecaManager.Domain.Entities.LibraryEntities;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace BibliothecaManager.Application.Authors.Commands.Delete;
public class DeleteAuthorCommand : IRequestWrapper<AuthorDto>
{
    public int AuthorId { get; set; }
}

public class DeleteAuthorCommandHandler : IRequestHandlerWrapper<DeleteAuthorCommand, AuthorDto>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public DeleteAuthorCommandHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ServiceResult<AuthorDto>> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Authors
            .Where(x => x.Id == request.AuthorId)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            return ServiceResult.Failed<AuthorDto>(ServiceError.NotFound);
        }

        _context.Authors.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(_mapper.Map<AuthorDto>(entity));
    }
}

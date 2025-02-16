using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using BibliothecaManager.Domain.Entities.LibraryEntities;
using MapsterMapper;

namespace BibliothecaManager.Application.Authors.Commands.Create;
public record CreateAuthorCommand(string Name, string Surname) : IRequestWrapper<AuthorDto>;

public class CreateAuthorCommandHandler : IRequestHandlerWrapper<CreateAuthorCommand, AuthorDto>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public CreateAuthorCommandHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ServiceResult<AuthorDto>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var entity = new Author
        {
            Name = request.Name,
            Surname = request.Surname
        };

        await _context.Authors.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(_mapper.Map<AuthorDto>(entity));
    }
}

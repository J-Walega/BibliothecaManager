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

namespace BibliothecaManager.Application.Genres.Commands;
public record CreateGenreCommand(string GenreName) : IRequestWrapper<GenreDto>;

public class CreateGenreCommandHandler : IRequestHandlerWrapper<CreateGenreCommand, GenreDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateGenreCommandHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<ServiceResult<GenreDto>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        var entity = new Genre 
        {
            GenereName = request.GenreName 
        };

        await _context.Genres.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(_mapper.Map<GenreDto>(entity));
    }
}

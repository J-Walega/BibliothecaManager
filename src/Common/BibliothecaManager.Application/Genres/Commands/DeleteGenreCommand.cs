using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace BibliothecaManager.Application.Genres.Commands;
public record DeleteGenreCommand(int GenreId) : IRequestWrapper<GenreDto>;

public class DeleteGenreCommandHandler : IRequestHandlerWrapper<DeleteGenreCommand, GenreDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DeleteGenreCommandHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ServiceResult<GenreDto>> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await _context.Genres
            .FirstOrDefaultAsync(x => x.Id == request.GenreId);

        if (genre != null)
        {
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<GenreDto>(genre));
        }

        return ServiceResult.Failed<GenreDto>(ServiceError.NotFound);
    }
}
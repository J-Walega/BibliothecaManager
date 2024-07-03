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

namespace BibliothecaManager.Application.Genres.Queries;
public record GetGenresQuery : IRequestWrapper<List<GenreDto>>;

public class GetGenresQueryHandler : IRequestHandlerWrapper<GetGenresQuery, List<GenreDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetGenresQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<GenreDto>>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
    {
        var response = await _context.Genres.ToListAsync(cancellationToken);

        return response.Count > 0 ? ServiceResult.Success(_mapper.Map<List<GenreDto>>(response)) : ServiceResult.Failed<List<GenreDto>>(ServiceError.NotFound);
    }
}

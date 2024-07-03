using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Mapping;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace BibliothecaManager.Application.Authors.Queries;
public class GetAllAuthorsQuery : IRequestWrapper<List<AuthorDto>>
{

}

public class GetAuthorsQueryHandler : IRequestHandlerWrapper<GetAllAuthorsQuery, List<AuthorDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAuthorsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<AuthorDto>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
    {
        List<AuthorDto> list = await _context.Authors
            .ProjectToType<AuthorDto>(_mapper.Config)
            .ToListAsync(cancellationToken);

        return list.Count > 0 ? ServiceResult.Success(list) : ServiceResult.Failed<List<AuthorDto>>(ServiceError.NotFound);
    }
}

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace BibliothecaManager.Application.Authors.Queries;
public class GetAuthorByIdQuery : IRequestWrapper<AuthorDto>
{
    public int AuthorId { get; set; }
}

public class GetAuthorByIdQueryHandler : IRequestHandlerWrapper<GetAuthorByIdQuery, AuthorDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAuthorByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ServiceResult<AuthorDto>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Authors
            .Where(x => x.Id == request.AuthorId)
            .FirstOrDefaultAsync();

        return entity != null ? ServiceResult.Success(_mapper.Map<AuthorDto>(entity)) : ServiceResult.Failed<AuthorDto>(ServiceError.NotFound);
    }
}
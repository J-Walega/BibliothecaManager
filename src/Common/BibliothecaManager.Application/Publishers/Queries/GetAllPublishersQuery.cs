using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace BibliothecaManager.Application.Publishers.Queries;
public record GetAllPublishersQuery : IRequestWrapper<List<PublisherNameDto>>;

public class GetAllPublishersQueryHandler : IRequestHandlerWrapper<GetAllPublishersQuery, List<PublisherNameDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAllPublishersQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ServiceResult<List<PublisherNameDto>>> Handle(GetAllPublishersQuery request, CancellationToken cancellationToken)
    {
        List<PublisherNameDto> list = await _context.Publishers
            .ProjectToType<PublisherNameDto>(_mapper.Config)
            .ToListAsync(cancellationToken);

        return list.Count > 0 ? ServiceResult.Success(list) : ServiceResult.Failed<List<PublisherNameDto>>(ServiceError.NotFound);
    }
}

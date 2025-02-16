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

namespace BibliothecaManager.Application.Comments.Queries.GetUserComments;
public class GetUserCommentsQuery : IRequestWrapper<List<CommentDto>>
{
    public Guid UserId { get; set; }
}

public class GetUserCommentsQueryHandler : IRequestHandlerWrapper<GetUserCommentsQuery, List<CommentDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUserCommentsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<CommentDto>>> Handle(GetUserCommentsQuery request, CancellationToken cancellationToken)
    {
        var commentsList = await _context.Comments
            .Where(x => x.UserId.Equals(request.UserId))
            .ProjectToType<CommentDto>(_mapper.Config)
            .ToListAsync(cancellationToken);

        return commentsList.Count > 0 ? ServiceResult.Success(commentsList) : ServiceResult.Failed<List<CommentDto>>(ServiceError.NotFound);
    }
}

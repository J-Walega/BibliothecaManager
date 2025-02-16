using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace BibliothecaManager.Application.Comments.Queries.GetBookComments;
public class GetBookCommentsQuery : IRequestWrapper<List<CommentDto>>
{
    public int BookId { get; set; }
}

public class GetBookCommentsHandler : IRequestHandlerWrapper<GetBookCommentsQuery, List<CommentDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBookCommentsHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ServiceResult<List<CommentDto>>> Handle(GetBookCommentsQuery request, CancellationToken cancellationToken)
    {
        List<CommentDto> commentsList = await _context.Comments
            .Where(x => x.Book.Id.Equals(request.BookId))
            .ProjectToType<CommentDto>(_mapper.Config)
            .ToListAsync(cancellationToken);

        return commentsList.Count > 0 ? ServiceResult.Success(commentsList) : ServiceResult.Failed<List<CommentDto>>(ServiceError.NotFound);
    }
}
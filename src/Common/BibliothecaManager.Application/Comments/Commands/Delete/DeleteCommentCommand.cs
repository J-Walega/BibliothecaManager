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

namespace BibliothecaManager.Application.Comments.Commands.Delete;
public record DeleteCommentCommand(int CommentId) : IRequestWrapper<CommentDto>;

public class DeleteCommentCommandHandler : IRequestHandlerWrapper<DeleteCommentCommand, CommentDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public DeleteCommentCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ServiceResult<CommentDto>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments
            .Where(x => x.Id == request.CommentId)
            .FirstOrDefaultAsync(cancellationToken);

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync(cancellationToken);

        return comment != null ? ServiceResult.Success(_mapper.Map<CommentDto>(comment)) : ServiceResult.Failed<CommentDto>(ServiceError.NotFound);
    }
}
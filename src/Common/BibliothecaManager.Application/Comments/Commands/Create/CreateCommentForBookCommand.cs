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

namespace BibliothecaManager.Application.Comments.Commands.Create;
public record CreateCommentForBookCommand(float Score, string Content) : IRequestWrapper<CommentDto>
{
    public int BookId { get; set; }
}

public class CreateCommentForBookCommandHandler : IRequestHandlerWrapper<CreateCommentForBookCommand, CommentDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public CreateCommentForBookCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser,  IMapper mapper, IIdentityService identityService)
    {
        _context = context;
        _currentUser = currentUser;
        _mapper = mapper;
        _identityService = identityService;
    }
    public async Task<ServiceResult<CommentDto>> Handle(CreateCommentForBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == request.BookId, cancellationToken: cancellationToken);
        var user = _currentUser.UserId;
        var username = await _identityService.GetAllUserInfoAsync(user);

        await _context.Comments.AddAsync(new Domain.Entities.LibraryEntities.Comment { UserId = user, FullName = $"{username.Name} {username.Surname}", Book = book, Content = request.Content, Score = request.Score }, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return ServiceResult.Success(_mapper.Map<CommentDto>(request));
    }
}
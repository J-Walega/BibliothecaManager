using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Exceptions;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using BibliothecaManager.Domain.Entities.LibraryEntities;
using BibliothecaManager.Domain.Enums;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace BibliothecaManager.Application.Books.Commands.Create;

public record CreateBookCommand(string Title, int PublisherId, List<int> AuthorsIds, List<int> GenresIds, int InitialBookCount) : IRequestWrapper<BookDto>;

public class CreateBookCommandHandler : IRequestHandlerWrapper<CreateBookCommand, BookDto>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public CreateBookCommandHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ServiceResult<BookDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var publisher = await _context.Publishers.FindAsync(new object[] { request.PublisherId }, cancellationToken);
        var entity = new Book
        {
            Title = request.Title,
            Authors = new List<Author>(),
            Genres = new List<Genre>(),
            BookItems = new List<BookItem>(),
            Comments = new List<Comment>(),
        };

        if (publisher != null)
        {
            entity.Publisher = publisher;
            foreach (var authors in request.AuthorsIds)
            {
                var authorsToAdd = await _context.Authors.FindAsync(new object[] { authors }, cancellationToken: cancellationToken);
                if(authorsToAdd == null)
                {
                    return ServiceResult.Failed<BookDto>(ServiceError.CustomMessageAndCode("One of provided authors was not found in the database", 404));
                }
                entity.Authors.Add(authorsToAdd);
            }
            foreach (var genres in request.GenresIds)
            {
                var genresToAdd = await _context.Genres.FindAsync(new object[] { genres }, cancellationToken: cancellationToken);
                entity.Genres.Add(genresToAdd);
            }

            for (int i = 1; i <= request.InitialBookCount; i++)
            {
                entity.BookItems.Add(new BookItem { Book = entity, Library = await _context.Libraries.FirstOrDefaultAsync(cancellationToken) });
            }
            await _context.Books.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<BookDto>(entity));
        }

        return ServiceResult.Failed<BookDto>(ServiceError.NotFound);
        
    }
}

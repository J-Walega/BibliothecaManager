using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Exceptions;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using BibliothecaManager.Domain.Entities.LibraryEntities;
using MapsterMapper;

namespace BibliothecaManager.Application.Books.Commands.Update;
public class UpdateBookCommand : IRequestWrapper<BookDto>
{
    public int BookId { get; set; }
    public string Title { get; set; }

    public class UpdateBookCommadHandler : IRequestHandlerWrapper<UpdateBookCommand, BookDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBookCommadHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<BookDto>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Books.FindAsync(request.BookId, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Book), request.BookId);
            }
            if (!string.IsNullOrEmpty(request.Title))
            {
                entity.Title = request.Title;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<BookDto>(entity));
        }
    }
}

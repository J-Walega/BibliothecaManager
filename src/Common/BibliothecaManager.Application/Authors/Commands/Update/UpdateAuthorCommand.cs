using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Exceptions;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using BibliothecaManager.Domain.Entities.LibraryEntities;
using MapsterMapper;

namespace BibliothecaManager.Application.Authors.Commands.Update;
public class UpdateAuthorCommand : IRequestWrapper<AuthorDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public class UpdateAuthorCommandHandler : IRequestHandlerWrapper<UpdateAuthorCommand, AuthorDto>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public UpdateAuthorCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResult<AuthorDto>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Authors.FindAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Author), request.Id);
            }
            if (!string.IsNullOrEmpty(request.Name))
            {
                entity.Name = request.Name;
            }
            if (!string.IsNullOrEmpty(request.Surname))
            {
                entity.Surname = request.Surname;
            }
         
            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<AuthorDto>(entity));
        }
    }
}

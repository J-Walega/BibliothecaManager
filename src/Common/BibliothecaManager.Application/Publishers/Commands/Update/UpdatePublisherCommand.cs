using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Exceptions;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using BibliothecaManager.Domain.Entities.LibraryEntities;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace BibliothecaManager.Application.Publishers.Commands.Update;
public record UpdatePublisherCommand : IRequestWrapper<PublisherNameDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public BookIdsDto Books { get; set; }

    public class UpdatePublisherCommandHandler : IRequestHandlerWrapper<UpdatePublisherCommand, PublisherNameDto>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public UpdatePublisherCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResult<PublisherNameDto>> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
        {
            var entity = new List<Publisher>() { await _context.Publishers.FindAsync(new object[] { request.Id }, cancellationToken) };

                foreach (var item in request.Books.Ids)
                {
                    _ = await _context.Books.FindAsync(new object[] { item }, cancellationToken);
                }
                

            await _context.SaveChangesAsync(cancellationToken);
            return ServiceResult.Success(_mapper.Map<PublisherNameDto>(entity));
        }
    }
}

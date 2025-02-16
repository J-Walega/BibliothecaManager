using System.Collections.Generic;
using BibliothecaManager.Domain.Entities.LibraryEntities;
using Mapster;

namespace BibliothecaManager.Application.Dto.LibraryDto;
public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public ICollection<GenreDto> Generes { get; set; }
    public ICollection<AuthorDto> Authors { get; set; }
    public PublisherNameDto Publisher { get; set; }
    public ICollection<CommentDto> Comments { get; set; }
}

public class GenreDto
{
    public int Id { get; set; }
    public string genreName { get; set; }
}

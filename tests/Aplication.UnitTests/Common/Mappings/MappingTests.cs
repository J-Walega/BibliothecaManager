using System;
using BibliothecaManager.Application.Dto;
using BibliothecaManager.Application.Dto.LibraryDto;
using BibliothecaManager.Domain.Entities;
using BibliothecaManager.Domain.Entities.LibraryEntities;
using FluentAssertions;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BibliothecaManager.Application.UnitTests.Common.Mappings;

public class MappingTests
{
    private readonly IMapper _mapper;

    public MappingTests()
    {
        TypeAdapterConfig typeAdapterConfig = new();

        IServiceCollection services = new ServiceCollection();
        services.AddSingleton(typeAdapterConfig);
        services.AddScoped<IMapper, ServiceMapper>();

        var sp = services.BuildServiceProvider();

        using var scope = sp.CreateScope();
        _mapper = scope.ServiceProvider.GetService<IMapper>();
    }


    [Test]
    [TestCase(typeof(Author), typeof(AuthorDto))]
    [TestCase(typeof(Book), typeof(BookDto))]
    [TestCase(typeof(Comment), typeof(CommentDto))]
    [TestCase(typeof(Publisher), typeof(PublisherNameDto))]
    [TestCase(typeof(BookItem), typeof(BookItemDto))]
    [TestCase(typeof (Borrow), typeof(BorrowDto))]
    [TestCase(typeof (BookItem), typeof(BookItemDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = Activator.CreateInstance(source);

        _mapper.Map(instance, source, destination);
    }

    [Test]
    public void ShouldMappingCorrectly()
    {
        var author = new Author { Id = 1, Name = "Adam" };
        var authorDto = _mapper.Map<Author, AuthorDto>(author);
        authorDto.Name.Should().Be("Adam");
    }
}

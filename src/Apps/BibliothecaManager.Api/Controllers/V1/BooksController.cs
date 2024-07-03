using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Books.Commands.Create;
using BibliothecaManager.Application.Books.Commands.Delete;
using BibliothecaManager.Application.Books.Commands.Update;
using BibliothecaManager.Application.Books.Queries.GetBook;
using BibliothecaManager.Application.Books.Queries.GetBooks;
using BibliothecaManager.Application.Books.Queries.GetBooksByAuthorId;
using BibliothecaManager.Application.Books.Queries.GetBooksByQuery;
using BibliothecaManager.Application.Common.Models;
using BibliothecaManager.Application.Dto.LibraryDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliothecaManager.Api.Controllers.V1;


/// <summary>
/// Books
/// </summary>
public class BooksController : BaseApiController
{
    /// <summary>
    /// Get all books
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<ServiceResult<List<BookDto>>>> GetAllBooks(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetAllBooksQuery(), cancellationToken));
    }

    /// <summary>
    /// Gets book with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResult<BookDto>>> GetBookById(int id, CancellationToken cancellationToken)
    {
        var query = new GetBookByIdQuery { BookId = id };
        return Ok(await Mediator.Send(query, cancellationToken));
    }

    /// <summary>
    /// Create a book
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Administrator, Employee")]
    public async Task<ActionResult<ServiceResult<BookDto>>> CreateBook(CreateBookCommand command, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }

    /// <summary>
    /// Delete a book
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator, Employee")]
    public async Task<ActionResult<ServiceResult<BookDto>>> DeleteBook(int id, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new DeleteBookCommand { BookId = id }, cancellationToken));
    }

    /// <summary>
    /// Update a book
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize(Roles = "Administrator, Employee")]
    public async Task<ActionResult<ServiceResult<BookDto>>> UpdateBook(UpdateBookCommand command, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }

    /// <summary>
    /// Get first 10 books in the record based on provided parameter, skips 10 * page books in record
    /// </summary>
    /// <param name="page"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("list/{page}")]
    public async Task<ActionResult<ServiceResult<List<BookDto>>>> GetNextBooks (int page, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetBooksWithPaginationQuery { Page = page }, cancellationToken));
    }

    /// <summary>
    /// Search books with title containing query
    /// </summary>
    /// <param name="q"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("search")]
    public async Task<ActionResult<ServiceResult<List<BookDto>>>> GetBooksQuery (string q, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetBooksByQuery(q), cancellationToken));
    }

    /// <summary>
    /// Gets books with author id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("author/{id}")]
    public async Task<ActionResult<ServiceResult<List<BookDto>>>> GetBooksByAuthorQuery (int id, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetBooksByAuthorIdQuery { AuthorId = id}, cancellationToken));
    }
}

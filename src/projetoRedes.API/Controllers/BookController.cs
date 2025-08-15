using Microsoft.AspNetCore.Mvc;
using projetoRedes.API.Attributes;
using projetoRedes.Application.UseCases.Book.AddBook;
using projetoRedes.Application.UseCases.Book.SearchByTitle;
using projetoRedes.Communication.Requests;
using projetoRedes.Communication.Responses;

namespace projetoRedes.API.Controllers;

public class BookController : BaseController
{
    [HttpPost]
    [AuthenticatedUser]
    [Route("add")]
    [ProducesResponseType(typeof(ResponseAdded), StatusCodes.Status201Created)]
    public async Task<IActionResult> AddBook([FromServices] IAddBookUseCase useCase,
        [FromBody] RequestAddBook req)
    {
        var result = await useCase.Execute(req);

        return Created(string.Empty, result);
    }

    [HttpGet]
    [AuthenticatedUser]
    [Route("search")]
    [ProducesResponseType(typeof(ResponseSearchBook), StatusCodes.Status200OK)]
    public async Task<IActionResult> SearchBook([FromServices] ISearchBookByTitleUseCase useCase,
        [FromQuery] RequestSearch req)
    {
        var result = await useCase.Execute(req);

        return Ok(result);
    }
}

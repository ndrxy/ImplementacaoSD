using Microsoft.AspNetCore.Mvc;
using projetoRedes.API.Attributes;
using projetoRedes.Application.UseCases.Author.AddAuthor;
using projetoRedes.Application.UseCases.Author.SearchByName;
using projetoRedes.Communication.Requests;
using projetoRedes.Communication.Responses;

namespace projetoRedes.API.Controllers;

public class AuthorController : BaseController
{
    [HttpPost]
    [AuthenticatedUser]
    [Route("add")]
    [ProducesResponseType(typeof(ResponseAdded), StatusCodes.Status201Created)]
    public async Task<IActionResult> AddAuthor([FromServices] IAddAuthorUseCase useCase,
        [FromBody] RequestAddAuthor req)
    {
        var result = await useCase.Execute(req);

        return Created(string.Empty, result);
    }

    [HttpGet]
    [AuthenticatedUser]
    [Route("search")]
    [ProducesResponseType(typeof(ResponseSearchAuthor), StatusCodes.Status200OK)]
    public async Task<IActionResult> SearchBook([FromServices] ISearchAuthorByNameUseCase useCase,
        [FromQuery] RequestSearch req)
    {
        var result = await useCase.Execute(req);

        return Ok(result);
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projetoRedes.Application.UseCases.User.Login;
using projetoRedes.Application.UseCases.User.Register;
using projetoRedes.Communication.Requests;
using projetoRedes.Communication.Responses;

namespace projetoRedes.API.Controllers
{
    public class UserController : BaseController
    {
        [HttpPost]
        [AllowAnonymous]
        [Route("/register")]
        [ProducesResponseType(typeof(ResponseRegisteredUser), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterUserUseCase useCase,
            [FromBody] RequestRegisterUser req)
        {
            var result = await useCase.Execute(req);

            return Created(string.Empty, result);
        }

        
        [HttpPost]
        [AllowAnonymous]
        [Route("/login")]
        [ProducesResponseType(typeof(ResponseRegisteredUser), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromServices] ILoginUseCase useCase,
            [FromBody] RequestLogin req)
        {
            var response = await useCase.Execute(req);

            return Ok(response);
        }
    }
}

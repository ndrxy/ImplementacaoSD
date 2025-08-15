using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using projetoRedes.Communication.Responses;
using projetoRedes.Domain.Repositories;
using projetoRedes.Domain.Security;
using projetoRedes.Exceptions.ExceptionsBase;

namespace projetoRedes.API.Filters;

public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
{
    private readonly IAccessTokenValidator _accessTokenValidator;
    private readonly IUserRepository _repository;

    public AuthenticatedUserFilter(IAccessTokenValidator accessTokenValidator,
        IUserRepository repository)
    {
        _accessTokenValidator = accessTokenValidator;
        _repository = repository;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenOnRequest(context);

            var userIdentifier = _accessTokenValidator.ValidateAndGetUserIdentifier(token);

            var exist = await _repository.ExistsActiveUserWithIdentifier(userIdentifier);

            if (!exist)
            {
                throw new UnauthorizedException("Sem autorização para acessar o recurso.");
            }
        }
        catch (SecurityTokenExpiredException)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseError("Token Expirado")
            {
                TokenIsExpired = true
            });
        }
        catch (SolutionsExceptions ex)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseError(ex.Message));
        }
        catch
        {
            context.Result = new UnauthorizedObjectResult(new ResponseError("Usuário não tem permissão para acessar o recurso"));
        }
    }

    private static string TokenOnRequest(AuthorizationFilterContext context)
    {
        var authentication = context.HttpContext.Request.Headers.Authorization.ToString();
        if (string.IsNullOrEmpty(authentication))
        {
            throw new UnauthorizedException("Sem permissão.");

        }

        return authentication["Bearer ".Length..].Trim();
    }
}

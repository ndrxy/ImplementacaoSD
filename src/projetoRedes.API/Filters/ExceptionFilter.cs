using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using projetoRedes.Communication.Responses;
using projetoRedes.Exceptions.ExceptionsBase;
using System.Net;

namespace projetoRedes.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is SolutionsExceptions solutionExceptions)
            HandleProjectException(solutionExceptions, context);
        else
            ThrowUnknownException(context);
    }

    private void HandleProjectException(SolutionsExceptions solutionExceptions, ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)solutionExceptions.GetStatusCode();
        context.Result = new ObjectResult(new ResponseError(solutionExceptions.GetErrorMessages()));
    }

    private void ThrowUnknownException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ResponseError("Ocorreu um erro desconhecido"));

    }
}

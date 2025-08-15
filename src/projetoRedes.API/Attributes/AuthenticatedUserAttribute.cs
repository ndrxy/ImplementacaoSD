using Microsoft.AspNetCore.Mvc;
using projetoRedes.API.Filters;

namespace projetoRedes.API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class AuthenticatedUserAttribute : TypeFilterAttribute
{
    public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
    {
        
    }
}

using System.Globalization;

namespace projetoRedes.Communication.Requests;

public class RequestLogin
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

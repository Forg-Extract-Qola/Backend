using System.Globalization;

namespace Qola.API.Security.Exceptions;

public class AppExceptions :Exception
{
    public AppExceptions()
    {
    }
    public AppExceptions(string? message) : base(message)
    {
    }
    public AppExceptions(string? message, params object[] args) :
        base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}
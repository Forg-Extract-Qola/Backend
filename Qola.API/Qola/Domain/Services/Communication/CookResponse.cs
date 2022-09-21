using Qola.API.Qola.Domain.Models;
using Qola.API.Shared.Domain.Repositories.Services.Communication;

namespace Qola.API.Qola.Domain.Services.Communication;

public class CookResponse:BaseResponse<Cook>
{
    public CookResponse(Cook resource) : base(resource)
    {
    }

    public CookResponse(string message) : base(message)
    {
    }
}
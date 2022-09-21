using Qola.API.Qola.Domain.Models;
using Qola.API.Shared.Domain.Repositories.Services.Communication;

namespace Qola.API.Qola.Domain.Services.Communication;

public class WaiterResponse: BaseResponse<Waiter>
{
    public WaiterResponse(Waiter resource) : base(resource)
    {
    }

    public WaiterResponse(string message) : base(message)
    {
    }
}
using Qola.API.Qola.Domain.Models;
using Qola.API.Shared.Domain.Repositories.Services.Communication;

namespace Qola.API.Qola.Domain.Services.Communication;

public class OrderResponse:BaseResponse<Order>
{
    public OrderResponse(Order resource) : base(resource)
    {
    }

    public OrderResponse(string message) : base(message)
    {
    }
}
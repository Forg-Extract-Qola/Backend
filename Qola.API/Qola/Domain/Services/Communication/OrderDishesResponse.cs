using Qola.API.Qola.Domain.Models;
using Qola.API.Shared.Domain.Repositories.Services.Communication;

namespace Qola.API.Qola.Domain.Services.Communication;

public class OrderDishesResponse : BaseResponse<OrderDishes>
{
    public OrderDishesResponse(OrderDishes resource) : base(resource)
    {
    }

    public OrderDishesResponse(string message) : base(message)
    {
    }
}
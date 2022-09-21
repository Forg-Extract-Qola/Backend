using Qola.API.Qola.Domain.Models;
using Qola.API.Shared.Domain.Repositories.Services.Communication;

namespace Qola.API.Qola.Domain.Services.Communication;

public class RestaurantResponse: BaseResponse<Restaurant>
{
    public RestaurantResponse(Restaurant resource) : base(resource)
    {
    }

    public RestaurantResponse(string message) : base(message)
    {
    }
}
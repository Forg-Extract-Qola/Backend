using Qola.API.Qola.Domain.Models;
using Qola.API.Shared.Domain.Repositories.Services.Communication;

namespace Qola.API.Qola.Domain.Services.Communication;

public class DishResponse:BaseResponse<Dish>
{
    public DishResponse(Dish resource) : base(resource)
    {
    }

    public DishResponse(string message) : base(message)
    {
    }
}
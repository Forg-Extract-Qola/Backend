using AutoMapper;
using Qola.API.Qola.Domain.Models;
using Qola.API.Qola.Resources;

namespace Qola.API.Qola.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Restaurant, RestaurantResource>();
        CreateMap<Cook, CookResource>();
        CreateMap<Waiter,WaiterResource>();
        CreateMap<Dish, DishResource>();
        CreateMap<Table,TableResource>();
        CreateMap<Order, OrderResource>();
        CreateMap<OrderDishes, OrderDishesResource>();
    }
}
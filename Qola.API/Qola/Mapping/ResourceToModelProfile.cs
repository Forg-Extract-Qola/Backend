using AutoMapper;
using Qola.API.Qola.Domain.Models;
using Qola.API.Qola.Resources;

namespace Qola.API.Qola.Mapping;

public class ResourceToModelProfile: Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveRestaurantResource, Restaurant>();
        CreateMap<SaveCookResource, Cook>();
        CreateMap<SaveWaiterResource, Waiter>();
        CreateMap<SaveDishResource, Dish>();
        CreateMap<SaveTableResource, Table>();
        CreateMap<SaveOrderResource, Order>();
        CreateMap<SaveOrderDishesResource, OrderDishes>();
    }
}
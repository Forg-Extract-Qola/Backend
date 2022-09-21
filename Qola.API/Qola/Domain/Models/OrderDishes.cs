namespace Qola.API.Qola.Domain.Models;

public class OrderDishes
{
    public int Id { get; set; }
    public Order Order { get; set; }
    public int OrderId { get; set; }
    public Dish Dish { get; set; }
    public int DishId { get; set; }
}
namespace Qola.API.Qola.Domain.Models;

public class Dish
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string Category_dish { get; set; }
    public float Price { get; set; }
    public Restaurant Restaurant { get; set; }
    public int RestaurantId { get; set; }
    public IList<OrderDishes> OrderDishes { get; set; } = new List<OrderDishes>();
}
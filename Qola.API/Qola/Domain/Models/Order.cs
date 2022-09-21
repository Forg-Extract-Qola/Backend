namespace Qola.API.Qola.Domain.Models;

public class Order
{
    public int Id { get; set; }
    public string Status { get; set; }
    public string Notes { get; set; }
    public float Total { get; set; }
    public Table Table { get; set; }
    public int TableId { get; set; }
    public Waiter Waiter { get; set; }
    public int WaiterId { get; set; }
    public Restaurant Restaurant { get; set; }
    public int RestaurantId { get; set; }
    public IList<OrderDishes> OrderDishes { get; set; } = new List<OrderDishes>();
}
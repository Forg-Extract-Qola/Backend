namespace Qola.API.Qola.Domain.Models;

public class Table
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsOccupied { get; set; }
    public Restaurant Restaurant { get; set; }
    public int RestaurantId { get; set; }
    public IList<Order> Orders { get; set; } = new List<Order>();
}
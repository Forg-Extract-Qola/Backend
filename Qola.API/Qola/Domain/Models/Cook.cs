namespace Qola.API.Qola.Domain.Models;

public class Cook
{
    public int Id { get; set; }
    public string UID { get; set; }
    public string FullName { get; set; }
    public string Charge { get; set; }
    public Restaurant Restaurant { get; set; }
    public int RestaurantId { get; set; }
}
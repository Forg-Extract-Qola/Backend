namespace Qola.API.Qola.Resources;

public class OrderResource
{
    public int Id { get; set; }
    public string Status { get; set; }
    public string Notes { get; set; }
    public float Total { get; set; }
    public int TableId { get; set; }
    public int WaiterId { get; set; }
    public int RestaurantId { get; set; }
}
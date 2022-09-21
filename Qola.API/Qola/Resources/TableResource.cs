namespace Qola.API.Qola.Resources;

public class TableResource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsOccupied { get; set; }
    public int RestaurantId { get; set; }
}
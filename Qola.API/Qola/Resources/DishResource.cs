namespace Qola.API.Qola.Resources;

public class DishResource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string Category_dish { get; set; }
    public float Price { get; set; }
    public int RestaurantId { get; set; }
}
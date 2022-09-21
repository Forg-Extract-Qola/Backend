namespace Qola.API.Qola.Resources;

public class RestaurantResource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string RUC { get; set; }
    public string Phone { get; set; }
    public int ManagerId { get; set; }
    public string Logo { get; set; }
}
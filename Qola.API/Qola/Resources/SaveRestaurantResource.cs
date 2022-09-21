using System.ComponentModel.DataAnnotations;

namespace Qola.API.Qola.Resources;

public class SaveRestaurantResource
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Address { get; set; }
    
    [Required]
    public string RUC { get; set; }
    
    [Required]
    public string Phone { get; set; }
    
    [Required]
    public string Logo { get; set; }
}
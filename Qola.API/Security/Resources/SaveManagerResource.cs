using System.ComponentModel.DataAnnotations;

namespace Qola.API.Security.Resources;

public class SaveManagerResource
{
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(100)]
    public string Email { get; set; }
}
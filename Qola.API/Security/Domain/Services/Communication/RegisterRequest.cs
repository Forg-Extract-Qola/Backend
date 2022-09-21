using System.ComponentModel.DataAnnotations;

namespace Qola.API.Security.Domain.Services.Communication;

public class RegisterRequest
{
    [Required]
    public string FullName { get; set; }

    [Required]
    public string Password { get; set; }
    
    [Required]
    public string Email { get; set; }
}
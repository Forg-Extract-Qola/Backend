namespace Qola.API.Security.Domain.Services.Communication;

public class UpdateRequest
{
    public string FullName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}
namespace Qola.API.Security.Resources;

public class ManagerResource
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
}
using System.Text.Json.Serialization;
using Qola.API.Qola.Domain.Models;

namespace Qola.API.Security.Domain.Models;

public class Manager
{
    public int Id { get; set; }
    public string FullName { get; set; }
    [JsonIgnore] 
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public Restaurant Restaurant { get; set; }
    public int RestaurantId { get; set; }
}
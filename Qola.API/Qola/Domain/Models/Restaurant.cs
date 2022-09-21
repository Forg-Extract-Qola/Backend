using System.Text.Json.Serialization;
using Qola.API.Security.Domain.Models;

namespace Qola.API.Qola.Domain.Models;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string RUC { get; set; }
    public string Phone { get; set; }
    public string Logo { get; set; }
    [JsonIgnore] public Manager Manager { get; set; }
    public int ManagerId { get; set; }
    // Conexiones deL Restaurante
    public IList<Cook> Cooks { get; set; } = new List<Cook>();
    public IList<Waiter> Waiters { get; set; } = new List<Waiter>();
    public IList<Dish> Dishes { get; set; } = new List<Dish>();
    public IList<Table> Tables { get; set; } = new List<Table>();
    public IList<Order> Orders { get; set; } = new List<Order>();
}
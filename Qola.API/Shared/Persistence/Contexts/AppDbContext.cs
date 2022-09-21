using Microsoft.EntityFrameworkCore;
using Qola.API.Qola.Domain.Models;
using Qola.API.Security.Domain.Models;
using Qola.API.Shared.Extensions;

namespace Qola.API.Shared.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    // Declare DbSet of the entity
    public DbSet<Manager> Managers { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Cook> Cooks { get; set; }
    public DbSet<Waiter> Waiters { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDishes> OrderDishes { get; set; }

    // Structure of the database
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Manager entity
        builder.Entity<Manager>().ToTable("Managers");
        builder.Entity<Manager>().HasKey(p => p.Id);
        builder.Entity<Manager>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Manager>().Property(p => p.FullName).IsRequired().HasMaxLength(100);
        builder.Entity<Manager>().Property(p => p.Email).IsRequired().HasMaxLength(100);

        // relationship

        builder.Entity<Manager>()
            .HasOne(p => p.Restaurant)
            .WithOne()
            .HasForeignKey<Manager>(p => p.RestaurantId);


        // Restaurant entity
        builder.Entity<Restaurant>().ToTable("Restaurants");
        builder.Entity<Restaurant>().HasKey(p => p.Id);
        builder.Entity<Restaurant>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Restaurant>().Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Restaurant>().Property(p => p.Address).IsRequired().HasMaxLength(100);
        builder.Entity<Restaurant>().Property(p => p.Phone).IsRequired().HasMaxLength(100);
        builder.Entity<Restaurant>().Property(p => p.Logo).IsRequired().HasMaxLength(1000);
        builder.Entity<Restaurant>().Property(p => p.RUC).IsRequired().HasMaxLength(100);
        builder.Entity<Restaurant>().Property(p => p.ManagerId).IsRequired();

        // relationship

        builder.Entity<Restaurant>()
            .HasOne(p => p.Manager)
            .WithOne(p => p.Restaurant)
            .HasForeignKey<Restaurant>(p => p.ManagerId);
        
        builder.Entity<Restaurant>()
            .HasMany(p => p.Cooks)
            .WithOne(p => p.Restaurant)
            .HasForeignKey(p => p.RestaurantId);
        
        builder.Entity<Restaurant>()
            .HasMany(p => p.Waiters)
            .WithOne(p => p.Restaurant)
            .HasForeignKey(p => p.RestaurantId);
        
        builder.Entity<Restaurant>()
            .HasMany(p => p.Dishes)
            .WithOne(p => p.Restaurant)
            .HasForeignKey(p => p.RestaurantId);
        
        builder.Entity<Restaurant>()
            .HasMany(p => p.Tables)
            .WithOne(p => p.Restaurant)
            .HasForeignKey(p => p.RestaurantId);
        
        builder.Entity<Restaurant>()
            .HasMany(p => p.Orders)
            .WithOne(p => p.Restaurant)
            .HasForeignKey(p => p.RestaurantId);

        // Cook entity
        builder.Entity<Cook>().ToTable("Cooks");
        builder.Entity<Cook>().HasKey(p => p.Id);
        builder.Entity<Cook>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Cook>().Property(p => p.UID).IsRequired().HasMaxLength(100);
        builder.Entity<Cook>().Property(p => p.FullName).IsRequired().HasMaxLength(100);
        builder.Entity<Cook>().Property(p => p.Charge).IsRequired().HasMaxLength(100);
        builder.Entity<Cook>().Property(p => p.RestaurantId).IsRequired();

        // relationship
        
        builder.Entity<Cook>()
            .HasOne(p => p.Restaurant)
            .WithMany(p => p.Cooks)
            .HasForeignKey(p => p.RestaurantId);
        
        // Waiter entity
        builder.Entity<Waiter>().ToTable("Waiters");
        builder.Entity<Waiter>().HasKey(p => p.Id);
        builder.Entity<Waiter>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Waiter>().Property(p => p.UID).IsRequired().HasMaxLength(100);
        builder.Entity<Waiter>().Property(p => p.FullName).IsRequired().HasMaxLength(100);
        builder.Entity<Waiter>().Property(p => p.Charge).IsRequired().HasMaxLength(100);
        builder.Entity<Waiter>().Property(p => p.RestaurantId).IsRequired();
        
        // relationship
        
        builder.Entity<Waiter>()
            .HasOne(p => p.Restaurant)
            .WithMany(p => p.Waiters)
            .HasForeignKey(p => p.RestaurantId);
        
        builder.Entity<Waiter>()
            .HasMany(p => p.Orders)
            .WithOne(p => p.Waiter)
            .HasForeignKey(p => p.WaiterId);

        // Dish entity
        builder.Entity<Dish>().ToTable("Dishes");
        builder.Entity<Dish>().HasKey(p => p.Id);
        builder.Entity<Dish>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Dish>().Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Dish>().Property(p => p.Description).IsRequired().HasMaxLength(1000);
        builder.Entity<Dish>().Property(p => p.Price).IsRequired();
        builder.Entity<Dish>().Property(p => p.Category_dish).IsRequired().HasMaxLength(100);
        builder.Entity<Dish>().Property(p => p.RestaurantId).IsRequired();
        
        // relationship
        
        builder.Entity<Dish>()
            .HasOne(p => p.Restaurant)
            .WithMany(p => p.Dishes)
            .HasForeignKey(p => p.RestaurantId);
        
        builder.Entity<Dish>()
            .HasMany(p => p.OrderDishes)
            .WithOne(p => p.Dish)
            .HasForeignKey(p => p.DishId);
        
        // Table entity
        builder.Entity<Table>().ToTable("Tables");
        builder.Entity<Table>().HasKey(p => p.Id);
        builder.Entity<Table>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Table>().Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Table>().Property(p => p.IsOccupied).IsRequired();
        builder.Entity<Table>().Property(p => p.RestaurantId).IsRequired();
        
        // relationship
        
        builder.Entity<Table>()
            .HasOne(p => p.Restaurant)
            .WithMany(p => p.Tables)
            .HasForeignKey(p => p.RestaurantId);
        
        builder.Entity<Table>()
            .HasMany(p => p.Orders)
            .WithOne(p => p.Table)
            .HasForeignKey(p => p.TableId);
        
        // Order entity
        builder.Entity<Order>().ToTable("Orders");
        builder.Entity<Order>().HasKey(p => p.Id);
        builder.Entity<Order>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Order>().Property(p => p.Status).IsRequired().HasMaxLength(100);
        builder.Entity<Order>().Property(p => p.Notes).IsRequired().HasMaxLength(1000);
        builder.Entity<Order>().Property(p => p.Total).IsRequired();
        builder.Entity<Order>().Property(p => p.TableId).IsRequired();
        builder.Entity<Order>().Property(p => p.WaiterId).IsRequired();
        
        // relationship
        
        builder.Entity<Order>()
            .HasOne(p => p.Table)
            .WithMany(p => p.Orders)
            .HasForeignKey(p => p.TableId);
        
        builder.Entity<Order>()
            .HasOne(p => p.Waiter)
            .WithMany(p => p.Orders)
            .HasForeignKey(p => p.WaiterId);
        
        builder.Entity<Order>()
            .HasOne(p => p.Restaurant)
            .WithMany(p => p.Orders)
            .HasForeignKey(p => p.RestaurantId);
        
        builder.Entity<Order>()
            .HasMany(p => p.OrderDishes)
            .WithOne(p => p.Order)
            .HasForeignKey(p => p.OrderId);
        
        
        // OrderDishes entity
        builder.Entity<OrderDishes>().ToTable("OrderDishes");
        builder.Entity<OrderDishes>().HasKey(p => p.Id);
        builder.Entity<OrderDishes>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<OrderDishes>().Property(p => p.OrderId).IsRequired();
        builder.Entity<OrderDishes>().Property(p => p.DishId).IsRequired();
        
        // relationship
        
        builder.Entity<OrderDishes>()
            .HasOne(p => p.Order)
            .WithMany(p => p.OrderDishes)
            .HasForeignKey(p => p.OrderId);
        
        builder.Entity<OrderDishes>()
            .HasOne(p => p.Dish)
            .WithMany(p => p.OrderDishes)
            .HasForeignKey(p => p.DishId);

        builder.UseSnakeCaseNamingConvention();
        
    }
}
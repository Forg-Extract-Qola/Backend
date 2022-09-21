using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Qola.API.Qola.Domain.Repositories;
using Qola.API.Qola.Domain.Services;
using Qola.API.Qola.Persistence.Repositories;
using Qola.API.Qola.Services;
using Qola.API.Security.Authorization.Handlers.Implementations;
using Qola.API.Security.Authorization.Handlers.Interfaces;
using Qola.API.Security.Authorization.Middleware;
using Qola.API.Security.Authorization.Settings;
using Qola.API.Security.Persistence.Repositories;
using Qola.API.Security.Services;
using Qola.API.Security.Domain.Repositories;
using Qola.API.Security.Domain.Services;
using Qola.API.Shared.Domain.Repositories.Repositories;
using Qola.API.Shared.Persistence.Contexts;
using Qola.API.Shared.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySQL(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());

// AppSettings Configuration

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// OpenAPI Configuration
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
    Version = "v1",
    Title = "Qola API",
    Description = "Qola Web Service",
    Contact = new OpenApiContact
    {
        Name = "Qola",
        Url = new Uri("https://qola.pe")
    },
    License = new OpenApiLicense
    {
        Name = "Qola Center Resource License",
        Url = new Uri("https://qola.pe/license")
    }
    });
    options.EnableAnnotations();
    options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearerAuth"
                }
            },
            Array.Empty<string>()
        }
    });
});


//Add lower case routing

builder.Services.AddRouting(options => options.LowercaseUrls = true);

//Dependency Injection Configuration

//Shared injection configuration

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddCors();

//TopWay injection configuration
builder.Services.AddScoped<IManagerRepository, ManagerRepository>();
builder.Services.AddScoped<IManagerService, ManagerService>();
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<ICookService, CookService>();
builder.Services.AddScoped<ICookRepository, CookRepository>();
builder.Services.AddScoped<IWaiterRepository, WaiterRepository>();
builder.Services.AddScoped<IWaiterService, WaiterService>();
builder.Services.AddScoped<IDishRepository, DishRepository>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDishesRepository, OrderDishesRepository>();
builder.Services.AddScoped<IOrderDishesService, OrderDishesService>();

//Security injection configuration
builder.Services.AddScoped<IJwtHandler, JwtHandler>();


//Auto Mapper Configuration
builder.Services.AddAutoMapper(
    typeof(Qola.API.Qola.Mapping.ModelToResourceProfile), 
    typeof(Qola.API.Qola.Mapping.ResourceToModelProfile),
    typeof(Qola.API.Security.Mapping.ModelToResourceProfile), 
    typeof(Qola.API.Security.Mapping.ResourceToModelProfile));

var app = builder.Build();

// Creating database of entities
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context!.Database.EnsureCreated();
}


// Run the application for Swagger
//if (app.Environment.IsDevelopment()) 
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

// Configure CORS
app.UseCors(x => x
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

// Configure Error Handler Middleware

app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure JWT Handling

app.UseMiddleware<JwtMiddleware>();

// Run the application

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
namespace Qola.API.Security.Domain.Services.Communication;

public class AuthenticateResponse
{
    public string Token { get; set; }
    public int RestaurantId { get; set; }
}
using Qola.API.Security.Domain.Models;
using Qola.API.Shared.Domain.Repositories.Services.Communication;

namespace Qola.API.Security.Domain.Services.Communication;

public class ManagerResponse: BaseResponse<Manager>
{
    public ManagerResponse(Manager resource) : base(resource)
    {
    }

    public ManagerResponse(string message) : base(message)
    {
    }
}
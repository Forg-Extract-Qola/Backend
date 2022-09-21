using Qola.API.Qola.Domain.Models;
using Qola.API.Shared.Domain.Repositories.Services.Communication;

namespace Qola.API.Qola.Domain.Services.Communication;

public class TableResponse: BaseResponse<Table>
{
    public TableResponse(Table resource) : base(resource)
    {
    }

    public TableResponse(string message) : base(message)
    {
    }
}
namespace Qola.API.Shared.Domain.Repositories.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}
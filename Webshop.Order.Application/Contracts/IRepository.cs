using Webshop.Order.Domain.Common;

namespace Webshop.Order.Application.Contracts;

public interface IRepository<T> where T : AggregateRoot
{
    Task<Guid> CreateAsync(T entity);
    Task DeleteAsync(Guid id);
    Task<T> GetById(Guid id);
    Task<IEnumerable<T>> GetAll();
    Task UpdateAsync(T entity);
}

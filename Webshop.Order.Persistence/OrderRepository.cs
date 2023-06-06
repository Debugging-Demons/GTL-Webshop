using Webshop.Order.Application.Contracts;
using Webshop.Order.Domain.AggregateRoots;

namespace Webshop.Order.Persistence;

public class OrderRepository : BaseRepository, IOrderRepository
{
    private Container<PurchaseOrder> _container;

    public OrderRepository(Container<PurchaseOrder> container, IDataContext dataContext) : base("", dataContext)
    {
        _container = container;
    }

    public Task<Guid> CreateAsync(PurchaseOrder entity)
    {
        entity.Id = Guid.NewGuid();
        _container.Items.Add(entity);
        return Task.FromResult(entity.Id);
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PurchaseOrder>> GetAll()
    {
        return Task.FromResult(_container.Items.AsEnumerable());
    }

    public Task<PurchaseOrder> GetById(Guid id)
    {
        PurchaseOrder? order = _container.Items.Find(x => x.Id == id);

        if (order is null) return Task.FromCanceled<PurchaseOrder>(CancellationToken.None);

        return Task.FromResult(order);
    }

    public Task UpdateAsync(PurchaseOrder entity)
    {
        throw new NotImplementedException();
    }
}

using Webshop.Order.Domain.AggregateRoots;

namespace Webshop.Order.Application.Contracts;

public interface IOrderRepository : IRepository<PurchaseOrder>
{

}

using Webshop.Order.Domain.AggregateRoots;

namespace Webshop.Order.Persistence;

public class Container
{
    internal List<PurchaseOrder> PurchaseOrders = new List<PurchaseOrder>();
}
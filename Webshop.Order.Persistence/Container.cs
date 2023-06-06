using Webshop.Order.Domain.AggregateRoots;

namespace Webshop.Order.Persistence;

public class Container<T>
{
    public List<T> Items = new List<T>();
}
using EnsureThat;
using Webshop.Order.Domain.Common;

namespace Webshop.Order.Domain.ValueObjects;

public sealed class Discount : ValueObject
{
    public int Value { get; init; }

    public Discount(int value)
    {
        EnsureArg.IsInRange(value, paramName: nameof(value), min: 0, max: 15);

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
using EnsureThat;
using Webshop.Order.Domain.Common;

namespace Webshop.Order.Domain.ValueObjects;

public sealed class Discount : ValueObject
{
    public int Value { get; init; }

    public DiscountType DiscountType { get; init; }

    public Discount(int value, DiscountType type)
    {
        switch (type)
        {
            case DiscountType.Percent:
                EnsureArg.IsInRange(value, paramName: nameof(value), min: 0, max: 15);
                break;
            case DiscountType.Absolute:
                EnsureArg.IsGte(value, paramName: nameof(value), limit: 0);
                break;
        }

        Value = value;
        DiscountType = type;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return DiscountType;
        yield return Value;
    }
}

public enum DiscountType
{
    Percent = 0,
    Absolute = 1,
}
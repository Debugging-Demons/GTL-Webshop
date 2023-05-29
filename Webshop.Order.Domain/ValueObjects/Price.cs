using EnsureThat;
using Webshop.Order.Domain.Common;

namespace Webshop.Order.Domain.ValueObjects;

public sealed class Price : ValueObject
{
    public Currency Currency { get; set; }
    public int Value { get; init; }

    public Price(int value, Currency currency)
    {
        Ensure.That(value, nameof(value)).IsGte(0); // Has to be 0 or more

        Value = value;
        Currency = currency;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Currency;
        yield return Value;
    }
}

public enum Currency
{
    DKK = 0,
    EUR = 1,
    USD = 2,
}

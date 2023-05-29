﻿using EnsureThat;
using Webshop.Order.Domain.Common;

namespace Webshop.Order.Domain.ValueObjects;

public class Address : ValueObject
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string ZipCode { get; private set; }

    public Address(string street, string city, string state, string country, string zipcode)
    {
        Ensure.String.IsNotNullOrWhiteSpace(street);
        Ensure.String.IsNotNullOrWhiteSpace(city);
        Ensure.String.IsNotNullOrWhiteSpace(state);
        Ensure.String.IsNotNullOrWhiteSpace(country);
        Ensure.String.IsNotNullOrWhiteSpace(zipcode);

        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipcode;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        // Using a yield return statement to return each element one at a time
        yield return Street;
        yield return City;
        yield return Country;
        yield return ZipCode;
        yield return State;
    }
}

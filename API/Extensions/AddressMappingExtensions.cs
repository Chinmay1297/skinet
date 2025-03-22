using System;
using API.DTOs;
using Core.Entities;

namespace API.Extensions;

public static class AddressMappingExtensions
{
    public static AddressDto? ToDto(this Address? address)
    {
        if (address == null) return null;
        return new AddressDto
        {
            Line1 = address.Line1,
            Line2 = address.Line2,
            City = address.City,
            State = address.State,
            PostalCode = address.PostalCode,
            Country = address.Country
        };
    }

    public static Address ToEntity(this AddressDto addressDto)
    {
        if (addressDto == null) throw new ArgumentNullException(nameof(addressDto));
        return new Address
        {
            Line1 = addressDto.Line1,
            Line2 = addressDto.Line2,
            City = addressDto.City,
            State = addressDto.State,
            PostalCode = addressDto.PostalCode,
            Country = addressDto.Country
        };
    }

    public static void UpdateFromDto(this Address address, AddressDto addressDto)
    {
        if (address == null) throw new ArgumentNullException(nameof(address));
        if (addressDto == null) throw new ArgumentNullException(nameof(addressDto));


        address.Line1 = address.Line1;
        address.Line2 = address.Line2;
        address.City = address.City;
        address.State = address.State;
        address.PostalCode = address.PostalCode;
        address.Country = address.Country;

    }
}

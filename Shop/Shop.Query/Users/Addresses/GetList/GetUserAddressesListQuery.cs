using Common.Query;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.Addresses.GetList;

public record GetUserAddressQuery(long UserId) : IQuery<AddressDto>;
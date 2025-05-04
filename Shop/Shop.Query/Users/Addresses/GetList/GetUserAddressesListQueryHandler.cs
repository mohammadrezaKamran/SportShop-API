using Common.Query;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.Addresses.GetList;

internal class GetUserAddressQueryHandler : IQueryHandler<GetUserAddressQuery, AddressDto>
{
    private readonly ShopContext _context;

    public GetUserAddressQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<AddressDto> Handle(GetUserAddressQuery request, CancellationToken cancellationToken)
    {
        //var sql = $"Select * from {_dapperContext.UserAddresses} where UserId=@userId";
        //using var context = _dapperContext.CreateConnection();
        //return await context.QueryFirstOrDefaultAsync<AddressDto>(sql, new { userId = request.UserId });

        var user = await _context.Users
          .FirstOrDefaultAsync(f => f.Id == request.UserId, cancellationToken);
        if (user == null)
            return null;


        var userAddress = user.Address;
        if (userAddress == null)
            return null;
        return new AddressDto()
        {
            UserId = request.UserId,
             Id= userAddress.Id,
            Name = userAddress.Name,
            Family = userAddress.Family,
            PhoneNumber=userAddress.PhoneNumber.Value,
            City = userAddress.City,
            Shire=userAddress.Shire,
            PostalAddress=userAddress.PostalAddress,
            PostalCode=userAddress.PostalCode,
            NationalCode=userAddress.NationalCode,
        };
    }
}
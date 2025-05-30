using Common.Domain;
using Common.Domain.Exceptions;

namespace Shop.Domain.UserAgg;

public class UserToken : BaseEntity
{
    private UserToken()
    {
        
    }
    public UserToken(string hashJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device,string ipAddress)
    {
        Guard(hashJwtToken, hashRefreshToken, tokenExpireDate, refreshTokenExpireDate);
        HashJwtToken = hashJwtToken;
        HashRefreshToken = hashRefreshToken;
        TokenExpireDate = tokenExpireDate;
        RefreshTokenExpireDate = refreshTokenExpireDate;
        Device = device;
        IpAddress = ipAddress;
    }
    public long UserId { get; internal set; }
    public string HashJwtToken { get; private set; }
    public string HashRefreshToken { get; private set; }
    public DateTime TokenExpireDate { get; private set; }
    public DateTime RefreshTokenExpireDate { get; private set; }
    public string Device { get; private set; }
    public string IpAddress { get; set; }


    public void Guard(string hashJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate)
    {
        NullOrEmptyDomainDataException.CheckString(hashJwtToken, nameof(HashJwtToken));
        NullOrEmptyDomainDataException.CheckString(hashRefreshToken, nameof(HashRefreshToken));

        if (tokenExpireDate < DateTime.UtcNow)
            throw new InvalidDomainDataException("Invalid Token ExpireDate");

        if (refreshTokenExpireDate < tokenExpireDate)
            throw new InvalidDomainDataException("Invalid RefreshToken ExpireDate");
    }
}
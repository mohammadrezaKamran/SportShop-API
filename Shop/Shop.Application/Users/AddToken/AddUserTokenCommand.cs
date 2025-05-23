﻿using Common.Application;

namespace Shop.Application.Users.AddToken;

public class AddUserTokenCommand : IBaseCommand
{
    public AddUserTokenCommand(long userId, string hashJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device,string ipAddress)
    {
        UserId = userId;
        HashJwtToken = hashJwtToken;
        HashRefreshToken = hashRefreshToken;
        TokenExpireDate = tokenExpireDate;
        RefreshTokenExpireDate = refreshTokenExpireDate;
        Device = device;
        IpAddress = ipAddress;
    }
    public long UserId { get; }
    public string HashJwtToken { get; }
    public string HashRefreshToken { get; }
    public DateTime TokenExpireDate { get; }
    public DateTime RefreshTokenExpireDate { get; }
    public string Device { get; }
    public string IpAddress { get; }

}
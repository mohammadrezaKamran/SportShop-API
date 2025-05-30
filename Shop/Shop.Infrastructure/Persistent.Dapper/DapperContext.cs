using System.Data;
using Microsoft.Data.SqlClient;

namespace Shop.Infrastructure.Persistent.Dapper;

public class DapperContext
{
    private readonly string _connectionString;

    public DapperContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);


 
    public string UserAddresses => "[user].Address";
    public string Orders => "[order].Orders";
    public string OrderItems => "[order].Items";
    public string Products => "[product].Products";
    public string ProductVariants => "[product].ProductVariants";
    public string UserTokens => "[user].Tokens";
}
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Webshop.Order.Persistence;

public interface IDataContext
{
    public IDbConnection CreateConnection();
}

public class DataContext : IDataContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    public DataContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
    }

    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);
}
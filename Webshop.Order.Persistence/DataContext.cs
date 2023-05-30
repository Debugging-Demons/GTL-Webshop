using System.Data;

namespace Webshop.Order.Persistence;

public class DataContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    public DataContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection");
    }
    public IDbConnection CreateConnection()
        => new System.Data.SqlClient.SqlConnection(_connectionString);
}
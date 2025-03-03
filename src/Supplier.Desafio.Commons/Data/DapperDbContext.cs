using MySql.Data.MySqlClient;
using System.Data;

namespace Supplier.Desafio.Commons.Data;

public class DapperDbContext
{
    private readonly string _connectionString;

    public DapperDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}

using System.Data;
using Dapper;
using Npgsql;

namespace Dal.Dapper;

public class BaseDapperDao
{
    private readonly string _connectionString;
    
    public BaseDapperDao()
    {
        _connectionString = @"Server=postgres;Port=5432;Database=factory;User Id=postgres;Password=2323;";
    }

    public NpgsqlConnection GetConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        return connection;
    }

    protected async IAsyncEnumerable<T> GetEnumerable<T>(string query, object parameters)
    {

        var connection = GetConnection();
        using var reader = await connection.ExecuteReaderAsync(query, parameters);
        var rowParser = reader.GetRowParser<T>();

        while (await reader.ReadAsync()) {
            yield return rowParser(reader);
        }
    }
}
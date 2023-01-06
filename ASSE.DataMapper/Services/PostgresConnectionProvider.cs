using System.Configuration;
using System.Data;
using Npgsql;

namespace ASSE.DataMapper.Services;
public class PostgresConnectionProvider : IDbConnectionProvider
{
	private static readonly string _postgresConnectionString = ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;

	public IDbConnection GetNewConnection()
	{
		return new NpgsqlConnection(_postgresConnectionString);
	}
}

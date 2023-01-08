using System.Configuration;
using System.Data;
using System.Data.SQLite;
using ASSE.DataMapper.Services;

namespace ASSE.Gui.Cli.Services;
public class SQLiteConnectionProvider : IDbConnectionProvider
{
	private static readonly string _sqliteConnectionString = ConfigurationManager.ConnectionStrings["SQLiteConnection"].ConnectionString;

	public IDbConnection GetNewConnection()
	{
		return new SQLiteConnection(_sqliteConnectionString);
	}
}

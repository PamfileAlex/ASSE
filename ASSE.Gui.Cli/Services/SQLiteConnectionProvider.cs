// --------------------------------------------------------------------------------------
// <copyright file="SQLiteConnectionProvider.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using System.Configuration;
using System.Data;
using System.Data.SQLite;
using ASSE.DataMapper.Services;

namespace ASSE.Gui.Cli.Services;

/// <summary>
/// <see cref="IDbConnectionProvider"/> implementation specific for SQLite connections.
/// </summary>
public class SQLiteConnectionProvider : IDbConnectionProvider
{
	private static readonly string _sqliteConnectionString = ConfigurationManager.ConnectionStrings["SQLiteConnection"].ConnectionString;

	/// <inheritdoc/>
	public IDbConnection GetNewConnection()
	{
		return new SQLiteConnection(_sqliteConnectionString);
	}
}

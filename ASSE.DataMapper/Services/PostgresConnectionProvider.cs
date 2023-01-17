// --------------------------------------------------------------------------------------
// <copyright file="PostgresConnectionProvider.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using System.Configuration;
using System.Data;
using Npgsql;

namespace ASSE.DataMapper.Services;

/// <summary>
/// <see cref="IDbConnectionProvider"/> implementation specific for PostgreSQL connections.
/// </summary>
public class PostgresConnectionProvider : IDbConnectionProvider
{
	private static readonly string _postgresConnectionString = ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;

	/// <inheritdoc/>
	public IDbConnection GetNewConnection()
	{
		return new NpgsqlConnection(_postgresConnectionString);
	}
}

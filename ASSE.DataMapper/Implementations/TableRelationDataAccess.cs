// --------------------------------------------------------------------------------------
// <copyright file="TableRelationDataAccess.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using System.Data;
using ASSE.Core.Models;
using ASSE.DataMapper.Interfaces;
using ASSE.DataMapper.Services;
using Dapper;
using Serilog;

namespace ASSE.DataMapper.Implementations;

/// <summary>
/// Abstract implementation of <see cref="ITableRelationDataAccess"/>.
/// </summary>
public abstract class TableRelationDataAccess : DataAccess, ITableRelationDataAccess
{
	/// <summary>
	/// Initializes a new instance of the <see cref="TableRelationDataAccess"/> class.
	/// </summary>
	/// <param name="dbConnectionProvider">Database connection provider instance.</param>
	/// <param name="logger">Serilog <see cref="ILogger"/> instance.</param>
	protected TableRelationDataAccess(IDbConnectionProvider dbConnectionProvider, ILogger logger)
		: base(dbConnectionProvider, logger)
	{
	}

	/// <summary>
	/// Returns a list of all relation entities by relation identity.
	/// </summary>
	/// <typeparam name="T">The type of relation that implements <see cref="IRelationEntity"/>.</typeparam>
	/// <param name="sql">Structured Query Language string.</param>
	/// <param name="dataAccess">Data access of relation.</param>
	/// <param name="id">Identity of relation.</param>
	/// <param name="connection">Connection to database.</param>
	/// <param name="transaction">Transaction on connection.</param>
	/// <returns>List of all relation entities by relation identity.</returns>
	protected List<T> GetAllByRelationId<T>(string sql, IDataAccess<T> dataAccess, int id, IDbConnection connection, IDbTransaction? transaction = null)
		where T : class, IKeyEntity, new()
	{
		Logger.Debug("Getting All by relationId: {id}", id);

		List<T> data = dataAccess.GetAll();

		HashSet<int> ids = connection.Query<int>(sql, new { id }, transaction)
			.ToHashSet();

		return data.Where(x => ids.Contains(x.Id)).AsList();
	}
}

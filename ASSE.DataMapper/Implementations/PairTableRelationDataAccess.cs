// --------------------------------------------------------------------------------------
// <copyright file="PairTableRelationDataAccess.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using System.Data;
using ASSE.Core.Models;
using ASSE.DataMapper.Interfaces;
using ASSE.DataMapper.Services;
using Serilog;

namespace ASSE.DataMapper.Implementations;

/// <summary>
/// Abstract implementation of <see cref="IPairTableRelationDataAccess{T}"/>.
/// </summary>
/// <typeparam name="T">The type of relation that implements <see cref="IRelationEntity"/>.</typeparam>
public abstract class PairTableRelationDataAccess<T> : TableRelationDataAccess, IPairTableRelationDataAccess<T>
	where T : IRelationEntity
{
	/// <summary>
	/// Initializes a new instance of the <see cref="PairTableRelationDataAccess{T}"/> class.
	/// </summary>
	/// <param name="dbConnectionProvider">Database connection provider instance.</param>
	/// <param name="logger">Serilog <see cref="ILogger"/> instance.</param>
	public PairTableRelationDataAccess(IDbConnectionProvider dbConnectionProvider, ILogger logger)
		: base(dbConnectionProvider, logger)
	{
	}

	/// <inheritdoc/>
	public virtual void Add(int first, int second)
	{
		Logger.Debug("Adding: ({first}, {second})", first, second);

		using IDbConnection connection = DbConnectionProvider.GetNewConnection();
		connection.Open();
		Add(first, second, connection);
	}

	/// <inheritdoc/>
	public abstract void Add(int first, int second, IDbConnection connection, IDbTransaction? transaction = null);

	/// <inheritdoc/>
	public virtual bool Delete(int first, int second)
	{
		Logger.Debug("Deleting: ({first}, {second})", first, second);

		using IDbConnection connection = DbConnectionProvider.GetNewConnection();
		connection.Open();
		return Delete(first, second, connection);
	}

	/// <inheritdoc/>
	public abstract bool Delete(int first, int second, IDbConnection connection, IDbTransaction? transaction = null);

	/// <inheritdoc/>
	public virtual List<T> GetAll()
	{
		Logger.Debug("Getting all");

		using IDbConnection connection = DbConnectionProvider.GetNewConnection();
		connection.Open();
		return GetAll(connection);
	}

	/// <inheritdoc/>
	public abstract List<T> GetAll(IDbConnection connection, IDbTransaction? transaction = null);
}

// --------------------------------------------------------------------------------------
// <copyright file="DataAccess{T}.cs" company="Transilvania University of Brasov">
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
using Dapper.Contrib.Extensions;
using Serilog;

namespace ASSE.DataMapper.Implementations;

/// <summary>
/// Abstract implementation of <see cref="IDataAccess{T}"/>.
/// </summary>
/// <typeparam name="T">Type of entity that implements <see cref="IKeyEntity"/>.</typeparam>
public abstract class DataAccess<T> : DataAccess, IDataAccess<T>
	where T : class, IKeyEntity, new()
{
	/// <summary>
	/// Initializes a new instance of the <see cref="DataAccess{T}"/> class.
	/// </summary>
	/// <param name="dbConnectionProvider">Database connection provider instance.</param>
	/// <param name="logger">Serilog <see cref="ILogger"/> instance.</param>
	protected DataAccess(IDbConnectionProvider dbConnectionProvider, ILogger logger)
		: base(dbConnectionProvider, logger)
	{
	}

	/// <inheritdoc/>
	public virtual int Add(T data)
	{
		using IDbConnection connection = DbConnectionProvider.GetNewConnection();
		connection.Open();
		return Add(data, connection);
	}

	/// <inheritdoc/>
	public virtual int Add(T data, IDbConnection connection, IDbTransaction? transaction = null)
	{
		Logger.Debug("Adding: {data}", data);
		data.Id = (int)connection.Insert(data, transaction);
		return data.Id;
	}

	/// <inheritdoc/>
	public virtual T? Get(int id)
	{
		Logger.Debug("Getting with id: {id}", id);
		using IDbConnection connection = DbConnectionProvider.GetNewConnection();
		connection.Open();
		return Get(id, connection);
	}

	/// <inheritdoc/>
	public virtual T? Get(int id, IDbConnection connection, IDbTransaction? transaction = null)
	{
		return connection.Get<T>(id, transaction);
	}

	/// <inheritdoc/>
	public virtual bool Update(T data)
	{
		using IDbConnection connection = DbConnectionProvider.GetNewConnection();
		connection.Open();
		return Update(data, connection);
	}

	/// <inheritdoc/>
	public virtual bool Update(T data, IDbConnection connection, IDbTransaction? transaction = null)
	{
		Logger.Debug("Updating: {data}", data);
		return connection.Update(data, transaction);
	}

	/// <inheritdoc/>
	public virtual bool Delete(int id)
	{
		using IDbConnection connection = DbConnectionProvider.GetNewConnection();
		connection.Open();
		return Delete(id, connection);
	}

	/// <inheritdoc/>
	public virtual bool Delete(int id, IDbConnection connection, IDbTransaction? transaction = null)
	{
		Logger.Debug("Deleting with id: {id}", id);
		return connection.Delete(new T() { Id = id }, transaction);
	}

	/// <inheritdoc/>
	public virtual List<T> GetAll()
	{
		using IDbConnection connection = DbConnectionProvider.GetNewConnection();
		connection.Open();
		return GetAll(connection);
	}

	/// <inheritdoc/>
	public virtual List<T> GetAll(IDbConnection connection, IDbTransaction? transaction = null)
	{
		Logger.Debug("Getting all entities");
		return connection.GetAll<T>(transaction).AsList();
	}
}

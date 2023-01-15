using System.Data;
using ASSE.Core.Models;
using ASSE.DataMapper.Interfaces;
using ASSE.DataMapper.Services;
using Dapper;
using Dapper.Contrib.Extensions;
using Serilog;

namespace ASSE.DataMapper.Implementations;
public abstract class DataAccess<T> : DataAccess, IDataAccess<T>
	where T : class, IKeyEntity, new()
{
	#region Constructors
	protected DataAccess(IDbConnectionProvider dbConnectionProvider, ILogger logger)
		: base(dbConnectionProvider, logger)
	{
	}
	#endregion

	#region Add Methods
	public virtual int Add(T data)
	{
		using IDbConnection connection = _dbConnectionProvider.GetNewConnection();
		connection.Open();
		return Add(data, connection);
	}

	public virtual int Add(T data, IDbConnection connection, IDbTransaction? transaction = null)
	{
		_logger.Debug("Adding: {data}", data);
		data.Id = (int)connection.Insert(data, transaction);
		return data.Id;
	}
	#endregion

	#region Get Methods
	public virtual T? Get(int id)
	{
		_logger.Debug("Getting with id: {id}", id);
		using IDbConnection connection = _dbConnectionProvider.GetNewConnection();
		connection.Open();
		return Get(id, connection);
	}

	public virtual T? Get(int id, IDbConnection connection, IDbTransaction? transaction = null)
	{
		return connection.Get<T>(id, transaction);
	}
	#endregion

	#region Update Methods
	public virtual bool Update(T data)
	{
		using IDbConnection connection = _dbConnectionProvider.GetNewConnection();
		connection.Open();
		return Update(data, connection);
	}

	public virtual bool Update(T data, IDbConnection connection, IDbTransaction? transaction = null)
	{
		_logger.Debug("Updating: {data}", data);
		return connection.Update(data, transaction);
	}
	#endregion

	#region Delete Methods
	public virtual bool Delete(int id)
	{
		using IDbConnection connection = _dbConnectionProvider.GetNewConnection();
		connection.Open();
		return Delete(id, connection);
	}

	public virtual bool Delete(int id, IDbConnection connection, IDbTransaction? transaction = null)
	{
		_logger.Debug("Deleting with id: {id}", id);
		return connection.Delete(new T() { Id = id }, transaction);
	}
	#endregion

	#region GetAll Methods
	public virtual List<T> GetAll()
	{
		using IDbConnection connection = _dbConnectionProvider.GetNewConnection();
		connection.Open();
		return GetAll(connection);
	}

	public virtual List<T> GetAll(IDbConnection connection, IDbTransaction? transaction = null)
	{
		_logger.Debug("Getting all entities");
		return connection.GetAll<T>(transaction).AsList();
	}
	#endregion
}

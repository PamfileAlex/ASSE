using System.Data;
using ASSE.Core.Models;
using ASSE.DataMapper.Interfaces;
using ASSE.DataMapper.Services;

namespace ASSE.DataMapper.Implementations;
public abstract class PairTableRelationDataAccess<T> : TableRelationDataAccess, IPairTableRelationDataAccess<T>
	where T : IRelationEntity
{
	public PairTableRelationDataAccess(IDbConnectionProvider dbConnectionProvider)
		: base(dbConnectionProvider)
	{
	}

	public virtual void Add(int first, int second)
	{
		using IDbConnection connection = _dbConnectionProvider.GetNewConnection();
		connection.Open();
		Add(first, second, connection);
	}

	public abstract void Add(int first, int second, IDbConnection connection, IDbTransaction? transaction = null);

	public virtual bool Delete(int first, int second)
	{
		using IDbConnection connection = _dbConnectionProvider.GetNewConnection();
		connection.Open();
		return Delete(first, second, connection);
	}

	public abstract bool Delete(int first, int second, IDbConnection connection, IDbTransaction? transaction = null);

	public virtual List<T> GetAll()
	{
		using IDbConnection connection = _dbConnectionProvider.GetNewConnection();
		connection.Open();
		return GetAll(connection);
	}

	public abstract List<T> GetAll(IDbConnection connection, IDbTransaction? transaction = null);
}

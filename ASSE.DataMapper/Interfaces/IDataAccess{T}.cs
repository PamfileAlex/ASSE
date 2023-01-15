using System.Data;
using ASSE.Core.Models;
using ASSE.Core.Services;

namespace ASSE.DataMapper.Interfaces;
public interface IDataAccess<T> : IDataAccess, ICrud<T>
	where T : class, IKeyEntity, new()
{
	int Add(T data, IDbConnection connection, IDbTransaction? transaction = null);
	T? Get(int id, IDbConnection connection, IDbTransaction? transaction = null);
	bool Update(T data, IDbConnection connection, IDbTransaction? transaction = null);
	bool Delete(int id, IDbConnection connection, IDbTransaction? transaction = null);
	List<T> GetAll(IDbConnection connection, IDbTransaction? transaction = null);
}

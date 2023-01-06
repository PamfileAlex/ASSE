using System.Data;
using ASSE.Core.Models;

namespace ASSE.DataMapper.Interfaces;
public interface IDataAccess<T> : IDataAccess
	where T : class, IKeyEntity, new()
{
	int Add(T data);
	int Add(T data, IDbConnection connection, IDbTransaction? transaction = null);
	T Get(int id);
	T Get(int id, IDbConnection connection, IDbTransaction? transaction = null);
	bool Update(T data);
	bool Update(T data, IDbConnection connection, IDbTransaction? transaction = null);
	bool Delete(int id);
	bool Delete(int id, IDbConnection connection, IDbTransaction? transaction = null);
	List<T> GetAll();
	List<T> GetAll(IDbConnection connection, IDbTransaction? transaction = null);
}

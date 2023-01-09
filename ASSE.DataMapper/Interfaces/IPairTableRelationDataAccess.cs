using System.Data;
using ASSE.Core.Models;

namespace ASSE.DataMapper.Interfaces;
public interface IPairTableRelationDataAccess<T> : ITableRelationDataAccess
	where T : IRelationEntity
{
	void Add(int first, int second);
	void Add(int first, int second, IDbConnection connection, IDbTransaction? transaction = null);
	bool Delete(int first, int second);
	bool Delete(int first, int second, IDbConnection connection, IDbTransaction? transaction = null);
	List<T> GetAll();
	List<T> GetAll(IDbConnection connection, IDbTransaction? transaction = null);
}

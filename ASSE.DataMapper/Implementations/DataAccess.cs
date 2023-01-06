using ASSE.DataMapper.Interfaces;
using ASSE.DataMapper.Services;

namespace ASSE.DataMapper.Implementations;
public abstract class DataAccess : IDataAccess
{
	protected readonly IDbConnectionProvider _dbConnectionProvider;

	protected DataAccess(IDbConnectionProvider dbConnectionProvider)
	{
		_dbConnectionProvider = dbConnectionProvider;
	}
}
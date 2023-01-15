using ASSE.DataMapper.Interfaces;
using ASSE.DataMapper.Services;
using Serilog;

namespace ASSE.DataMapper.Implementations;
public abstract class DataAccess : IDataAccess
{
	protected readonly IDbConnectionProvider _dbConnectionProvider;
	protected readonly ILogger _logger;

	protected DataAccess(IDbConnectionProvider dbConnectionProvider, ILogger logger)
	{
		_dbConnectionProvider = dbConnectionProvider;
		_logger = logger;
	}
}
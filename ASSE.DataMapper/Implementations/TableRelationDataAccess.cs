using System.Data;
using ASSE.Core.Models;
using ASSE.DataMapper.Interfaces;
using ASSE.DataMapper.Services;
using Dapper;
using Serilog;

namespace ASSE.DataMapper.Implementations;
public abstract class TableRelationDataAccess : DataAccess, ITableRelationDataAccess
{
	protected TableRelationDataAccess(IDbConnectionProvider dbConnectionProvider, ILogger logger)
		: base(dbConnectionProvider, logger)
	{
	}

	protected List<T> GetAllByRelationId<T>(string sql, IDataAccess<T> dataAccess, int id, IDbConnection connection, IDbTransaction? transaction = null)
		where T : class, IKeyEntity, new()
	{
		_logger.Debug("Getting All by relationId: {id}", id);

		List<T> data = dataAccess.GetAll();

		HashSet<int> ids = connection.Query<int>(sql, new { id }, transaction)
			.ToHashSet();

		return data.Where(x => ids.Contains(x.Id)).AsList();
	}
}

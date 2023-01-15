using ASSE.DataMapper.Interfaces;
using ASSE.DataMapper.Services;
using ASSE.DomainModel.Models;
using Serilog;

namespace ASSE.DataMapper.Implementations;
public class CurrencyDataAccess : DataAccess<Currency>, ICurrencyDataAccess
{
	public CurrencyDataAccess(IDbConnectionProvider dbConnectionProvider, ILogger logger)
		: base(dbConnectionProvider, logger)
	{
	}
}

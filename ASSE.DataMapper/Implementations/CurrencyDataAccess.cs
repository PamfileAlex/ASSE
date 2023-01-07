using ASSE.DataMapper.Interfaces;
using ASSE.DataMapper.Services;
using ASSE.DomainModel.Models;

namespace ASSE.DataMapper.Implementations;
public class CurrencyDataAccess : DataAccess<Currency>, ICurrencyDataAccess
{
	public CurrencyDataAccess(IDbConnectionProvider dbConnectionProvider)
		: base(dbConnectionProvider)
	{
	}
}

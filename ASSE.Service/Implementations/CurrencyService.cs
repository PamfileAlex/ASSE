using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.Service.Interfaces;
using FluentValidation;

namespace ASSE.Service.Implementations;
public class CurrencyService : EntityService<Currency, ICurrencyDataAccess>, ICurrencyService
{
	public CurrencyService(ICurrencyDataAccess dataAccess, IValidator<Currency> validator)
		: base(dataAccess, validator)
	{
	}
}

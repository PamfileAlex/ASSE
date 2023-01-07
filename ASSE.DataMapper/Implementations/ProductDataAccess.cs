using ASSE.DataMapper.Interfaces;
using ASSE.DataMapper.Services;
using ASSE.DomainModel.Models;

namespace ASSE.DataMapper.Implementations;
public class ProductDataAccess : DataAccess<Product>, IProductDataAccess
{
	public ProductDataAccess(IDbConnectionProvider dbConnectionProvider)
		: base(dbConnectionProvider)
	{
	}
}

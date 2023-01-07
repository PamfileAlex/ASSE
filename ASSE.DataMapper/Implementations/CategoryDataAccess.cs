using ASSE.DataMapper.Interfaces;
using ASSE.DataMapper.Services;
using ASSE.DomainModel.Models;

namespace ASSE.DataMapper.Implementations;
public class CategoryDataAccess : DataAccess<Category>, ICategoryDataAccess
{
	public CategoryDataAccess(IDbConnectionProvider dbConnectionProvider)
		: base(dbConnectionProvider)
	{
	}
}

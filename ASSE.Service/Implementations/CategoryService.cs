using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.Service.Interfaces;

namespace ASSE.Service.Implementations;
public class CategoryService : EntityService<Category, ICategoryDataAccess>, ICategoryService
{
	public CategoryService(ICategoryDataAccess dataAccess)
		: base(dataAccess)
	{
	}
}

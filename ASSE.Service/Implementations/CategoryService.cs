using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.Service.Interfaces;
using FluentValidation;

namespace ASSE.Service.Implementations;
public class CategoryService : EntityService<Category, ICategoryDataAccess>, ICategoryService
{
	public CategoryService(ICategoryDataAccess dataAccess, IValidator<Category> validator)
		: base(dataAccess, validator)
	{
	}
}

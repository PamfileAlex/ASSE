using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.Service.Interfaces;
using FluentValidation;

namespace ASSE.Service.Implementations;
public class ProductService : EntityService<Product, IProductDataAccess>, IProductService
{
	public ProductService(IProductDataAccess dataAccess, IValidator<Product> validator)
		: base(dataAccess, validator)
	{
	}
}

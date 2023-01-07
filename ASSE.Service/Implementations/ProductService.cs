using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.Service.Interfaces;

namespace ASSE.Service.Implementations;
public class ProductService : EntityService<Product, IProductDataAccess>, IProductService
{
	public ProductService(IProductDataAccess dataAccess)
		: base(dataAccess)
	{
	}
}

using System.Data;
using ASSE.DomainModel.Models;

namespace ASSE.DataMapper.Interfaces;
public interface IProductDataAccess : IDataAccess<Product>
{
	List<Product> GetAllByCategoryId(int categoryId);
	List<Product> GetAllByCategoryId(int categoryId, IDbConnection connection, IDbTransaction? transaction = null);
}

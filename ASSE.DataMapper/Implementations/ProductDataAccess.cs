using System.Data;
using ASSE.DataMapper.Interfaces;
using ASSE.DataMapper.Services;
using ASSE.DomainModel.Models;
using Dapper;
using Serilog;

namespace ASSE.DataMapper.Implementations;
public class ProductDataAccess : DataAccess<Product>, IProductDataAccess
{
	public ProductDataAccess(IDbConnectionProvider dbConnectionProvider, ILogger logger)
		: base(dbConnectionProvider, logger)
	{
	}

	public List<Product> GetAllByCategoryId(int categoryId)
	{
		using IDbConnection connection = _dbConnectionProvider.GetNewConnection();
		connection.Open();
		return GetAllByCategoryId(categoryId, connection);
	}

	public List<Product> GetAllByCategoryId(int categoryId, IDbConnection connection, IDbTransaction? transaction = null)
	{
		_logger.Debug("Getting All by categoryId: {categoryId}", categoryId);

		string sql = @"SELECT * FROM Products
							WHERE CategoryId=@categoryId";

		return connection.Query<Product>(sql, new { categoryId }, transaction).AsList();
	}
}

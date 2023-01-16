// --------------------------------------------------------------------------------------
// <copyright file="ProductDataAccess.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using System.Data;
using ASSE.DataMapper.Interfaces;
using ASSE.DataMapper.Services;
using ASSE.DomainModel.Models;
using Dapper;
using Serilog;

namespace ASSE.DataMapper.Implementations;

/// <summary>
/// Implementation of data access for <see cref="Product"/>.
/// </summary>
public class ProductDataAccess : DataAccess<Product>, IProductDataAccess
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ProductDataAccess"/> class.
	/// </summary>
	/// <param name="dbConnectionProvider">Database connection provider instance.</param>
	/// <param name="logger">Serilog <see cref="ILogger"/> instance.</param>
	public ProductDataAccess(IDbConnectionProvider dbConnectionProvider, ILogger logger)
		: base(dbConnectionProvider, logger)
	{
	}

	/// <inheritdoc/>
	public List<Product> GetAllByCategoryId(int categoryId)
	{
		using IDbConnection connection = _dbConnectionProvider.GetNewConnection();
		connection.Open();
		return GetAllByCategoryId(categoryId, connection);
	}

	/// <inheritdoc/>
	public List<Product> GetAllByCategoryId(int categoryId, IDbConnection connection, IDbTransaction? transaction = null)
	{
		_logger.Debug("Getting All by categoryId: {categoryId}", categoryId);

		string sql = @"SELECT * FROM Products
							WHERE CategoryId=@categoryId";

		return connection.Query<Product>(sql, new { categoryId }, transaction).AsList();
	}
}

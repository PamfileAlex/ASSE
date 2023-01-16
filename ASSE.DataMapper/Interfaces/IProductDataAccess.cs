// --------------------------------------------------------------------------------------
// <copyright file="IProductDataAccess.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using System.Data;
using ASSE.DomainModel.Models;

namespace ASSE.DataMapper.Interfaces;

/// <summary>
/// Represents data access for <see cref="Product"/>.
/// </summary>
public interface IProductDataAccess : IDataAccess<Product>
{
	/// <summary>
	/// Returns a list of all products for a given category.
	/// </summary>
	/// <param name="categoryId">Identity of <see cref="Category"/>.</param>
	/// <returns>List of all products for a given category.</returns>
	List<Product> GetAllByCategoryId(int categoryId);

	/// <summary>
	/// Returns a list of all products for a given category.
	/// </summary>
	/// <param name="categoryId">Identity of <see cref="Category"/>.</param>
	/// <param name="connection">Connection to database.</param>
	/// <param name="transaction">Transaction on connection.</param>
	/// <returns>List of all products for a given category.</returns>
	List<Product> GetAllByCategoryId(int categoryId, IDbConnection connection, IDbTransaction? transaction = null);
}

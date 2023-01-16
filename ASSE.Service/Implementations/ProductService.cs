// --------------------------------------------------------------------------------------
// <copyright file="ProductService.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.Service.Interfaces;
using FluentValidation;

namespace ASSE.Service.Implementations;

/// <summary>
/// Implementation of service for <see cref="Product"/>.
/// </summary>
public class ProductService : EntityService<Product, IProductDataAccess>, IProductService
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ProductService"/> class.
	/// </summary>
	/// <param name="dataAccess"><see cref="Product"/> data access.</param>
	/// <param name="validator"><see cref="Product"/> validator.</param>
	public ProductService(IProductDataAccess dataAccess, IValidator<Product> validator)
		: base(dataAccess, validator)
	{
	}
}

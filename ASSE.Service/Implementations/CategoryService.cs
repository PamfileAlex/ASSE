// --------------------------------------------------------------------------------------
// <copyright file="CategoryService.cs" company="Transilvania University of Brasov">
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
/// Implementation of service for <see cref="Currency"/>.
/// </summary>
public class CategoryService : EntityService<Category, ICategoryDataAccess>, ICategoryService
{
	/// <summary>
	/// Initializes a new instance of the <see cref="CategoryService"/> class.
	/// </summary>
	/// <param name="dataAccess"><see cref="Category"/> data access.</param>
	/// <param name="validator"><see cref="Category"/> validator.</param>
	public CategoryService(ICategoryDataAccess dataAccess, IValidator<Category> validator)
		: base(dataAccess, validator)
	{
	}
}

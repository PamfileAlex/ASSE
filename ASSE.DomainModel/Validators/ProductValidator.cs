// --------------------------------------------------------------------------------------
// <copyright file="ProductValidator.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.DomainModel.Models;
using FluentValidation;

namespace ASSE.DomainModel.Validators;

/// <summary>
/// Validator for <see cref="Product"/>.
/// </summary>
public class ProductValidator : AbstractValidator<Product>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ProductValidator"/> class.
	/// </summary>
	public ProductValidator()
	{
		RuleFor(x => x.CategoryId).NotEmpty();
		RuleFor(x => x.Name).NotEmpty();
	}
}

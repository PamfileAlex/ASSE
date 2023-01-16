// --------------------------------------------------------------------------------------
// <copyright file="Product.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.Core.Models;

namespace ASSE.DomainModel.Models;

/// <summary>
/// Represents a product entity.
/// </summary>
public class Product : IKeyEntity
{
	/// <inheritdoc/>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the category id of the product.
	/// </summary>
	public int CategoryId { get; set; }

	/// <summary>
	/// Gets or sets the name of the product.
	/// By default it is empty.
	/// </summary>
	public string Name { get; set; } = string.Empty;
}

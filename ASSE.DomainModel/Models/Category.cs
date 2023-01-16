// --------------------------------------------------------------------------------------
// <copyright file="Category.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.Core.Models;
using Dapper.Contrib.Extensions;

namespace ASSE.DomainModel.Models;

/// <summary>
/// Represents a category entity.
/// </summary>
[Table("Categories")]
public class Category : IKeyEntity
{
	/// <inheritdoc/>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the parent id of the category.
	/// This property can be <see langword="null"/>.
	/// </summary>
	public int? ParentId { get; set; }

	/// <summary>
	/// Gets or sets the name of the category.
	/// By default it is empty.
	/// </summary>
	public string Name { get; set; } = string.Empty;
}

// --------------------------------------------------------------------------------------
// <copyright file="Role.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.Core.Models;

namespace ASSE.DomainModel.Models;

/// <summary>
/// Represents a role entity.
/// </summary>
public class Role : IKeyEntity
{
	/// <inheritdoc/>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the name of the role.
	/// By default it is empty.
	/// </summary>
	public string Name { get; set; } = string.Empty;
}

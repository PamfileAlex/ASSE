// --------------------------------------------------------------------------------------
// <copyright file="User.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.Core.Models;

namespace ASSE.DomainModel.Models;

/// <summary>
/// Represents a user entity.
/// </summary>
public class User : IKeyEntity
{
	/// <inheritdoc/>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the first name of the user.
	/// By default it is empty.
	/// </summary>
	public string FirstName { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the last name of the user.
	/// By default it is empty.
	/// </summary>
	public string LastName { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the score of the user.
	/// </summary>
	public double Score { get; set; }
}

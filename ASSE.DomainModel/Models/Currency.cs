// --------------------------------------------------------------------------------------
// <copyright file="Currency.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.Core.Models;
using Dapper.Contrib.Extensions;

namespace ASSE.DomainModel.Models;

/// <summary>
/// Represents a currency entity.
/// </summary>
[Table("Currencies")]
public class Currency : IKeyEntity
{
	/// <inheritdoc/>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the name of the currency.
	/// By default it is empty.
	/// </summary>
	public string Name { get; set; } = string.Empty;
}

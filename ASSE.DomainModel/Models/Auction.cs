// --------------------------------------------------------------------------------------
// <copyright file="Auction.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.Core.Models;

namespace ASSE.DomainModel.Models;

/// <summary>
/// Represents an auction entity.
/// </summary>
public class Auction : IKeyEntity
{
	/// <inheritdoc/>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the owner id of the auction.
	/// </summary>
	public int OwnerId { get; set; }

	/// <summary>
	/// Gets or sets the product id of the auction.
	/// </summary>
	public int ProductId { get; set; }

	/// <summary>
	/// Gets or sets the currency id of the auction.
	/// </summary>
	public int CurrencyId { get; set; }

	/// <summary>
	/// Gets or sets the buyer id of the auction.
	/// This property can be <see langword="null"/>.
	/// </summary>
	public int? BuyerId { get; set; }

	/// <summary>
	/// Gets or sets the description of the auction.
	/// By default it is empty.
	/// </summary>
	public string Description { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the start date of the auction.
	/// </summary>
	public DateTime StartDate { get; set; }

	/// <summary>
	/// Gets or sets the end date of the auction.
	/// </summary>
	public DateTime EndDate { get; set; }

	/// <summary>
	/// Gets or sets the starting price of the auction.
	/// </summary>
	public double StartingPrice { get; set; }

	/// <summary>
	/// Gets or sets the current price of the auction.
	/// This property can be <see langword="null"/>.
	/// </summary>
	public double? CurrentPrice { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether the auction is active or not.
	/// </summary>
	public bool IsActive { get; set; }
}

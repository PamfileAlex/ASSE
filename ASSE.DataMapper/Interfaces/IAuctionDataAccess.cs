// --------------------------------------------------------------------------------------
// <copyright file="IAuctionDataAccess.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using System.Data;
using ASSE.DomainModel.Models;

namespace ASSE.DataMapper.Interfaces;

/// <summary>
/// Represents data access for <see cref="Auction"/>.
/// </summary>
public interface IAuctionDataAccess : IDataAccess<Auction>
{
	/// <summary>
	/// Returns a list of all auctions that are active.
	/// </summary>
	/// <returns>List of all active auctions.</returns>
	List<Auction> GetAllActive();

	/// <summary>
	/// Returns a list of all auctions that are active.
	/// </summary>
	/// <param name="connection">Connection to database.</param>
	/// <param name="transaction">Transaction on connection.</param>
	/// <returns>List of all active auctions.</returns>
	List<Auction> GetAllActive(IDbConnection connection, IDbTransaction? transaction = null);

	/// <summary>
	/// Returns a list of all auctions that are active for a given owner.
	/// </summary>
	/// <param name="ownerId">Identity of owner (<see cref="User"/>).</param>
	/// <returns>List of all active auctions for a given owner.</returns>
	List<Auction> GetAllActiveByOwnerId(int ownerId);

	/// <summary>
	/// Returns a list of all auctions that are active for a given owner.
	/// </summary>
	/// <param name="ownerId">Identity of owner (<see cref="User"/>).</param>
	/// <param name="connection">Connection to database.</param>
	/// <param name="transaction">Transaction on connection.</param>
	/// <returns>List of all active auctions for a given owner.</returns>
	List<Auction> GetAllActiveByOwnerId(int ownerId, IDbConnection connection, IDbTransaction? transaction = null);
}

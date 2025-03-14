﻿// --------------------------------------------------------------------------------------
// <copyright file="IAuctionService.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.DomainModel.Models;

namespace ASSE.Service.Interfaces;

/// <summary>
/// Represents service for <see cref="Auction"/>.
/// </summary>
public interface IAuctionService : IEntityService<Auction>
{
	/// <summary>
	/// Returns a list of all auctions that are active.
	/// </summary>
	/// <returns>List of all active auctions.</returns>
	List<Auction> GetAllActive();

	/// <summary>
	/// Returns a list of all auctions that are active for a given owner.
	/// </summary>
	/// <param name="ownerId">Identity of owner (<see cref="User"/>).</param>
	/// <returns>List of all active auctions for a given owner.</returns>
	List<Auction> GetAllActiveByOwnerId(int ownerId);

	/// <summary>
	/// Close the auction by given user.
	/// </summary>
	/// <param name="loggedUserId">Logged <see cref="User"/> identity.</param>
	/// <param name="auction"><see cref="Auction"/> we wish to close.</param>
	/// <returns><see langword="true"/> if auction was closed, <see langword="false"/> otherwise.</returns>
	bool CloseAuction(int loggedUserId, Auction auction);
}

// --------------------------------------------------------------------------------------
// <copyright file="AuctionService.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.Core.Services;
using ASSE.Core.Utils;
using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.Service.Interfaces;
using FluentValidation;
using Serilog;

namespace ASSE.Service.Implementations;

/// <summary>
/// Implementation of service for <see cref="Auction"/>.
/// </summary>
public class AuctionService : EntityService<Auction, IAuctionDataAccess>, IAuctionService
{
	private readonly IConfigProvider _configProvider;

	/// <summary>
	/// Initializes a new instance of the <see cref="AuctionService"/> class.
	/// </summary>
	/// <param name="dataAccess"><see cref="Auction"/> data access.</param>
	/// <param name="validator"><see cref="Auction"/> validator.</param>
	/// <param name="configProvider"><see cref="IConfigProvider"/> instance.</param>
	public AuctionService(IAuctionDataAccess dataAccess, IValidator<Auction> validator, IConfigProvider configProvider)
		: base(dataAccess, validator)
	{
		_configProvider = configProvider;
	}

	/// <inheritdoc/>
	public List<Auction> GetAllActive()
	{
		Log.Debug("Getting all active");
		return DataAccess.GetAllActive();
	}

	/// <inheritdoc/>
	public List<Auction> GetAllActiveByOwnerId(int ownerId)
	{
		Log.Debug("Getting all active by ownerId: {ownerId}", ownerId);
		return DataAccess.GetAllActiveByOwnerId(ownerId);
	}

	/// <inheritdoc/>
	public override bool ValidateAdd(Auction auction)
	{
		Log.Debug("Validating add: {auction}", auction);
		if (!base.ValidateAdd(auction))
		{
			Log.Debug("Failed validator for: {auction}", auction);
			return false;
		}

		if (!ValidateLevenshteinDistance(auction))
		{
			Log.Debug("Failed Levenshtein distance for: {auction}", auction);
			return false;
		}

		if (!ValidateMaxAuctions(auction))
		{
			Log.Debug("Failed max auctions");
			return false;
		}

		return true;
	}

	/// <inheritdoc/>
	public override bool ValidateUpdate(Auction auction)
	{
		Log.Debug("Validating update: {auction}", auction);
		if (!base.ValidateUpdate(auction))
		{
			return false;
		}

		if (!ValidatePrice(auction))
		{
			return false;
		}

		return true;
	}

	/// <summary>
	/// Validates price before updating.
	/// </summary>
	/// <param name="auction">Auction to be validated.</param>
	/// <returns><see langword="true"/> if data is valid, <see langword="false"/> otherwise.</returns>
	public bool ValidatePrice(Auction auction)
	{
		Log.Debug("Validating price for: {auction}", auction);
		if (auction.BuyerId is null && auction.CurrentPrice is null)
		{
			Log.Debug("Early exit from validating price, no buyer");
			return true;
		}

		if (auction.BuyerId is null)
		{
			Log.Debug("There is no buyer");
			return false;
		}

		if (auction.CurrentPrice is null)
		{
			Log.Debug("There is no price");
			return false;
		}

		Auction? previousAuction = Get(auction.Id);
		if (previousAuction is null)
		{
			Log.Debug("There is no previous auction");
			return false;
		}

		if (auction.CurrentPrice <= previousAuction.CurrentPrice)
		{
			Log.Debug("New price cannot be lower than previous price");
			return false;
		}

		if (auction.CurrentPrice > previousAuction.CurrentPrice * 4)
		{
			Log.Debug("New price cannot be more than 300% of previous price");
			return false;
		}

		return true;
	}

	/// <summary>
	/// Validates Levenshtein distance before adding.
	/// </summary>
	/// <param name="auction">Auction to be validated.</param>
	/// <returns><see langword="true"/> if data is valid, <see langword="false"/> otherwise.</returns>
	public bool ValidateLevenshteinDistance(Auction auction)
	{
		Log.Debug("Validating Levenshtein distance for: {auction}", auction);
		var auctions = GetAllActiveByOwnerId(auction.OwnerId);
		foreach (var item in auctions)
		{
			if (!LevenshteinDistance.AreDifferent(auction.Description, item.Description))
			{
				Log.Debug($"Descriptions are too similar: {auction.Description} - {item.Description}");
				return false;
			}
		}

		return true;
	}

	/// <summary>
	/// Validates maximum number of auctions before adding.
	/// </summary>
	/// <param name="auction">Auction to be validated.</param>
	/// <returns><see langword="true"/> if data is valid, <see langword="false"/> otherwise.</returns>
	public bool ValidateMaxAuctions(Auction auction)
	{
		Log.Debug("Validating max auctions");
		var auctions = GetAllActiveByOwnerId(auction.OwnerId);
		var maxAuctions = _configProvider.MaxAuctions;
		if (auctions.Count >= maxAuctions)
		{
			Log.Debug("Maximum limit of auctions reached for user");
			return false;
		}

		return true;
	}
}

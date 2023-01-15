﻿using ASSE.Core.Services;
using ASSE.Core.Utils;
using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.Service.Interfaces;
using FluentValidation;
using Serilog;

namespace ASSE.Service.Implementations;
public class AuctionService : EntityService<Auction, IAuctionDataAccess>, IAuctionService
{
	private readonly IConfigProvider _configProvider;
	public AuctionService(IAuctionDataAccess dataAccess, IValidator<Auction> validator, IConfigProvider configProvider)
		: base(dataAccess, validator)
	{
		_configProvider = configProvider;
	}

	public List<Auction> GetAllActive()
	{
		Log.Debug("Getting all active");
		return _dataAccess.GetAllActive();
	}


	public List<Auction> GetAllActiveByOwnerId(int ownerId)
	{
		Log.Debug("Getting all active by ownerId: {ownerId}", ownerId);
		return _dataAccess.GetAllActiveByOwnerId(ownerId);
	}

	protected override bool ValidateAdd(Auction auction)
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

	protected override bool ValidateUpdate(Auction auction)
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

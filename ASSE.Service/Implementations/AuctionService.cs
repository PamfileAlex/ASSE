using ASSE.Core.Utils;
using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.Service.Interfaces;
using FluentValidation;

namespace ASSE.Service.Implementations;
public class AuctionService : EntityService<Auction, IAuctionDataAccess>, IAuctionService
{
	public AuctionService(IAuctionDataAccess dataAccess, IValidator<Auction> validator)
		: base(dataAccess, validator)
	{
	}

	public List<Auction> GetAllActive()
	{
		return _dataAccess.GetAllActive();
	}


	public List<Auction> GetAllActiveByOwnerId(int ownerId)
	{
		return _dataAccess.GetAllActiveByOwnerId(ownerId);
	}

	protected override bool ValidateAdd(Auction auction)
	{
		if (!base.ValidateAdd(auction))
		{
			return false;
		}

		if (!ValidateLevenshteinDistance(auction))
		{
			return false;
		}
		return true;
	}

	protected override bool ValidateUpdate(Auction auction)
	{
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
		if (auction.BuyerId is null && auction.CurrentPrice is null)
		{
			return true;
		}

		if (auction.BuyerId is null)
		{
			return false;
		}
		if (auction.CurrentPrice is null)
		{
			return false;
		}

		Auction? previousAuction = Get(auction.Id);
		if (previousAuction is null)
		{
			return false;
		}
		if (auction.CurrentPrice <= previousAuction.CurrentPrice)
		{
			return false;
		}
		if (auction.CurrentPrice > previousAuction.CurrentPrice * 4)
		{
			return false;
		}
		return true;
	}

	public bool ValidateLevenshteinDistance(Auction auction)
	{
		var auctions = GetAllActiveByOwnerId(auction.OwnerId);
		if (auctions is null)
		{
			return true;
		}
		foreach (var item in auctions)
		{
			if (!LevenshteinDistance.AreDifferent(auction.Description, item.Description))
			{
				return false;
			}
		}
		return true;
	}
}

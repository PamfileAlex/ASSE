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
}

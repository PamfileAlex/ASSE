using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.Service.Interfaces;

namespace ASSE.Service.Implementations;
public class AuctionService : EntityService<Auction, IAuctionDataAccess>, IAuctionService
{
	public AuctionService(IAuctionDataAccess dataAccess)
		: base(dataAccess)
	{
	}

	public List<Auction> GetAllActive()
	{
		return _dataAccess.GetAllActive();
	}
}

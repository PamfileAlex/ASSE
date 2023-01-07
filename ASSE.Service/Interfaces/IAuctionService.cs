using ASSE.DomainModel.Models;

namespace ASSE.Service.Interfaces;
public interface IAuctionService : IEntityService<Auction>
{
	List<Auction> GetAllActive();
}

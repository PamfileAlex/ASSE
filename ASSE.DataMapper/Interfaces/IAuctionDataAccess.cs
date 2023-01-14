using System.Data;
using ASSE.DomainModel.Models;

namespace ASSE.DataMapper.Interfaces;
public interface IAuctionDataAccess : IDataAccess<Auction>
{
	List<Auction> GetAllActive();
	List<Auction> GetAllActive(IDbConnection connection, IDbTransaction? transaction = null);
	List<Auction> GetAllActiveByOwnerId(int ownerId);
	List<Auction> GetAllActiveByOwnerId(int ownerId, IDbConnection connection, IDbTransaction? transaction = null);
}

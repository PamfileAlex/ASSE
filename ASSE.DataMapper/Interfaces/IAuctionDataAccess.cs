using System.Data;
using ASSE.DomainModel.Models;

namespace ASSE.DataMapper.Interfaces;
public interface IAuctionDataAccess : IDataAccess<Auction>
{
	List<Auction> GetAllActive();
	List<Auction> GetAllActive(IDbConnection connection, IDbTransaction? transaction = null);
}

using ASSE.DataMapper.Interfaces;
using ASSE.DataMapper.Services;
using ASSE.DomainModel.Models;

namespace ASSE.DataMapper.Implementations;
public class UserDataAccess : DataAccess<User>, IUserDataAccess
{
	public UserDataAccess(IDbConnectionProvider dbConnectionProvider)
		: base(dbConnectionProvider)
	{
	}
}

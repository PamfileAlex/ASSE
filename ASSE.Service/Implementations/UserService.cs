using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.Service.Interfaces;

namespace ASSE.Service.Implementations;
public class UserService : EntityService<User, IUserDataAccess>, IUserService
{
	public UserService(IUserDataAccess dataAccess)
		: base(dataAccess)
	{
	}
}

using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.Service.Interfaces;
using FluentValidation;

namespace ASSE.Service.Implementations;
public class UserService : EntityService<User, IUserDataAccess>, IUserService
{
	public UserService(IUserDataAccess dataAccess, IValidator<User> validator)
		: base(dataAccess, validator)
	{
	}
}

using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.DomainModel.Validators;
using ASSE.Service.Interfaces;

namespace ASSE.Service.Implementations;
public class UserService : EntityService<User, IUserDataAccess>, IUserService
{
	private readonly UserValidator _validator;

	public UserService(IUserDataAccess dataAccess, UserValidator validator)
		: base(dataAccess)
	{
		_validator = validator;
	}

	public override int Add(User user)
	{
		if (!_validator.Validate(user).IsValid)
		{
			return default;
		}
		return base.Add(user);
	}
}

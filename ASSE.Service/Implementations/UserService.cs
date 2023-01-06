using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.Service.Interfaces;

namespace ASSE.Service.Implementations;
public class UserService : IUserService
{
	private readonly IUserDataAccess _userDataAccess;

	public UserService(IUserDataAccess userDataAccess)
	{
		_userDataAccess = userDataAccess;
	}

	public int Add(User user)
	{
		return _userDataAccess.Add(user);
	}

	public bool Delete(int id)
	{
		return _userDataAccess.Delete(id);
	}

	public User Get(int id)
	{
		return _userDataAccess.Get(id);
	}

	public List<User> GetAll()
	{
		return _userDataAccess.GetAll();
	}

	public bool Update(User user)
	{
		return _userDataAccess.Update(user);
	}
}

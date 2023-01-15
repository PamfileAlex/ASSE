using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.Service.Interfaces;
using FluentValidation;
using Serilog;

namespace ASSE.Service.Implementations;
public class UserRoleService : IUserRoleService
{
	private readonly IUserRoleDataAccess _dataAccess;
	private readonly IValidator<UserRole> _validator;

	public UserRoleService(IUserRoleDataAccess userRoleDataAccess, IValidator<UserRole> validator)
	{
		_dataAccess = userRoleDataAccess;
		_validator = validator;
	}

	public void Add(int userId, int roleId)
	{
		Add(new UserRole(userId, roleId));
	}

	public void Add(UserRole userRole)
	{
		Log.Debug("Adding: {userRole}", userRole);
		if (!_validator.Validate(userRole).IsValid)
		{
			Log.Debug("Failed validator for: {userRole}", userRole);
			throw new ApplicationException("Invalid data");
		}
		_dataAccess.Add(userRole.UserId, userRole.RoleId);
	}

	public bool Delete(int userId, int roleId)
	{
		return _dataAccess.Delete(userId, roleId);
	}

	public bool Delete(UserRole userRole)
	{
		Log.Debug("Deleting: {userRole}", userRole);
		return _dataAccess.Delete(userRole.UserId, userRole.RoleId);
	}

	public List<UserRole> GetAll()
	{
		Log.Debug("Getting all");
		Log.Debug("Getting all");
		return _dataAccess.GetAll();
	}
}

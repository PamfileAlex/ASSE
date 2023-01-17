// --------------------------------------------------------------------------------------
// <copyright file="UserRoleService.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.Service.Interfaces;
using FluentValidation;
using Serilog;

namespace ASSE.Service.Implementations;

/// <summary>
/// Implementation of service for <see cref="UserRole"/>.
/// </summary>
public class UserRoleService : IUserRoleService
{
	private readonly IUserRoleDataAccess _dataAccess;
	private readonly IValidator<UserRole> _validator;

	/// <summary>
	/// Initializes a new instance of the <see cref="UserRoleService"/> class.
	/// </summary>
	/// <param name="userRoleDataAccess"><see cref="UserRole"/> data access.</param>
	/// <param name="validator"><see cref="UserRole"/> validator.</param>
	public UserRoleService(IUserRoleDataAccess userRoleDataAccess, IValidator<UserRole> validator)
	{
		_dataAccess = userRoleDataAccess;
		_validator = validator;
	}

	/// <inheritdoc/>
	public void Add(int userId, int roleId)
	{
		Add(new UserRole(userId, roleId));
	}

	/// <inheritdoc/>
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

	/// <inheritdoc/>
	public bool Delete(int userId, int roleId)
	{
		return _dataAccess.Delete(userId, roleId);
	}

	/// <inheritdoc/>
	public bool Delete(UserRole userRole)
	{
		Log.Debug("Deleting: {userRole}", userRole);
		return _dataAccess.Delete(userRole.UserId, userRole.RoleId);
	}

	/// <inheritdoc/>
	public List<UserRole> GetAll()
	{
		Log.Debug("Getting all");
		Log.Debug("Getting all");
		return _dataAccess.GetAll();
	}
}

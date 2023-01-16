// --------------------------------------------------------------------------------------
// <copyright file="UserService.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.Service.Interfaces;
using FluentValidation;

namespace ASSE.Service.Implementations;

/// <summary>
/// Implementation of service for <see cref="User"/>.
/// </summary>
public class UserService : EntityService<User, IUserDataAccess>, IUserService
{
	/// <summary>
	/// Initializes a new instance of the <see cref="UserService"/> class.
	/// </summary>
	/// <param name="dataAccess"><see cref="User"/> data access.</param>
	/// <param name="validator"><see cref="User"/> validator.</param>
	public UserService(IUserDataAccess dataAccess, IValidator<User> validator)
		: base(dataAccess, validator)
	{
	}
}

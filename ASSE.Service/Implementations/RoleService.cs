// --------------------------------------------------------------------------------------
// <copyright file="RoleService.cs" company="Transilvania University of Brasov">
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
/// Implementation of service for <see cref="Role"/>.
/// </summary>
public class RoleService : EntityService<Role, IRoleDataAccess>, IRoleService
{
	/// <summary>
	/// Initializes a new instance of the <see cref="RoleService"/> class.
	/// </summary>
	/// <param name="dataAccess"><see cref="Role"/> data access.</param>
	/// <param name="validator"><see cref="Role"/> validator.</param>
	public RoleService(IRoleDataAccess dataAccess, IValidator<Role> validator)
		: base(dataAccess, validator)
	{
	}
}

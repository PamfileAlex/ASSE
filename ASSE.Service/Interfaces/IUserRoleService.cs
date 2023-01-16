// --------------------------------------------------------------------------------------
// <copyright file="IUserRoleService.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.DomainModel.Models;

namespace ASSE.Service.Interfaces;

/// <summary>
/// Represents service for <see cref="Auction"/>.
/// </summary>
public interface IUserRoleService : IEntityService
{
	/// <summary>
	/// Adds relation based on identities.
	/// </summary>
	/// <param name="userId"><see cref="User"/> identity.</param>
	/// <param name="roleId"><see cref="Role"/> identity.</param>
	void Add(int userId, int roleId);

	/// <summary>
	/// Adds relation based on <see cref="UserRole"/> entity.
	/// </summary>
	/// <param name="userRole"><see cref="UserRole"/> entity to be added.</param>
	void Add(UserRole userRole);

	/// <summary>
	/// Deletes relation based on identities.
	/// </summary>
	/// <param name="userId"><see cref="User"/> identity.</param>
	/// <param name="roleId"><see cref="Role"/> identity.</param>
	/// <returns><see langword="true"/> if deleted, <see langword="false"/> if not found.</returns>
	bool Delete(int userId, int roleId);

	/// <summary>
	/// Deletes relation based on <see cref="UserRole"/> entity.
	/// </summary>
	/// <param name="userRole"><see cref="UserRole"/> entity to be deleted.</param>
	/// <returns><see langword="true"/> if deleted, <see langword="false"/> if not found.</returns>
	bool Delete(UserRole userRole);

	/// <summary>
	/// Returns a list of all <see cref="User"/>-<see cref="Role"/> relations.
	/// </summary>
	/// <returns>List of all <see cref="UserRole"/> relations.</returns>
	List<UserRole> GetAll();
}

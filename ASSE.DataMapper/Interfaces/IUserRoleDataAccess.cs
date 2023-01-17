// --------------------------------------------------------------------------------------
// <copyright file="IUserRoleDataAccess.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using System.Data;
using ASSE.DomainModel.Models;

namespace ASSE.DataMapper.Interfaces;

/// <summary>
/// Represents data access for <see cref="UserRole"/>.
/// </summary>
public interface IUserRoleDataAccess : IPairTableRelationDataAccess<UserRole>
{
	/// <summary>
	/// Adds relation based on identities.
	/// </summary>
	/// <param name="userId"><see cref="User"/> identity.</param>
	/// <param name="roleId"><see cref="Role"/> identity.</param>
	new void Add(int userId, int roleId);

	/// <summary>
	/// Adds relation based on identities.
	/// </summary>
	/// <param name="userId"><see cref="User"/> identity.</param>
	/// <param name="roleId"><see cref="Role"/> identity.</param>
	/// <param name="connection">Connection to database.</param>
	/// <param name="transaction">Transaction on connection.</param>
	new void Add(int userId, int roleId, IDbConnection connection, IDbTransaction? transaction = null);

	/// <summary>
	/// Deletes relation based on identities.
	/// </summary>
	/// <param name="userId"><see cref="User"/> identity.</param>
	/// <param name="roleId"><see cref="Role"/> identity.</param>
	/// <returns><see langword="true"/> if deleted, <see langword="false"/> if not found.</returns>
	new bool Delete(int userId, int roleId);

	/// <summary>
	/// Deletes relation based on identities.
	/// </summary>
	/// <param name="userId"><see cref="User"/> identity.</param>
	/// <param name="roleId"><see cref="Role"/> identity.</param>
	/// <param name="connection">Connection to database.</param>
	/// <param name="transaction">Transaction on connection.</param>
	/// <returns><see langword="true"/> if deleted, <see langword="false"/> if not found.</returns>
	new bool Delete(int userId, int roleId, IDbConnection connection, IDbTransaction? transaction = null);

	/// <summary>
	/// Returns a list of all <see cref="User"/>-<see cref="Role"/> relations.
	/// </summary>
	/// <returns>List of all <see cref="UserRole"/> relations.</returns>
	new List<UserRole> GetAll();

	/// <summary>
	/// Returns a list of all <see cref="User"/>-<see cref="Role"/> relations.
	/// </summary>
	/// <param name="connection">Connection to database.</param>
	/// <param name="transaction">Transaction on connection.</param>
	/// <returns>List of all <see cref="UserRole"/> relations.</returns>
	new List<UserRole> GetAll(IDbConnection connection, IDbTransaction? transaction = null);

	/// <summary>
	/// Returns a list of all <see cref="User"/>-<see cref="Role"/> relations by role identity.
	/// </summary>
	/// <param name="roleId"><see cref="Role"/> identity.</param>
	/// <returns>List of all <see cref="UserRole"/> relations by role identity.</returns>
	List<User> GetAllUsersByRoleId(int roleId);

	/// <summary>
	/// Returns a list of all <see cref="User"/>-<see cref="Role"/> relations by role identity.
	/// </summary>
	/// <param name="roleId"><see cref="Role"/> identity.</param>
	/// <param name="connection">Connection to database.</param>
	/// <param name="transaction">Transaction on connection.</param>
	/// <returns>List of all <see cref="UserRole"/> relations by role identity.</returns>
	List<User> GetAllUsersByRoleId(int roleId, IDbConnection connection, IDbTransaction? transaction = null);

	/// <summary>
	/// Returns a list of all <see cref="User"/>-<see cref="Role"/> relations by user identity.
	/// </summary>
	/// <param name="userId"><see cref="User"/> identity.</param>
	/// <returns>List of all <see cref="UserRole"/> relations by user identity.</returns>
	List<Role> GetAllRolesByUserId(int userId);

	/// <summary>
	/// Returns a list of all <see cref="User"/>-<see cref="Role"/> relations by user identity.
	/// </summary>
	/// <param name="userId"><see cref="User"/> identity.</param>
	/// <param name="connection">Connection to database.</param>
	/// <param name="transaction">Transaction on connection.</param>
	/// <returns>List of all <see cref="UserRole"/> relations by user identity.</returns>
	List<Role> GetAllRolesByUserId(int userId, IDbConnection connection, IDbTransaction? transaction = null);
}

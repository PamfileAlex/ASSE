// --------------------------------------------------------------------------------------
// <copyright file="UserRoleDataAccess.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using System.Data;
using ASSE.Core.Extensions;
using ASSE.DataMapper.Interfaces;
using ASSE.DataMapper.Services;
using ASSE.DomainModel.Models;
using Dapper;
using Serilog;

namespace ASSE.DataMapper.Implementations;

/// <summary>
/// Implementation of data access for <see cref="UserRole"/>.
/// </summary>
public class UserRoleDataAccess : PairTableRelationDataAccess<UserRole>, IUserRoleDataAccess
{
	/// <summary>
	/// Data access for <see cref="User"/>.
	/// </summary>
	protected readonly IUserDataAccess _userDataAccess;

	/// <summary>
	/// Data access for <see cref="Role"/>.
	/// </summary>
	protected readonly IRoleDataAccess _roleDataAccess;

	/// <summary>
	/// Initializes a new instance of the <see cref="UserRoleDataAccess"/> class.
	/// </summary>
	/// <param name="dbConnectionProvider">Database connection provider instance.</param>
	/// <param name="userDataAccess">Instance of user data access.</param>
	/// <param name="roleDataAccess">Instance of role data access.</param>
	/// <param name="logger">Serilog <see cref="ILogger"/> instance.</param>
	public UserRoleDataAccess(
		IDbConnectionProvider dbConnectionProvider,
		IUserDataAccess userDataAccess,
		IRoleDataAccess roleDataAccess,
		ILogger logger)
		: base(dbConnectionProvider, logger)
	{
		_userDataAccess = userDataAccess;
		_roleDataAccess = roleDataAccess;
	}

	/// <inheritdoc cref="IUserRoleDataAccess.Add(int, int)"/>
	public override void Add(int userId, int roleId)
	{
		base.Add(userId, roleId);
	}

	/// <inheritdoc cref="IUserRoleDataAccess.Add(int, int, IDbConnection, IDbTransaction?)"/>
	public override void Add(int userId, int roleId, IDbConnection connection, IDbTransaction? transaction = null)
	{
		_logger.Debug("Adding: ({userId}, {roleId})", userId, roleId);

		string sql = @"INSERT INTO User_Role
						(UserId, RoleId)
						VALUES (@userId, @roleId)";

		connection.Execute(sql, new { userId, roleId }, transaction);
	}

	/// <inheritdoc cref="IUserRoleDataAccess.Delete(int, int)"/>
	public override bool Delete(int userId, int roleId)
	{
		return base.Delete(userId, roleId);
	}

	/// <inheritdoc cref="IUserRoleDataAccess.Delete(int, int, IDbConnection, IDbTransaction?)"/>
	public override bool Delete(int userId, int roleId, IDbConnection connection, IDbTransaction? transaction = null)
	{
		_logger.Debug("Deleting: ({userId}, {roleId})", userId, roleId);

		string sql = @"DELETE FROM User_Role
							WHERE UserId=@userId
							AND RoleId=@roleId";

		return connection.Execute(sql, new { userId, roleId }, transaction).ToBoolean();
	}

	/// <inheritdoc  cref="IUserRoleDataAccess.GetAll"/>
	public override List<UserRole> GetAll()
	{
		return base.GetAll();
	}

	/// <inheritdoc cref="IUserRoleDataAccess.GetAll(IDbConnection, IDbTransaction?)"/>
	public override List<UserRole> GetAll(IDbConnection connection, IDbTransaction? transaction = null)
	{
		_logger.Debug("Getting all");

		string sql = @"SELECT * FROM User_Role";

		return connection.Query(sql)
			.AsList()
			.ConvertAll(x => new UserRole((int)x.UserId, (int)x.RoleId));
	}

	/// <inheritdoc/>
	public List<User> GetAllUsersByRoleId(int roleId)
	{
		using IDbConnection connection = _dbConnectionProvider.GetNewConnection();
		connection.Open();
		return GetAllUsersByRoleId(roleId, connection);
	}

	/// <inheritdoc/>
	public List<User> GetAllUsersByRoleId(int roleId, IDbConnection connection, IDbTransaction? transaction = null)
	{
		_logger.Debug("Getting all users by roleId: {roleId}", roleId);

		string sql = @"SELECT UserId FROM User_Role
							WHERE RoleId=@id";

		return GetAllByRelationId<User>(sql, _userDataAccess, roleId, connection, transaction);
	}

	/// <inheritdoc/>
	public List<Role> GetAllRolesByUserId(int userId)
	{
		using IDbConnection connection = _dbConnectionProvider.GetNewConnection();
		connection.Open();
		return GetAllRolesByUserId(userId, connection);
	}

	/// <inheritdoc/>
	public List<Role> GetAllRolesByUserId(int userId, IDbConnection connection, IDbTransaction? transaction = null)
	{
		_logger.Debug("Getting all roles by userId: {userId}", userId);

		string sql = @"SELECT RoleId FROM User_Role
							WHERE UserId=@id";

		return GetAllByRelationId<Role>(sql, _roleDataAccess, userId, connection, transaction);
	}
}

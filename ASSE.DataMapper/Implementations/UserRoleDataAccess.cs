using System.Data;
using ASSE.Core.Extensions;
using ASSE.DataMapper.Interfaces;
using ASSE.DataMapper.Services;
using ASSE.DomainModel.Models;
using Dapper;
using Serilog;

namespace ASSE.DataMapper.Implementations;
public class UserRoleDataAccess : PairTableRelationDataAccess<UserRole>, IUserRoleDataAccess
{
	protected readonly IUserDataAccess _userDataAccess;
	protected readonly IRoleDataAccess _roleDataAccess;
	public UserRoleDataAccess(IDbConnectionProvider dbConnectionProvider,
		IUserDataAccess userDataAccess, IRoleDataAccess roleDataAccess, ILogger logger)
		: base(dbConnectionProvider, logger)
	{
		_userDataAccess = userDataAccess;
		_roleDataAccess = roleDataAccess;
	}

	public override void Add(int userId, int roleId)
	{
		base.Add(userId, roleId);
	}

	public override void Add(int userId, int roleId, IDbConnection connection, IDbTransaction? transaction = null)
	{
		_logger.Debug("Adding: ({userId}, {roleId})", userId, roleId);

		string sql = @"INSERT INTO User_Role
						(UserId, RoleId)
						VALUES (@userId, @roleId)";

		connection.Execute(sql, new { userId, roleId }, transaction);
	}

	public override bool Delete(int userId, int roleId)
	{
		return base.Delete(userId, roleId);
	}

	public override bool Delete(int userId, int roleId, IDbConnection connection, IDbTransaction? transaction = null)
	{
		_logger.Debug("Deleting: ({userId}, {roleId})", userId, roleId);

		string sql = @"DELETE FROM User_Role
							WHERE UserId=@userId
							AND RoleId=@roleId";

		return connection.Execute(sql, new { userId, roleId }, transaction).ToBoolean();
	}

	public override List<UserRole> GetAll(IDbConnection connection, IDbTransaction? transaction = null)
	{
		_logger.Debug("Getting all");

		string sql = @"SELECT * FROM User_Role";

		return connection.Query(sql)
			.AsList()
			.ConvertAll(x => new UserRole((int)x.UserId, (int)x.RoleId));
	}


	public List<User> GetAllUsersByRoleId(int roleId)
	{
		using IDbConnection connection = _dbConnectionProvider.GetNewConnection();
		connection.Open();
		return GetAllUsersByRoleId(roleId, connection);
	}

	public List<User> GetAllUsersByRoleId(int roleId, IDbConnection connection, IDbTransaction? transaction = null)
	{
		_logger.Debug("Getting all users by roleId: {roleId}", roleId);

		string sql = @"SELECT UserId FROM User_Role
							WHERE RoleId=@id";

		return GetAllByRelationId<User>(sql, _userDataAccess, roleId, connection, transaction);
	}

	public List<Role> GetAllRolesByUserId(int userId)
	{
		using IDbConnection connection = _dbConnectionProvider.GetNewConnection();
		connection.Open();
		return GetAllRolesByUserId(userId, connection);
	}

	public List<Role> GetAllRolesByUserId(int userId, IDbConnection connection, IDbTransaction? transaction = null)
	{
		_logger.Debug("Getting all roles by userId: {userId}", userId);

		string sql = @"SELECT RoleId FROM User_Role
							WHERE UserId=@id";

		return GetAllByRelationId<Role>(sql, _roleDataAccess, userId, connection, transaction);
	}
}

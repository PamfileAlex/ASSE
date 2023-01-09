using System.Data;
using ASSE.DomainModel.Models;

namespace ASSE.DataMapper.Interfaces;
internal interface IUserRoleDataAccess : IPairTableRelationDataAccess<UserRole>
{
	new void Add(int userId, int roleId);
	new void Add(int userId, int roleId, IDbConnection connection, IDbTransaction? transaction = null);
	new bool Delete(int userId, int roleId);
	new bool Delete(int userId, int roleId, IDbConnection connection, IDbTransaction? transaction = null);
	new List<UserRole> GetAll();
	new List<UserRole> GetAll(IDbConnection connection, IDbTransaction? transaction = null);
	List<User> GetAllUsersByRoleId(int roleId);
	List<User> GetAllUsersByRoleId(int roleId, IDbConnection connection, IDbTransaction? transaction = null);
	List<Role> GetAllRolesByUserId(int userId);
	List<Role> GetAllRolesByUserId(int userId, IDbConnection connection, IDbTransaction? transaction = null);
}

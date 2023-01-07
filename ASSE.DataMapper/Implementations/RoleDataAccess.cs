using ASSE.DataMapper.Interfaces;
using ASSE.DataMapper.Services;
using ASSE.DomainModel.Models;

namespace ASSE.DataMapper.Implementations;
public class RoleDataAccess : DataAccess<Role>, IRoleDataAccess
{
	public RoleDataAccess(IDbConnectionProvider dbConnectionProvider)
		: base(dbConnectionProvider)
	{
	}
}

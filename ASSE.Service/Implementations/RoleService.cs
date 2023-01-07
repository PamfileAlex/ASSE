using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.Service.Interfaces;

namespace ASSE.Service.Implementations;
public class RoleService : EntityService<Role, IRoleDataAccess>, IRoleService
{
	public RoleService(IRoleDataAccess dataAccess)
		: base(dataAccess)
	{
	}
}

using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.Service.Interfaces;
using FluentValidation;

namespace ASSE.Service.Implementations;
public class RoleService : EntityService<Role, IRoleDataAccess>, IRoleService
{
	public RoleService(IRoleDataAccess dataAccess, IValidator<Role> validator)
		: base(dataAccess, validator)
	{
	}
}

using ASSE.DomainModel.Models;

namespace ASSE.Service.Interfaces;
public interface IUserService : IEntityService<User>
{
	void AddRole(Role role);
}

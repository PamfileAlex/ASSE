using ASSE.DomainModel.Models;

namespace ASSE.Service.Interfaces;
public interface IUserRoleService : IEntityService
{
	void Add(int userId, int roleId);
	void Add(UserRole userRole);
	bool Delete(int userId, int roleId);
	bool Delete(UserRole userRole);
	List<UserRole> GetAll();
}

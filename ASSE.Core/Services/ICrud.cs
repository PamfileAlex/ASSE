using ASSE.Core.Models;

namespace ASSE.Core.Services;
public interface ICrud<T> : IService
	where T : class, IKeyEntity, new()
{
	int Add(T data);
	T Get(int id);
	bool Update(T data);
	bool Delete(int id);
	List<T> GetAll();
}

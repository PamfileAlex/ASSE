using ASSE.Core.Models;
using ASSE.Core.Services;

namespace ASSE.Service.Interfaces;
public interface IEntityService<T> : IService
    where T : class, IKeyEntity, new()
{
    int Add(T data);
    T Get(int id);
    bool Update(T data);
    bool Delete(int id);
    List<T> GetAll();
}

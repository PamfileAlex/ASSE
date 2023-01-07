using ASSE.Core.Models;
using ASSE.Core.Services;

namespace ASSE.Service.Interfaces;
public interface IEntityService<T> : ICrud<T>
	where T : class, IKeyEntity, new()
{
}

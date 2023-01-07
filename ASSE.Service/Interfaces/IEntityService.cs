using ASSE.Core.Models;
using ASSE.Core.Services;

namespace ASSE.Service.Interfaces;

public interface IEntityService : IService
{
}

public interface IEntityService<T> : IEntityService, ICrud<T>
	where T : class, IKeyEntity, new()
{
}

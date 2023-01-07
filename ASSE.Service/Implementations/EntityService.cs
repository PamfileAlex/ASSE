using ASSE.Core.Models;
using ASSE.DataMapper.Interfaces;
using ASSE.Service.Interfaces;

namespace ASSE.Service.Implementations;

public abstract class EntityService<T> : EntityService<T, IDataAccess<T>>
	where T : class, IKeyEntity, new()
{
	public EntityService(IDataAccess<T> dataAccess)
		: base(dataAccess)
	{
	}
}

public abstract class EntityService<T, D> : GenericService<D>, IEntityService<T>
	where T : class, IKeyEntity, new()
	where D : IDataAccess<T>
{
	public EntityService(D dataAccess)
		: base(dataAccess)
	{
	}

	public virtual int Add(T data)
	{
		return _dataAccess.Add(data);
	}

	public virtual T Get(int id)
	{
		return _dataAccess.Get(id);
	}

	public virtual bool Update(T data)
	{
		return _dataAccess.Update(data);
	}

	public virtual bool Delete(int id)
	{
		return _dataAccess.Delete(id);
	}

	public virtual List<T> GetAll()
	{
		return _dataAccess.GetAll();
	}
}

using ASSE.Core.Models;
using ASSE.DataMapper.Interfaces;
using ASSE.Service.Interfaces;
using FluentValidation;

namespace ASSE.Service.Implementations;

public abstract class EntityService<T> : EntityService<T, IDataAccess<T>>
	where T : class, IKeyEntity, new()
{
	public EntityService(IDataAccess<T> dataAccess, IValidator<T> validator)
		: base(dataAccess, validator)
	{
	}
}

public abstract class EntityService<T, D> : GenericService<D>, IEntityService<T>
	where T : class, IKeyEntity, new()
	where D : IDataAccess<T>
{
	private readonly IValidator<T> _validator;

	public EntityService(D dataAccess, IValidator<T> validator)
		: base(dataAccess)
	{
		_validator = validator;
	}

	public virtual int Add(T data)
	{
		if (_validator.Validate(data).IsValid)
		{
			return _dataAccess.Add(data);
		}
		return default;
	}

	public virtual T? Get(int id)
	{
		return _dataAccess.Get(id);
	}

	public virtual bool Update(T data)
	{
		if (data.Id <= 0)
		{
			return false;
		}
		if (_validator.Validate(data).IsValid)
		{
			return _dataAccess.Update(data);
		}
		return false;
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

using ASSE.Core.Models;
using ASSE.DataMapper.Interfaces;
using ASSE.Service.Interfaces;
using FluentValidation;
using Serilog;

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
	protected readonly IValidator<T> _validator;

	public EntityService(D dataAccess, IValidator<T> validator)
		: base(dataAccess)
	{
		_validator = validator;
	}

	public virtual int Add(T data)
	{
		Log.Debug("Adding: {data}", data);
		if (!ValidateAdd(data))
		{
			return default;
		}
		return _dataAccess.Add(data);
	}

	protected virtual bool ValidateAdd(T data)
	{
		Log.Debug("Validating add: {data}", data);
		return _validator.Validate(data).IsValid;
	}

	public virtual T? Get(int id)
	{
		Log.Debug("Getting by id: {id}", id);
		return _dataAccess.Get(id);
	}

	public virtual bool Update(T data)
	{
		Log.Debug("Updating: {data}", data);
		if (!ValidateUpdate(data))
		{
			return false;
		}
		return _dataAccess.Update(data);
	}

	protected virtual bool ValidateUpdate(T data)
	{
		Log.Debug("Validating update: {data}", data);
		if (data.Id <= 0)
		{
			return false;
		}
		return _validator.Validate(data).IsValid;
	}

	public virtual bool Delete(int id)
	{
		Log.Debug("Deleting by id: {id}", id);
		return _dataAccess.Delete(id);
	}

	public virtual List<T> GetAll()
	{
		Log.Debug("Getting all");
		return _dataAccess.GetAll();
	}
}

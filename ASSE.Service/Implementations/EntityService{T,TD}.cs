// --------------------------------------------------------------------------------------
// <copyright file="EntityService{T,TD}.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.Core.Models;
using ASSE.DataMapper.Interfaces;
using ASSE.Service.Interfaces;
using FluentValidation;
using Serilog;

namespace ASSE.Service.Implementations;

/// <summary>
/// Abstract entity service for <see cref="IKeyEntity"/> that has a data access that implements <see cref="IDataAccess{T}"/>.
/// </summary>
/// <typeparam name="T">Type of entity that implements <see cref="IKeyEntity"/>.</typeparam>
/// <typeparam name="TD">Type for data access that implements <see cref="IDataAccess"/>.</typeparam>
public abstract class EntityService<T, TD> : GenericService<TD>, IEntityService<T>
	where T : class, IKeyEntity, new()
	where TD : IDataAccess<T>
{
	/// <summary>
	/// Validator for entity of type <typeparamref name="T"/> that implements <see cref="IKeyEntity"/>.
	/// </summary>
	protected readonly IValidator<T> _validator;

	/// <summary>
	/// Initializes a new instance of the <see cref="EntityService{T, TD}"/> class.
	/// </summary>
	/// <param name="dataAccess">Data access that implements <see cref="IDataAccess"/>.</param>
	/// <param name="validator">Validator for entity of type <typeparamref name="T"/> that implements <see cref="IKeyEntity"/>.</param>
	public EntityService(TD dataAccess, IValidator<T> validator)
		: base(dataAccess)
	{
		_validator = validator;
	}

	/// <inheritdoc/>
	public virtual int Add(T data)
	{
		Log.Debug("Adding: {data}", data);
		if (!ValidateAdd(data))
		{
			return default;
		}

		return _dataAccess.Add(data);
	}

	/// <summary>
	/// Validates entity to be added.
	/// </summary>
	/// <param name="data">Entity to be verified of type <typeparamref name="T"/>.</param>
	/// <returns><see langword="true"/> if data is valid, <see langword="false"/> otherwise.</returns>
	public virtual bool ValidateAdd(T data)
	{
		Log.Debug("Validating add: {data}", data);
		return _validator.Validate(data).IsValid;
	}

	/// <inheritdoc/>
	public virtual T? Get(int id)
	{
		Log.Debug("Getting by id: {id}", id);
		return _dataAccess.Get(id);
	}

	/// <inheritdoc/>
	public virtual bool Update(T data)
	{
		Log.Debug("Updating: {data}", data);
		if (!ValidateUpdate(data))
		{
			return false;
		}

		return _dataAccess.Update(data);
	}

	/// <summary>
	/// Validates entity to be updated.
	/// </summary>
	/// <param name="data">Entity to be verified of type <typeparamref name="T"/>.</param>
	/// <returns><see langword="true"/> if data is valid, <see langword="false"/> otherwise.</returns>
	public virtual bool ValidateUpdate(T data)
	{
		Log.Debug("Validating update: {data}", data);
		if (data.Id <= 0)
		{
			return false;
		}

		return _validator.Validate(data).IsValid;
	}

	/// <inheritdoc/>
	public virtual bool Delete(int id)
	{
		Log.Debug("Deleting by id: {id}", id);
		return _dataAccess.Delete(id);
	}

	/// <inheritdoc/>
	public virtual List<T> GetAll()
	{
		Log.Debug("Getting all");
		return _dataAccess.GetAll();
	}
}

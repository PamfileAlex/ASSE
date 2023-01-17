// --------------------------------------------------------------------------------------
// <copyright file="EntityService{T}.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.Core.Models;
using ASSE.DataMapper.Interfaces;
using FluentValidation;

namespace ASSE.Service.Implementations;

/// <summary>
/// Abstract entity service for <see cref="IKeyEntity"/> that has a data access of <see cref="IDataAccess{T}"/>.
/// </summary>
/// <typeparam name="T">Type of entity that implements <see cref="IKeyEntity"/>.</typeparam>
public abstract class EntityService<T> : EntityService<T, IDataAccess<T>>
	where T : class, IKeyEntity, new()
{
	/// <summary>
	/// Initializes a new instance of the <see cref="EntityService{T}"/> class.
	/// </summary>
	/// <param name="dataAccess">Data access that implements <see cref="IDataAccess"/>.</param>
	/// <param name="validator">Validator for entity of type <typeparamref name="T"/> that implements <see cref="IKeyEntity"/>.</param>
	public EntityService(IDataAccess<T> dataAccess, IValidator<T> validator)
		: base(dataAccess, validator)
	{
	}
}

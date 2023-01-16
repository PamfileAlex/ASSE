// --------------------------------------------------------------------------------------
// <copyright file="ICrud.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.Core.Models;

namespace ASSE.Core.Services;

/// <summary>
/// Interface for CRUD operations. Should be implemented by data access and service classes for entities of type <see cref="IKeyEntity"/>.
/// </summary>
/// <typeparam name="T">The type on which the CRUD operations are performed.</typeparam>
public interface ICrud<T> : IService
	where T : class, IKeyEntity, new()
{
	/// <summary>
	/// Adds entity.
	/// </summary>
	/// <param name="data">Entity to be added of type <typeparamref name="T"/>.</param>
	/// <returns>Identity of added entity.</returns>
	int Add(T data);

	/// <summary>
	/// Returns a single entity by provided <paramref name="id"/>.
	/// </summary>
	/// <param name="id">Identity of searched entity.</param>
	/// <returns>Entity of <typeparamref name="T"/> if found, otherwise <see langword="null"/>.</returns>
	T? Get(int id);

	/// <summary>
	/// Updates entity.
	/// </summary>
	/// <param name="data">Entity to be updated.</param>
	/// <returns><see langword="true"/> if updated, <see langword="false"/> if not found or not modified.</returns>
	bool Update(T data);

	/// <summary>
	/// Deletes entity.
	/// </summary>
	/// <param name="id">Identity of entity to delete.</param>
	/// <returns><see langword="true"/> if deleted, <see langword="false"/> if not found.</returns>
	bool Delete(int id);

	/// <summary>
	/// Returns a list of all entities of type <typeparamref name="T"/>.
	/// </summary>
	/// <returns><see cref="List{T}"/> of all entities.</returns>
	List<T> GetAll();
}

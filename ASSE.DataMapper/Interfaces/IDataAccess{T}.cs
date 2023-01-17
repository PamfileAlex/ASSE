// --------------------------------------------------------------------------------------
// <copyright file="IDataAccess{T}.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using System.Data;
using ASSE.Core.Models;
using ASSE.Core.Services;

namespace ASSE.DataMapper.Interfaces;

/// <summary>
/// Represents database data access for <see cref="IKeyEntity"/>.
/// Extension of <see cref="ICrud{T}"/>.
/// </summary>
/// <typeparam name="T">Type of entity that implements <see cref="IKeyEntity"/>.</typeparam>
public interface IDataAccess<T> : IDataAccess, ICrud<T>
	where T : class, IKeyEntity, new()
{
	/// <summary>
	/// Adds entity.
	/// </summary>
	/// <param name="data">Entity to be added of type <typeparamref name="T"/>.</param>
	/// <param name="connection">Connection to database.</param>
	/// <param name="transaction">Transaction on connection.</param>
	/// <returns>Identity of added entity.</returns>
	int Add(T data, IDbConnection connection, IDbTransaction? transaction = null);

	/// <summary>
	/// Returns a single entity by provided <paramref name="id"/>.
	/// </summary>
	/// <param name="id">Identity of searched entity.</param>
	/// <param name="connection">Connection to database.</param>
	/// <param name="transaction">Transaction on connection.</param>
	/// <returns>Entity of <typeparamref name="T"/> if found, otherwise <see langword="null"/>.</returns>
	T? Get(int id, IDbConnection connection, IDbTransaction? transaction = null);

	/// <summary>
	/// Updates entity.
	/// </summary>
	/// <param name="data">Entity to be updated.</param>
	/// <param name="connection">Connection to database.</param>
	/// <param name="transaction">Transaction on connection.</param>
	/// <returns><see langword="true"/> if updated, <see langword="false"/> if not found or not modified.</returns>
	bool Update(T data, IDbConnection connection, IDbTransaction? transaction = null);

	/// <summary>
	/// Deletes entity.
	/// </summary>
	/// <param name="id">Identity of entity to delete.</param>
	/// <param name="connection">Connection to database.</param>
	/// <param name="transaction">Transaction on connection.</param>
	/// <returns><see langword="true"/> if deleted, <see langword="false"/> if not found.</returns>
	bool Delete(int id, IDbConnection connection, IDbTransaction? transaction = null);

	/// <summary>
	/// Returns a list of all entities of type <typeparamref name="T"/>.
	/// </summary>
	/// <param name="connection">Connection to database.</param>
	/// <param name="transaction">Transaction on connection.</param>
	/// <returns><see cref="List{T}"/> of all entities.</returns>
	List<T> GetAll(IDbConnection connection, IDbTransaction? transaction = null);
}

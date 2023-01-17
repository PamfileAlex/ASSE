// --------------------------------------------------------------------------------------
// <copyright file="IPairTableRelationDataAccess.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using System.Data;
using ASSE.Core.Models;

namespace ASSE.DataMapper.Interfaces;

/// <summary>
/// Represents data access for pair table relation where table relation is represented by entity of type <see cref="IRelationEntity"/>.
/// </summary>
/// <typeparam name="T">The type of relation that implements <see cref="IRelationEntity"/>.</typeparam>
public interface IPairTableRelationDataAccess<T> : ITableRelationDataAccess
	where T : IRelationEntity
{
	/// <summary>
	/// Adds relation based on identities.
	/// </summary>
	/// <param name="first">First identity.</param>
	/// <param name="second">Second identity.</param>
	void Add(int first, int second);

	/// <summary>
	/// Adds relation based on identities.
	/// </summary>
	/// <param name="first">First identity.</param>
	/// <param name="second">Second identity.</param>
	/// <param name="connection">Connection to database.</param>
	/// <param name="transaction">Transaction on connection.</param>
	void Add(int first, int second, IDbConnection connection, IDbTransaction? transaction = null);

	/// <summary>
	/// Deletes relation based on identities.
	/// </summary>
	/// <param name="first">First identity.</param>
	/// <param name="second">Second identity.</param>
	/// <returns><see langword="true"/> if deleted, <see langword="false"/> if not found.</returns>
	bool Delete(int first, int second);

	/// <summary>
	/// Deletes relation based on identities.
	/// </summary>
	/// <param name="first">First identity.</param>
	/// <param name="second">Second identity.</param>
	/// <param name="connection">Connection to database.</param>
	/// <param name="transaction">Transaction on connection.</param>
	/// <returns><see langword="true"/> if deleted, <see langword="false"/> if not found.</returns>
	bool Delete(int first, int second, IDbConnection connection, IDbTransaction? transaction = null);

	/// <summary>
	/// Returns a list of all relation entities.
	/// </summary>
	/// <returns>List of all relation entities.</returns>
	List<T> GetAll();

	/// <summary>
	/// Returns a list of all relation entities.
	/// </summary>
	/// <param name="connection">Connection to database.</param>
	/// <param name="transaction">Transaction on connection.</param>
	/// <returns>List of all relation entities.</returns>
	List<T> GetAll(IDbConnection connection, IDbTransaction? transaction = null);
}

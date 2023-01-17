// --------------------------------------------------------------------------------------
// <copyright file="IScoreHistoryDataAccess.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using System.Data;
using ASSE.DomainModel.Models;

namespace ASSE.DataMapper.Interfaces;

/// <summary>
/// Represents data access for <see cref="ScoreHistory"/>.
/// </summary>
public interface IScoreHistoryDataAccess : IDataAccess<ScoreHistory>
{
	/// <summary>
	/// Returns a list of all score histories for a given user.
	/// </summary>
	/// <param name="userId">Identity of user (<see cref="User"/>).</param>
	/// <returns>List of all score histories for a given user.</returns>
	List<ScoreHistory> GetAllByUserId(int userId);

	/// <summary>
	/// Returns a list of all score histories for a given user.
	/// </summary>
	/// <param name="userId">Identity of user (<see cref="User"/>).</param>
	/// <param name="connection">Connection to database.</param>
	/// <param name="transaction">Transaction on connection.</param>
	/// <returns>List of all score histories for a given user.</returns>
	List<ScoreHistory> GetAllByUserId(int userId, IDbConnection connection, IDbTransaction? transaction = null);
}

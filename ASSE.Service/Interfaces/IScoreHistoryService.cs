// --------------------------------------------------------------------------------------
// <copyright file="IScoreHistoryService.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.DomainModel.Models;

namespace ASSE.Service.Interfaces;

/// <summary>
/// Represents service for <see cref="ScoreHistory"/>.
/// </summary>
public interface IScoreHistoryService : IEntityService<ScoreHistory>
{
	/// <summary>
	/// Calculates score for given <see cref="User"/>.
	/// </summary>
	/// <param name="user"><see cref="User"/> for which to calculate score.</param>
	/// <returns>Score of user or null if there is no history.</returns>
	double? CalculateScoreForUser(User user);

	/// <summary>
	/// Returns a list of all score histories for a given user.
	/// </summary>
	/// <param name="userId">Identity of user (<see cref="User"/>).</param>
	/// <returns>List of all score histories for a given user.</returns>
	List<ScoreHistory> GetAllByUserId(int userId);
}

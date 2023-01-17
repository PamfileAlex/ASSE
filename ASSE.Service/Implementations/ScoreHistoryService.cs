// --------------------------------------------------------------------------------------
// <copyright file="ScoreHistoryService.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.Core.Utils;
using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.Service.Interfaces;
using FluentValidation;

namespace ASSE.Service.Implementations;

/// <summary>
/// Implementation of service for <see cref="ScoreHistory"/>.
/// </summary>
public class ScoreHistoryService : EntityService<ScoreHistory, IScoreHistoryDataAccess>, IScoreHistoryService
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ScoreHistoryService"/> class.
	/// </summary>
	/// <param name="dataAccess"><see cref="ScoreHistory"/> data access.</param>
	/// <param name="validator"><see cref="ScoreHistory"/> validator.</param>
	public ScoreHistoryService(IScoreHistoryDataAccess dataAccess, IValidator<ScoreHistory> validator)
		: base(dataAccess, validator)
	{
	}

	/// <inheritdoc/>
	public double? CalculateScoreForUser(User user)
	{
		var scores = GetAllByUserId(user.Id);
		if (scores is null || scores.Count == 0)
		{
			return null;
		}

		return MoreMath.Median(scores.Select(x => x.Score).ToList());
	}

	/// <inheritdoc/>
	public List<ScoreHistory> GetAllByUserId(int userId)
	{
		return DataAccess.GetAllByUserId(userId);
	}
}

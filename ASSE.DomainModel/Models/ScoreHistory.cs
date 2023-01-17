// --------------------------------------------------------------------------------------
// <copyright file="ScoreHistory.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.Core.Models;
using Dapper.Contrib.Extensions;

namespace ASSE.DomainModel.Models;

/// <summary>
/// Represents a history entry for the score.
/// </summary>
[Table("ScoreHistories")]
public class ScoreHistory : IKeyEntity
{
	/// <inheritdoc/>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the user id of the auction.
	/// </summary>
	public int UserId { get; set; }

	/// <summary>
	/// Gets or sets the score.
	/// </summary>
	public double Score { get; set; }

	/// <summary>
	/// Gets or sets the date time.
	/// </summary>
	public DateTime DateTime { get; set; }
}

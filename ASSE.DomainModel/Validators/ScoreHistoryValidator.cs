// --------------------------------------------------------------------------------------
// <copyright file="ScoreHistoryValidator.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.DomainModel.Models;
using FluentValidation;

namespace ASSE.DomainModel.Validators;

/// <summary>
/// Validator for <see cref="ScoreHistory"/>.
/// </summary>
public class ScoreHistoryValidator : AbstractValidator<ScoreHistory>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ScoreHistoryValidator"/> class.
	/// </summary>
	public ScoreHistoryValidator()
	{
		RuleFor(x => x.Score).NotEmpty().GreaterThan(0.0);
		RuleFor(x => x.UserId).NotEmpty();
		RuleFor(x => x.DateTime).NotEmpty();
	}
}

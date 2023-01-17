// --------------------------------------------------------------------------------------
// <copyright file="ScoreHistoryValidatorTests.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.DomainModel.Models;
using ASSE.DomainModel.Validators;
using FluentValidation.TestHelper;

namespace ASSE.DomainModel.Tests;

/// <summary>
/// Tests for <see cref="ScoreHistoryValidator"/>.
/// </summary>
public class ScoreHistoryValidatorTests
{
	private readonly ScoreHistoryValidator _validator;

	/// <summary>
	/// Initializes a new instance of the <see cref="ScoreHistoryValidatorTests"/> class.
	/// </summary>
	public ScoreHistoryValidatorTests()
	{
		_validator = new ScoreHistoryValidator();
	}

	/// <summary>
	/// Test for valid <see cref="ScoreHistory"/>.
	/// </summary>
	[Fact]
	public void Validate_ValidScoreHistory_NoErrors()
	{
		var data = new ScoreHistory()
		{
			Id = 1,
			UserId = 1,
			Score = 100,
			DateTime = DateTime.Now,
		};

		var result = _validator.TestValidate(data);

		result.ShouldNotHaveAnyValidationErrors();
	}

	/// <summary>
	/// Test that default <see cref="ScoreHistory"/> fails.
	/// </summary>
	[Fact]
	public void Validate_DefaultUser_Fails()
	{
		var data = new ScoreHistory();

		var result = _validator.TestValidate(data);

		result.ShouldHaveAnyValidationError();
	}

	/// <summary>
	/// Test that invalid <see cref="ScoreHistory.UserId"/> fails.
	/// </summary>
	[Fact]
	public void Validate_EmptyUserId_Fails()
	{
		var data = new ScoreHistory()
		{
			Id = 1,
			Score = 100,
			DateTime = DateTime.Now,
		};

		var result = _validator.TestValidate(data);
		result.ShouldHaveValidationErrorFor(data => data.UserId);
	}

	/// <summary>
	/// Test that invalid <see cref="ScoreHistory.Score"/> fails.
	/// </summary>
	/// <param name="score">Parametrized score value.</param>
	[Theory]
	[InlineData(0.0)]
	[InlineData(-1.0)]
	public void Validate_InvalidScore_Fails(double score)
	{
		var data = new ScoreHistory()
		{
			Id = 1,
			UserId = 1,
			Score = score,
			DateTime = DateTime.Now,
		};

		var result = _validator.TestValidate(data);

		result.ShouldHaveValidationErrorFor(data => data.Score);
	}

	/// <summary>
	/// Test that invalid <see cref="ScoreHistory.DateTime"/> fails.
	/// </summary>
	[Fact]
	public void Validate_EmptyDateTime_Fails()
	{
		var data = new ScoreHistory()
		{
			Id = 1,
			UserId = 1,
			Score = 100,
		};

		var result = _validator.TestValidate(data);
		result.ShouldHaveValidationErrorFor(data => data.DateTime);
	}
}

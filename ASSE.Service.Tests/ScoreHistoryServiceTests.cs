// --------------------------------------------------------------------------------------
// <copyright file="ScoreHistoryServiceTests.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.Core.Test;
using ASSE.Core.Test.xUnit;
using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.DomainModel.Validators;
using ASSE.Service.Implementations;
using FluentAssertions;
using FluentValidation;
using Moq;

namespace ASSE.Service.Tests;

/// <summary>
/// Tests for <see cref="ScoreHistory"/>.
/// </summary>
public class ScoreHistoryServiceTests
{
	private readonly IValidator<ScoreHistory> _validator;
	private readonly Mock<IValidator<ScoreHistory>> _mockValidator;
	private readonly Mock<IScoreHistoryDataAccess> _mockDataAccess;

	/// <summary>
	/// Initializes a new instance of the <see cref="ScoreHistoryServiceTests"/> class.
	/// </summary>
	public ScoreHistoryServiceTests()
	{
		_validator = new ScoreHistoryValidator();
		_mockDataAccess = new Mock<IScoreHistoryDataAccess>();
		_mockValidator = new Mock<IValidator<ScoreHistory>>();
	}

	/// <summary>
	/// Gets a new valid <see cref="ScoreHistory"/>.
	/// </summary>
	/// <returns>Returns a valid user.</returns>
	public static ScoreHistory GetValidScoreHistory()
	{
		return new ScoreHistory()
		{
			Id = 1,
			UserId = 1,
			Score = 100,
			DateTime = DateTime.Now,
		};
	}

	/// <summary>
	/// Test that validator is called on Add method on passing validator.
	/// </summary>
	[Fact]
	public void Add_ValidatorIsCalled_Passes()
	{
		var data = new ScoreHistory();

		_mockValidator.Setup(x => x.Validate(data))
			.Returns(ValidationUtils.PassingValidationResult);
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);

		var service = new ScoreHistoryService(_mockDataAccess.Object, _mockValidator.Object);

		var result = service.Add(data);

		result.Should().Be(1);
		_mockValidator.Verify(x => x.Validate(data));
		_mockDataAccess.Verify(x => x.Add(data));
	}

	/// <summary>
	/// Test that validator is called on Add method on failing validator.
	/// </summary>
	[Fact]
	public void Add_ValidatorIsCalled_Fails()
	{
		var data = new ScoreHistory();

		_mockValidator.Setup(x => x.Validate(data))
			.Returns(ValidationUtils.FailingValidationResult);
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);

		var service = new ScoreHistoryService(_mockDataAccess.Object, _mockValidator.Object);

		var result = service.Add(data);

		result.Should().Be(default);
		_mockValidator.Verify(x => x.Validate(data));
		_mockDataAccess.Verify(x => x.Add(data), Times.Never());
	}

	/// <summary>
	/// Test that Add method returns expected id.
	/// </summary>
	/// <param name="id">Parametrized id value.</param>
	[Theory]
	[InlineData(1)]
	[InlineData(10)]
	[InlineData(100)]
	public void Add_ValidScoreHistory_ValidationPasses_ReturnsValidId(int id)
	{
		var data = GetValidScoreHistory();
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(id);

		var service = new ScoreHistoryService(_mockDataAccess.Object, _validator);

		var result = service.Add(data);

		result.Should().Be(id);
		_mockDataAccess.Verify(x => x.Add(data));
	}

	/// <summary>
	/// Test that Add method returns default when validator fails for invalid <see cref="ScoreHistory"/>.
	/// </summary>
	[Fact]
	public void Add_InvalidScoreHistory_ValidationFails_ReturnsDefault()
	{
		var data = new ScoreHistory();
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);

		var service = new ScoreHistoryService(_mockDataAccess.Object, _validator);

		var result = service.Add(data);

		result.Should().Be(default);
		_mockDataAccess.Verify(x => x.Add(data), Times.Never());
	}

	private static IEnumerable<object?[]> GetScoreHistoryById()
	{
		yield return new object?[] { 1, new ScoreHistory() { Id = 1, UserId = 1, Score = 100, DateTime = DateTime.Now } };
		yield return new object?[] { 10, new ScoreHistory() { Id = 10, UserId = 1, Score = 100, DateTime = DateTime.Now } };
		yield return new object?[] { 20, null };
	}

	/// <summary>
	/// Test GetById method.
	/// </summary>
	/// <param name="id">Parametrized id value.</param>
	/// <param name="data">Parametrized data value.</param>
	[ComplexTheory]
	[MemberData(nameof(GetScoreHistoryById))]
	public void GetById_ProvidedId_ReturnsExpected(int id, ScoreHistory? data)
	{
		_mockDataAccess.Setup(x => x.Get(id))
			.Returns(data);

		var service = new ScoreHistoryService(_mockDataAccess.Object, _validator);

		var result = service.Get(id);

		result.Should().Be(data);
		_mockDataAccess.Verify(x => x.Get(id));
	}

	private static IEnumerable<object[]> UpdateScoreHistory()
	{
		yield return new object[] { true, Times.Once(), GetValidScoreHistory() };
		yield return new object[] { false, Times.Never(), new ScoreHistory() { Id = 1 } };
		yield return new object[] { false, Times.Never(), new ScoreHistory() };
	}

	/// <summary>
	/// Test for Update method.
	/// </summary>
	/// <param name="expected">Expected result.</param>
	/// <param name="times">Times the data access Update method gets called.</param>
	/// <param name="data">Parametrized data value.</param>
	[ComplexTheory]
	[MemberData(nameof(UpdateScoreHistory))]
	public void Update_ProvidedRole_ReturnsProvided(bool expected, Times times, ScoreHistory data)
	{
		_mockDataAccess.Setup(x => x.Update(data))
			.Returns(expected);

		var service = new ScoreHistoryService(_mockDataAccess.Object, _validator);

		var result = service.Update(data);

		result.Should().Be(expected);
		_mockDataAccess.Verify(x => x.Update(data), times);
	}

	/// <summary>
	/// Test Delete method.
	/// </summary>
	/// <param name="expected">Expected result.</param>
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	public void Delete_AnyId_ReturnsExpected(bool expected)
	{
		_mockDataAccess.Setup(x => x.Delete(It.IsAny<int>()))
			.Returns(expected);

		var service = new ScoreHistoryService(_mockDataAccess.Object, _validator);

		var result = service.Delete(1);

		result.Should().Be(expected);
		_mockDataAccess.Verify(x => x.Delete(It.IsAny<int>()));
	}

	private static IEnumerable<object[]> GetAll()
	{
		yield return new object[] { new List<ScoreHistory>() };
		yield return new object[]
		{
			new List<ScoreHistory>()
			{
				GetValidScoreHistory(),
				GetValidScoreHistory(),
			},
		};
	}

	/// <summary>
	/// Test GetAll method.
	/// </summary>
	/// <param name="data">Parametrized list of auctions.</param>
	[ComplexTheory]
	[MemberData(nameof(GetAll))]
	public void GetAll_ProvidedInput_ReturnsProvided(List<ScoreHistory> data)
	{
		_mockDataAccess.Setup(x => x.GetAll())
			.Returns(data);

		var service = new ScoreHistoryService(_mockDataAccess.Object, _validator);

		var result = service.GetAll();

		result.Should().BeEquivalentTo(data);
		_mockDataAccess.Verify(x => x.GetAll());
	}

	/// <summary>
	/// Test GetAllByUserId method.
	/// </summary>
	/// <param name="data">Parametrized list of auctions.</param>
	[ComplexTheory]
	[MemberData(nameof(GetAll))]
	public void GetAllByUserId_AnyUserId_ReturnsExpected(List<ScoreHistory> data)
	{
		var userId = 1;
		_mockDataAccess.Setup(x => x.GetAllByUserId(userId))
			.Returns(data);

		var service = new ScoreHistoryService(_mockDataAccess.Object, _validator);

		var result = service.GetAllByUserId(userId);

		result.Should().BeEquivalentTo(data);
		_mockDataAccess.Verify(x => x.GetAllByUserId(userId));
	}

	private static IEnumerable<object?[]> CalculateScoreForUserData()
	{
		yield return new object?[] { null, new List<ScoreHistory>() };
		yield return new object?[] { 100, new List<ScoreHistory>() { new ScoreHistory() { Score = 100 } } };
		yield return new object?[] { 150, new List<ScoreHistory>() { new ScoreHistory() { Score = 100 }, new ScoreHistory() { Score = 200 } } };
	}

	/// <summary>
	/// Test CalculateScoreForUser method.
	/// </summary>
	/// <param name="expected">Parametrized expected score.</param>
	/// <param name="data">Parametrized list of auctions.</param>
	[ComplexTheory]
	[MemberData(nameof(CalculateScoreForUserData))]
	public void CalculateScoreForUser_ReturnsExpected(double? expected, List<ScoreHistory> data)
	{
		var user = new User() { Id = 1 };
		_mockDataAccess.Setup(x => x.GetAllByUserId(user.Id))
			.Returns(data);

		var service = new ScoreHistoryService(_mockDataAccess.Object, _validator);

		var result = service.CalculateScoreForUser(user);

		result.Should().Be(expected);
		_mockDataAccess.Verify(x => x.GetAllByUserId(user.Id));
	}
}

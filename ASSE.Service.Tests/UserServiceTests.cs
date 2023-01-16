// --------------------------------------------------------------------------------------
// <copyright file="UserServiceTests.cs" company="Transilvania University of Brasov">
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
/// Tests for <see cref="UserService"/>.
/// </summary>
public class UserServiceTests
{
	private readonly IValidator<User> _validator;
	private readonly Mock<IValidator<User>> _mockValidator;
	private readonly Mock<IUserDataAccess> _mockDataAccess;

	/// <summary>
	/// Initializes a new instance of the <see cref="UserServiceTests"/> class.
	/// </summary>
	public UserServiceTests()
	{
		_validator = new UserValidator();
		_mockDataAccess = new Mock<IUserDataAccess>();
		_mockValidator = new Mock<IValidator<User>>();
	}

	/// <summary>
	/// Gets a new valid <see cref="User"/>.
	/// </summary>
	/// <returns>Returns a valid user.</returns>
	public static User GetValidUser()
	{
		return new User()
		{
			Id = 1,
			FirstName = "Max",
			LastName = "Verstappen",
			Score = 650,
		};
	}

	/// <summary>
	/// Test that validator is called on Add method on passing validator.
	/// </summary>
	[Fact]
	public void Add_ValidatorIsCalled_Passes()
	{
		var data = new User();

		_mockValidator.Setup(x => x.Validate(data))
			.Returns(ValidationUtils.PassingValidationResult);
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);

		var service = new UserService(_mockDataAccess.Object, _mockValidator.Object);

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
		var data = new User();

		_mockValidator.Setup(x => x.Validate(data))
			.Returns(ValidationUtils.FailingValidationResult);
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);

		var service = new UserService(_mockDataAccess.Object, _mockValidator.Object);

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
	public void Add_ValidRole_ValidationPasses_ReturnsValidId(int id)
	{
		var data = GetValidUser();
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(id);

		var service = new UserService(_mockDataAccess.Object, _validator);

		var result = service.Add(data);

		result.Should().Be(id);
		_mockDataAccess.Verify(x => x.Add(data));
	}

	/// <summary>
	/// Test that Add method returns default when validator fails for invalid <see cref="User"/>.
	/// </summary>
	[Fact]
	public void Add_InvalidRole_ValidationFails_ReturnsDefault()
	{
		var data = new User();
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);

		var service = new UserService(_mockDataAccess.Object, _validator);

		var result = service.Add(data);

		result.Should().Be(default);
		_mockDataAccess.Verify(x => x.Add(data), Times.Never());
	}

	private static IEnumerable<object?[]> GetUserById()
	{
		yield return new object?[] { 1, new User() { Id = 1, FirstName = "Max", LastName = "Verstappen", Score = 650 } };
		yield return new object?[] { 10, new User() { Id = 1, FirstName = "Charles", LastName = "Leclerc", Score = 550 } };
		yield return new object?[] { 20, null };
	}

	/// <summary>
	/// Test GetById method.
	/// </summary>
	/// <param name="id">Parametrized id value.</param>
	/// <param name="data">Parametrized data value.</param>
	[ComplexTheory]
	[MemberData(nameof(GetUserById))]
	public void GetById_ProvidedId_ReturnsExpected(int id, User? data)
	{
		_mockDataAccess.Setup(x => x.Get(id))
			.Returns(data);

		var service = new UserService(_mockDataAccess.Object, _validator);

		var result = service.Get(id);

		result.Should().Be(data);
		_mockDataAccess.Verify(x => x.Get(id));
	}

	private static IEnumerable<object[]> UpdateUsers()
	{
		yield return new object[] { true, Times.Once(), GetValidUser() };
		yield return new object[] { false, Times.Never(), new User() { Id = 1 } };
		yield return new object[] { false, Times.Never(), new User() };
	}

	/// <summary>
	/// Test for Update method.
	/// </summary>
	/// <param name="expected">Expected result.</param>
	/// <param name="times">Times the data access Update method gets called.</param>
	/// <param name="data">Parametrized data value.</param>
	[ComplexTheory]
	[MemberData(nameof(UpdateUsers))]
	public void Update_ProvidedRole_ReturnsProvided(bool expected, Times times, User data)
	{
		_mockDataAccess.Setup(x => x.Update(data))
			.Returns(expected);

		var service = new UserService(_mockDataAccess.Object, _validator);

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

		var service = new UserService(_mockDataAccess.Object, _validator);

		var result = service.Delete(1);

		result.Should().Be(expected);
		_mockDataAccess.Verify(x => x.Delete(It.IsAny<int>()));
	}

	private static IEnumerable<object[]> GetAll()
	{
		yield return new object[] { new List<User>() };
		yield return new object[]
		{
			new List<User>()
			{
				new User() { Id = 1, FirstName = "Max", LastName = "Verstappen", Score = 650 },
				new User() { Id = 1, FirstName = "Charles", LastName = "Leclerc", Score = 550 },
			},
		};
	}

	/// <summary>
	/// Test GetAll method.
	/// </summary>
	/// <param name="data">Parametrized list of auctions.</param>
	[ComplexTheory]
	[MemberData(nameof(GetAll))]
	public void GetAll_ProvidedInput_ReturnsProvided(List<User> data)
	{
		_mockDataAccess.Setup(x => x.GetAll())
			.Returns(data);

		var service = new UserService(_mockDataAccess.Object, _validator);

		var result = service.GetAll();

		result.Should().BeEquivalentTo(data);
		_mockDataAccess.Verify(x => x.GetAll());
	}
}

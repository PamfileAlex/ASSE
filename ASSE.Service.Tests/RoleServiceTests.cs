// --------------------------------------------------------------------------------------
// <copyright file="RoleServiceTests.cs" company="Transilvania University of Brasov">
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
/// Tests for <see cref="RoleService"/>.
/// </summary>
public class RoleServiceTests
{
	private readonly IValidator<Role> _validator;
	private readonly Mock<IValidator<Role>> _mockValidator;
	private readonly Mock<IRoleDataAccess> _mockDataAccess;

	/// <summary>
	/// Initializes a new instance of the <see cref="RoleServiceTests"/> class.
	/// </summary>
	public RoleServiceTests()
	{
		_validator = new RoleValidator();
		_mockDataAccess = new Mock<IRoleDataAccess>();
		_mockValidator = new Mock<IValidator<Role>>();
	}

	/// <summary>
	/// Gets a new valid <see cref="Role"/>.
	/// </summary>
	/// <returns>Returns a valid role.</returns>
	public static Role GetValidRole()
	{
		return new Role()
		{
			Id = 1,
			Name = "Admin",
		};
	}

	/// <summary>
	/// Test that validator is called on Add method on passing validator.
	/// </summary>
	[Fact]
	public void Add_ValidatorIsCalled_Passes()
	{
		var data = new Role();

		_mockValidator.Setup(x => x.Validate(data))
			.Returns(ValidationUtils.PassingValidationResult);
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);

		var service = new RoleService(_mockDataAccess.Object, _mockValidator.Object);

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
		var data = new Role();

		_mockValidator.Setup(x => x.Validate(data))
			.Returns(ValidationUtils.FailingValidationResult);
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);

		var service = new RoleService(_mockDataAccess.Object, _mockValidator.Object);

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
		var data = GetValidRole();
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(id);

		var service = new RoleService(_mockDataAccess.Object, _validator);

		var result = service.Add(data);

		result.Should().Be(id);
		_mockDataAccess.Verify(x => x.Add(data));
	}

	/// <summary>
	/// Test that Add method returns default when validator fails for invalid <see cref="Role"/>.
	/// </summary>
	[Fact]
	public void Add_InvalidRole_ValidationFails_ReturnsDefault()
	{
		var data = new Role();
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);

		var service = new RoleService(_mockDataAccess.Object, _validator);

		var result = service.Add(data);

		result.Should().Be(default);
		_mockDataAccess.Verify(x => x.Add(data), Times.Never());
	}

	private static IEnumerable<object?[]> GetRoleById()
	{
		yield return new object?[] { 1, new Role() { Id = 1, Name = "Admin" } };
		yield return new object?[] { 10, new Role() { Id = 10, Name = "Admin" } };
		yield return new object?[] { 20, null };
	}

	/// <summary>
	/// Test GetById method.
	/// </summary>
	/// <param name="id">Parametrized id value.</param>
	/// <param name="data">Parametrized data value.</param>
	[ComplexTheory]
	[MemberData(nameof(GetRoleById))]
	public void GetById_ProvidedId_ReturnsExpected(int id, Role? data)
	{
		_mockDataAccess.Setup(x => x.Get(id))
			.Returns(data);

		var service = new RoleService(_mockDataAccess.Object, _validator);

		var result = service.Get(id);

		result.Should().Be(data);
		_mockDataAccess.Verify(x => x.Get(id));
	}

	private static IEnumerable<object[]> UpdateRoles()
	{
		yield return new object[] { true, Times.Once(), GetValidRole() };
		yield return new object[] { false, Times.Never(), new Role() { Id = 1 } };
		yield return new object[] { false, Times.Never(), new Role() };
	}

	/// <summary>
	/// Test for Update method.
	/// </summary>
	/// <param name="expected">Expected result.</param>
	/// <param name="times">Times the data access Update method gets called.</param>
	/// <param name="data">Parametrized data value.</param>
	[ComplexTheory]
	[MemberData(nameof(UpdateRoles))]
	public void Update_ProvidedRole_ReturnsProvided(bool expected, Times times, Role data)
	{
		_mockDataAccess.Setup(x => x.Update(data))
			.Returns(expected);

		var service = new RoleService(_mockDataAccess.Object, _validator);

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

		var service = new RoleService(_mockDataAccess.Object, _validator);

		var result = service.Delete(1);

		result.Should().Be(expected);
		_mockDataAccess.Verify(x => x.Delete(It.IsAny<int>()));
	}

	private static IEnumerable<object[]> GetAll()
	{
		yield return new object[] { new List<Role>() };
		yield return new object[]
		{
			new List<Role>()
			{
				new Role() { Id = 1, Name = "Admin" },
				new Role() { Id = 10, Name = "Admin" },
			},
		};
	}

	/// <summary>
	/// Test GetAll method.
	/// </summary>
	/// <param name="data">Parametrized list of auctions.</param>
	[ComplexTheory]
	[MemberData(nameof(GetAll))]
	public void GetAll_ProvidedInput_ReturnsProvided(List<Role> data)
	{
		_mockDataAccess.Setup(x => x.GetAll())
			.Returns(data);

		var service = new RoleService(_mockDataAccess.Object, _validator);

		var result = service.GetAll();

		result.Should().BeEquivalentTo(data);
		_mockDataAccess.Verify(x => x.GetAll());
	}
}

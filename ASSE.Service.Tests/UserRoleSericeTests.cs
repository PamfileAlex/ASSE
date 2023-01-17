// --------------------------------------------------------------------------------------
// <copyright file="UserRoleSericeTests.cs" company="Transilvania University of Brasov">
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
/// Tests for <see cref="UserRoleService"/>.
/// </summary>
public class UserRoleSericeTests
{
	private readonly IValidator<UserRole> _validator;
	private readonly Mock<IValidator<UserRole>> _mockValidator;
	private readonly Mock<IUserRoleDataAccess> _mockDataAccess;

	/// <summary>
	/// Initializes a new instance of the <see cref="UserRoleSericeTests"/> class.
	/// </summary>
	public UserRoleSericeTests()
	{
		_validator = new UserRoleValidator();
		_mockValidator = new Mock<IValidator<UserRole>>();
		_mockDataAccess = new Mock<IUserRoleDataAccess>();
	}

	/// <summary>
	/// Gets a new valid <see cref="UserRole"/>.
	/// </summary>
	/// <returns>Returns a valid user-role.</returns>
	public static UserRole GetValidUserRole()
	{
		return new UserRole(1, 1);
	}

	/// <summary>
	/// Test that validator is called on Add method on passing validator.
	/// </summary>
	[Fact]
	public void Add_ValidatorIsCalled_Passes()
	{
		var data = default(UserRole);

		_mockValidator.Setup(x => x.Validate(data))
			.Returns(ValidationUtils.PassingValidationResult);
		_mockDataAccess.Setup(x => x.Add(data.UserId, data.RoleId));

		var service = new UserRoleService(_mockDataAccess.Object, _mockValidator.Object);

		// service.Add(data);
		service.Invoking(x => x.Add(data)).Should().NotThrow();

		_mockValidator.Verify(x => x.Validate(data));
		_mockDataAccess.Verify(x => x.Add(data.UserId, data.RoleId));
	}

	/// <summary>
	/// Test that validator is called on Add method on failing validator.
	/// </summary>
	[Fact]
	public void Add_ValidatorIsCalled_Fails()
	{
		var data = default(UserRole);

		_mockValidator.Setup(x => x.Validate(data))
			.Returns(ValidationUtils.FailingValidationResult);
		_mockDataAccess.Setup(x => x.Add(data.UserId, data.RoleId));

		var service = new UserRoleService(_mockDataAccess.Object, _mockValidator.Object);

		// Action act = () => service.Add(data);
		// act.Should().Throw<ApplicationException>();
		service.Invoking(x => x.Add(data)).Should().Throw<ApplicationException>();

		_mockValidator.Verify(x => x.Validate(data));
		_mockDataAccess.Verify(x => x.Add(data.UserId, data.RoleId), Times.Never());
	}

	/// <summary>
	/// Test Add method with valid <see cref="UserRole"/> entity.
	/// </summary>
	[Fact]
	public void Add_ValidUserRole()
	{
		var data = GetValidUserRole();

		_mockDataAccess.Setup(x => x.Add(data.UserId, data.RoleId));

		var service = new UserRoleService(_mockDataAccess.Object, _validator);

		service.Invoking(x => x.Add(data)).Should().NotThrow();

		_mockDataAccess.Verify(x => x.Add(data.UserId, data.RoleId));
	}

	/// <summary>
	/// Test Add method with valid identities.
	/// </summary>
	[Fact]
	public void Add_ValidUserRoleIds()
	{
		var data = GetValidUserRole();

		_mockDataAccess.Setup(x => x.Add(data.UserId, data.RoleId));

		var service = new UserRoleService(_mockDataAccess.Object, _validator);

		service.Invoking(x => x.Add(data.UserId, data.RoleId)).Should().NotThrow();

		_mockDataAccess.Verify(x => x.Add(data.UserId, data.RoleId));
	}

	/// <summary>
	/// Test Add method with invalid <see cref="UserRole"/>.
	/// </summary>
	[Fact]
	public void Add_InvalidUserRole()
	{
		var data = default(UserRole);

		_mockDataAccess.Setup(x => x.Add(data.UserId, data.RoleId));

		var service = new UserRoleService(_mockDataAccess.Object, _validator);

		service.Invoking(x => x.Add(data)).Should().Throw<ApplicationException>();

		_mockDataAccess.Verify(x => x.Add(data.UserId, data.RoleId), Times.Never());
	}

	/// <summary>
	/// Test Add method with invalid identities.
	/// </summary>
	[Fact]
	public void Add_InvalidUserRole_UsingIds()
	{
		var data = default(UserRole);

		_mockDataAccess.Setup(x => x.Add(data.UserId, data.RoleId));

		var service = new UserRoleService(_mockDataAccess.Object, _validator);

		service.Invoking(x => x.Add(data.UserId, data.RoleId)).Should().Throw<ApplicationException>();

		_mockDataAccess.Verify(x => x.Add(data.UserId, data.RoleId), Times.Never());
	}

	private static IEnumerable<object[]> GetUserRoles()
	{
		yield return new object[] { true, GetValidUserRole() };
		yield return new object[] { false, default(UserRole) };
	}

	/// <summary>
	/// Test Delete method with <see cref="UserRole"/> entity.
	/// </summary>
	/// <param name="expected">Expected result.</param>
	/// <param name="data">Parametrized data value.</param>
	[ComplexTheory]
	[MemberData(nameof(GetUserRoles))]
	public void Delete_UserRole(bool expected, UserRole data)
	{
		_mockDataAccess.Setup(x => x.Delete(data.UserId, data.RoleId))
			.Returns(expected);

		var service = new UserRoleService(_mockDataAccess.Object, _validator);

		var result = service.Delete(data);

		result.Should().Be(expected);
		_mockDataAccess.Verify(x => x.Delete(data.UserId, data.RoleId));
	}

	/// <summary>
	/// Test Delete method with identities.
	/// </summary>
	/// <param name="expected">Expected result.</param>
	/// <param name="data">Parametrized data value.</param>
	[ComplexTheory]
	[MemberData(nameof(GetUserRoles))]
	public void Delete_UserRole_UsingIds(bool expected, UserRole data)
	{
		_mockDataAccess.Setup(x => x.Delete(data.UserId, data.RoleId))
			.Returns(expected);

		var service = new UserRoleService(_mockDataAccess.Object, _validator);

		var result = service.Delete(data.UserId, data.RoleId);

		result.Should().Be(expected);
		_mockDataAccess.Verify(x => x.Delete(data.UserId, data.RoleId));
	}

	private static IEnumerable<object[]> GetAll()
	{
		yield return new object[] { new List<UserRole>() };
		yield return new object[]
		{
			new List<UserRole>()
			{
				new UserRole(1, 1),
				new UserRole(1, 2),
			},
		};
	}

	/// <summary>
	/// Test GetAll method.
	/// </summary>
	/// <param name="data">Parametrized list of auctions.</param>
	[ComplexTheory]
	[MemberData(nameof(GetAll))]
	public void GetAll_ProvidedInput_ReturnsProvided(List<UserRole> data)
	{
		_mockDataAccess.Setup(x => x.GetAll())
			.Returns(data);

		var service = new UserRoleService(_mockDataAccess.Object, _validator);

		var result = service.GetAll();

		result.Should().BeEquivalentTo(data);
		_mockDataAccess.Verify(x => x.GetAll());
	}
}

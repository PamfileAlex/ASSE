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
public class UserRoleSericeTests
{
	private readonly IValidator<UserRole> _validator;
	private readonly Mock<IValidator<UserRole>> _mockValidator;
	private readonly Mock<IUserRoleDataAccess> _mockDataAccess;

	public UserRoleSericeTests()
	{
		_validator = new UserRoleValidator();
		_mockValidator = new Mock<IValidator<UserRole>>();
		_mockDataAccess = new Mock<IUserRoleDataAccess>();
	}

	public static UserRole GetValidUserRole()
	{
		return new UserRole(1, 1);
	}

	[Fact]
	public void Add_ValidatorIsCalled_Passes()
	{
		var data = new UserRole();

		_mockValidator.Setup(x => x.Validate(data))
			.Returns(ValidationUtils.PassingValidationResult);
		_mockDataAccess.Setup(x => x.Add(data.UserId, data.RoleId));

		var service = new UserRoleService(_mockDataAccess.Object, _mockValidator.Object);

		//service.Add(data);
		service.Invoking(x => x.Add(data)).Should().NotThrow();

		_mockValidator.Verify(x => x.Validate(data));
		_mockDataAccess.Verify(x => x.Add(data.UserId, data.RoleId));
	}

	[Fact]
	public void Add_ValidatorIsCalled_Fails()
	{
		var data = new UserRole();

		_mockValidator.Setup(x => x.Validate(data))
			.Returns(ValidationUtils.FailingValidationResult);
		_mockDataAccess.Setup(x => x.Add(data.UserId, data.RoleId));

		var service = new UserRoleService(_mockDataAccess.Object, _mockValidator.Object);

		//Action act = () => service.Add(data);
		//act.Should().Throw<ApplicationException>();
		service.Invoking(x => x.Add(data)).Should().Throw<ApplicationException>();

		_mockValidator.Verify(x => x.Validate(data));
		_mockDataAccess.Verify(x => x.Add(data.UserId, data.RoleId), Times.Never());
	}

	[Fact]
	public void Add_ValidUserRole()
	{
		var data = GetValidUserRole();

		_mockDataAccess.Setup(x => x.Add(data.UserId, data.RoleId));

		var service = new UserRoleService(_mockDataAccess.Object, _validator);

		service.Invoking(x => x.Add(data)).Should().NotThrow();

		_mockDataAccess.Verify(x => x.Add(data.UserId, data.RoleId));
	}

	[Fact]
	public void Add_ValidUserRoleIds()
	{
		var data = GetValidUserRole();

		_mockDataAccess.Setup(x => x.Add(data.UserId, data.RoleId));

		var service = new UserRoleService(_mockDataAccess.Object, _validator);

		service.Invoking(x => x.Add(data.UserId, data.RoleId)).Should().NotThrow();

		_mockDataAccess.Verify(x => x.Add(data.UserId, data.RoleId));
	}

	[Fact]
	public void Add_InvalidUserRole()
	{
		var data = new UserRole();

		_mockDataAccess.Setup(x => x.Add(data.UserId, data.RoleId));

		var service = new UserRoleService(_mockDataAccess.Object, _validator);

		service.Invoking(x => x.Add(data)).Should().Throw<ApplicationException>();

		_mockDataAccess.Verify(x => x.Add(data.UserId, data.RoleId), Times.Never());
	}

	[Fact]
	public void Add_InvalidUserRole_UsingIds()
	{
		var data = new UserRole();

		_mockDataAccess.Setup(x => x.Add(data.UserId, data.RoleId));

		var service = new UserRoleService(_mockDataAccess.Object, _validator);

		service.Invoking(x => x.Add(data.UserId, data.RoleId)).Should().Throw<ApplicationException>();

		_mockDataAccess.Verify(x => x.Add(data.UserId, data.RoleId), Times.Never());
	}

	private static IEnumerable<object[]> GetUserRoles()
	{
		yield return new object[] { true, GetValidUserRole() };
		yield return new object[] { false, new UserRole() };
	}

	[ComplexTheory]
	[MemberData(nameof(GetUserRoles))]
	public void Delete_UserRole(bool status, UserRole data)
	{
		_mockDataAccess.Setup(x => x.Delete(data.UserId, data.RoleId))
			.Returns(status);

		var service = new UserRoleService(_mockDataAccess.Object, _validator);

		var result = service.Delete(data);

		result.Should().Be(status);
		_mockDataAccess.Verify(x => x.Delete(data.UserId, data.RoleId));
	}

	[ComplexTheory]
	[MemberData(nameof(GetUserRoles))]
	public void Delete_UserRole_UsingIds(bool status, UserRole data)
	{
		_mockDataAccess.Setup(x => x.Delete(data.UserId, data.RoleId))
			.Returns(status);

		var service = new UserRoleService(_mockDataAccess.Object, _validator);

		var result = service.Delete(data.UserId, data.RoleId);

		result.Should().Be(status);
		_mockDataAccess.Verify(x => x.Delete(data.UserId, data.RoleId));
	}

	private static IEnumerable<object[]> GetAll()
	{
		yield return new object[] { new List<UserRole>() };
		yield return new object[] { new List<UserRole>() {
				new UserRole(1,1),
				new UserRole(1,2),
			}
		};
	}

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

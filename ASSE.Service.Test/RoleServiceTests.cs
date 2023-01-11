using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.DomainModel.Validators;
using ASSE.Service.Implementations;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace ASSE.Service.Test;
public class RoleServiceTests
{
	private readonly IValidator<Role> _validator;
	private readonly Mock<IValidator<Role>> _mockValidator;
	private readonly Mock<IRoleDataAccess> _mockDataAccess;

	public RoleServiceTests()
	{
		_validator = new RoleValidator();
		_mockDataAccess = new Mock<IRoleDataAccess>();
		_mockValidator = new Mock<IValidator<Role>>();
	}

	public static Role GetValidRole()
	{
		return new Role()
		{
			Id = 1,
			Name = "Admin",
		};
	}

	[Fact]
	public void Add_ValidatorIsCalled_Passes()
	{
		var data = new Role();

		_mockValidator.Setup(x => x.Validate(data))
			.Returns(TestUtils.PassingValidationResult);
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);

		var service = new RoleService(_mockDataAccess.Object, _mockValidator.Object);

		var result = service.Add(data);

		result.Should().Be(1);
		_mockValidator.Verify(x => x.Validate(data));
		_mockDataAccess.Verify(x => x.Add(data));
	}

	[Fact]
	public void Add_ValidatorIsCalled_Fails()
	{
		var data = new Role();

		_mockValidator.Setup(x => x.Validate(data))
			.Returns(TestUtils.FailingValidationResult);
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);

		var service = new RoleService(_mockDataAccess.Object, _mockValidator.Object);

		var result = service.Add(data);

		result.Should().Be(default);
		_mockValidator.Verify(x => x.Validate(data));
		_mockDataAccess.Verify(x => x.Add(data), Times.Never());
	}

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

	private static IEnumerable<object[]> GetRoleById()
	{
		yield return new object[] { 1, new Role() { Id = 1, Name = "Admin" } };
		yield return new object[] { 10, new Role() { Id = 10, Name = "Admin" } };
		yield return new object[] { 20, null };
	}

	[Theory]
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
		yield return new object[] { false, Times.Never(), new Role() };
	}

	[Theory]
	[MemberData(nameof(UpdateRoles))]
	public void Update_ProvidedRole_ReturnsProvided(bool status, Times times, Role data)
	{
		_mockDataAccess.Setup(x => x.Update(data))
			.Returns(status);

		var service = new RoleService(_mockDataAccess.Object, _validator);

		var result = service.Update(data);

		result.Should().Be(status);
		_mockDataAccess.Verify(x => x.Update(data), times);
	}

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	public void Delete_AnyId_ReturnsExpected(bool status)
	{
		_mockDataAccess.Setup(x => x.Delete(It.IsAny<int>()))
			.Returns(status);

		var service = new RoleService(_mockDataAccess.Object, _validator);

		var result = service.Delete(1);

		result.Should().Be(status);
		_mockDataAccess.Verify(x => x.Delete(It.IsAny<int>()));
	}

	private static IEnumerable<object[]> GetAll()
	{
		yield return new object[] { new List<Role>() };
		yield return new object[] { new List<Role>() {
				new Role() { Id = 1, Name = "Admin" },
				new Role() { Id = 10, Name = "Admin" }
			}
		};
	}

	[Theory]
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

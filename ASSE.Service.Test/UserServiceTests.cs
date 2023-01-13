using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.DomainModel.Validators;
using ASSE.Service.Implementations;
using ASSE.TestCore;
using ASSE.TestCore.xUnit;
using FluentAssertions;
using FluentValidation;
using Moq;

namespace ASSE.Service.Test;
public class UserServiceTests
{
	private readonly IValidator<User> _validator;
	private readonly Mock<IValidator<User>> _mockValidator;
	private readonly Mock<IUserDataAccess> _mockDataAccess;

	public UserServiceTests()
	{
		_validator = new UserValidator();
		_mockDataAccess = new Mock<IUserDataAccess>();
		_mockValidator = new Mock<IValidator<User>>();
	}

	public static User GetValidUser()
	{
		return new User()
		{
			Id = 1,
			FirstName = "Max",
			LastName = "Verstappen",
			Score = 650
		};
	}

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

	private static IEnumerable<object[]> GetUserById()
	{
		yield return new object[] { 1, new User() { Id = 1, FirstName = "Max", LastName = "Verstappen", Score = 650 } };
		yield return new object[] { 10, new User() { Id = 1, FirstName = "Charles", LastName = "Leclerc", Score = 550 } };
		yield return new object[] { 20, null };
	}

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

	[ComplexTheory]
	[MemberData(nameof(UpdateUsers))]
	public void Update_ProvidedRole_ReturnsProvided(bool status, Times times, User data)
	{
		_mockDataAccess.Setup(x => x.Update(data))
			.Returns(status);

		var service = new UserService(_mockDataAccess.Object, _validator);

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

		var service = new UserService(_mockDataAccess.Object, _validator);

		var result = service.Delete(1);

		result.Should().Be(status);
		_mockDataAccess.Verify(x => x.Delete(It.IsAny<int>()));
	}

	private static IEnumerable<object[]> GetAll()
	{
		yield return new object[] { new List<User>() };
		yield return new object[] { new List<User>() {
				new User() { Id = 1, FirstName = "Max", LastName = "Verstappen", Score = 650 },
				new User() { Id = 1, FirstName = "Charles", LastName = "Leclerc", Score = 550 }
			}
		};
	}

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

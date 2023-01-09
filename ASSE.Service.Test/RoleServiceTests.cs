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
	private readonly RoleValidator _validator;
	private readonly Mock<IRoleDataAccess> _mockDataAccess;
	private readonly Mock<IValidator<Role>> _mockValidator;

	public RoleServiceTests()
	{
		_validator = new RoleValidator();
		_mockDataAccess = new Mock<IRoleDataAccess>();
		_mockValidator = new Mock<IValidator<Role>>();
	}

	[Fact]
	public void Add_ValidationPasses_ReturnsValidId()
	{
		var data = It.IsAny<Role>();
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);
		_mockValidator.Setup(x => x.Validate(data))
			.Returns(new ValidationResult());

		var service = new RoleService(_mockDataAccess.Object, _mockValidator.Object);

		var result = service.Add(data);

		result.Should().Be(1);
		_mockDataAccess.Verify(x => x.Add(data));
		_mockValidator.Verify(x => x.Validate(data));
	}

	[Fact]
	public void Add_ValidRole_ReturnsValidId()
	{
		var data = new Role() { Id = 1, Name = "Role" };
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);

		var service = new RoleService(_mockDataAccess.Object, _validator);

		var result = service.Add(data);

		result.Should().Be(1);
		_mockDataAccess.Verify(x => x.Add(data));
	}

	[Fact]
	public void Add_InvalidRole_ReturnsDefault()
	{
		var data = new Role();
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(default(int));

		var service = new RoleService(_mockDataAccess.Object, _validator);

		var result = service.Add(data);

		result.Should().Be(default);
		_mockDataAccess.Verify(x => x.Add(data), Times.Never);
	}

	[Fact]
	public void GetById_AnyInput_ReturnsAny()
	{
		_mockDataAccess.Setup(x => x.Get(It.IsAny<int>()))
			.Returns(null as Role);

		var service = new RoleService(_mockDataAccess.Object, _mockValidator.Object);

		var result = service.Get(default);

		_mockDataAccess.Verify(x => x.Get(It.IsAny<int>()));
	}

	[Fact]
	public void GetAll_ReturnsAny()
	{
		_mockDataAccess.Setup(x => x.GetAll())
			.Returns(new List<Role>());

		var service = new RoleService(_mockDataAccess.Object, _mockValidator.Object);

		var result = service.GetAll();

		result.Should().BeEmpty();
		_mockDataAccess.Verify(x => x.GetAll());
	}
}

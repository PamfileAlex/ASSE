using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.DomainModel.Validators;
using ASSE.Service.Implementations;
using ASSE.Core.Test;
using ASSE.Core.Test.xUnit;
using FluentAssertions;
using FluentValidation;
using Moq;

namespace ASSE.Service.Tests;
public class CategoryServiceTests
{
	private readonly IValidator<Category> _validator;
	private readonly Mock<IValidator<Category>> _mockValidator;
	private readonly Mock<ICategoryDataAccess> _mockDataAccess;

	public CategoryServiceTests()
	{
		_validator = new CategoryValidator();
		_mockValidator = new Mock<IValidator<Category>>();
		_mockDataAccess = new Mock<ICategoryDataAccess>();
	}

	public static Category GetValidCategory()
	{
		return new Category()
		{
			Id = 1,
			Name = "Electronics"
		};
	}

	[Fact]
	public void Add_ValidatorIsCalled_Passes()
	{
		var data = new Category();

		_mockValidator.Setup(x => x.Validate(data))
			.Returns(ValidationUtils.PassingValidationResult);
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);

		var service = new CategoryService(_mockDataAccess.Object, _mockValidator.Object);

		var result = service.Add(data);

		result.Should().Be(1);
		_mockValidator.Verify(x => x.Validate(data));
		_mockDataAccess.Verify(x => x.Add(data));
	}

	[Fact]
	public void Add_ValidatorIsCalled_Fails()
	{
		var data = new Category();

		_mockValidator.Setup(x => x.Validate(data))
			.Returns(ValidationUtils.FailingValidationResult);
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);

		var service = new CategoryService(_mockDataAccess.Object, _mockValidator.Object);

		var result = service.Add(data);

		result.Should().Be(default);
		_mockValidator.Verify(x => x.Validate(data));
		_mockDataAccess.Verify(x => x.Add(data), Times.Never());
	}

	[Theory]
	[InlineData(1)]
	[InlineData(10)]
	[InlineData(100)]
	public void Add_ValidCategory_ValidationPasses_ReturnsValidId(int id)
	{
		var data = GetValidCategory();
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(id);

		var service = new CategoryService(_mockDataAccess.Object, _validator);

		var result = service.Add(data);

		result.Should().Be(id);
		_mockDataAccess.Verify(x => x.Add(data));
	}

	[Fact]
	public void Add_InvalidCategory_ValidationFails_ReturnsDefault()
	{
		var data = new Category();
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);

		var service = new CategoryService(_mockDataAccess.Object, _validator);

		var result = service.Add(data);

		result.Should().Be(default);
		_mockDataAccess.Verify(x => x.Add(data), Times.Never());
	}

	private static IEnumerable<object?[]> GetCategoryById()
	{
		yield return new object?[] { 1, new Category() { Id = 1, Name = "Electronics" } };
		yield return new object?[] { 2, new Category() { Id = 2, Name = "Laptops", ParentId = 1 } };
		yield return new object?[] { 8, null };
	}

	[ComplexTheory]
	[MemberData(nameof(GetCategoryById))]
	public void GetById_ProvidedId_ReturnsExpected(int id, Category? data)
	{
		_mockDataAccess.Setup(x => x.Get(id))
			.Returns(data);

		var service = new CategoryService(_mockDataAccess.Object, _validator);

		var result = service.Get(id);

		result.Should().Be(data);
		_mockDataAccess.Verify(x => x.Get(id));
	}

	private static IEnumerable<object[]> UpdateCategories()
	{
		yield return new object[] { true, Times.Once(), GetValidCategory() };
		yield return new object[] { false, Times.Never(), new Category() { Id = 1 } };
		yield return new object[] { false, Times.Never(), new Category() };
	}

	[ComplexTheory]
	[MemberData(nameof(UpdateCategories))]
	public void Update_ProvidedCategory_ReturnsProvided(bool status, Times times, Category data)
	{
		_mockDataAccess.Setup(x => x.Update(data))
			.Returns(status);

		var service = new CategoryService(_mockDataAccess.Object, _validator);

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

		var service = new CategoryService(_mockDataAccess.Object, _validator);

		var result = service.Delete(1);

		result.Should().Be(status);
		_mockDataAccess.Verify(x => x.Delete(It.IsAny<int>()));
	}

	private static IEnumerable<object[]> GetAll()
	{
		yield return new object[] { new List<Category>() };
		yield return new object[] { new List<Category>() {
				new Category() { Id = 1, Name = "Electronics" },
				new Category() { Id = 2, Name = "Laptops", ParentId = 1 },
			}
		};
	}

	[ComplexTheory]
	[MemberData(nameof(GetAll))]
	public void GetAll_ProvidedInput_ReturnsProvided(List<Category> data)
	{
		_mockDataAccess.Setup(x => x.GetAll())
			.Returns(data);

		var service = new CategoryService(_mockDataAccess.Object, _validator);

		var result = service.GetAll();

		result.Should().BeEquivalentTo(data);
		_mockDataAccess.Verify(x => x.GetAll());
	}
}

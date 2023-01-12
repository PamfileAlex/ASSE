using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.DomainModel.Validators;
using ASSE.Service.Implementations;
using ASSE.TestCore;
using FluentAssertions;
using FluentValidation;
using Moq;

namespace ASSE.Service.Test;
public class ProductServiceTests
{
	private readonly IValidator<Product> _validator;
	private readonly Mock<IValidator<Product>> _mockValidator;
	private readonly Mock<IProductDataAccess> _mockDataAccess;

	public ProductServiceTests()
	{
		_validator = new ProductValidator();
		_mockDataAccess = new Mock<IProductDataAccess>();
		_mockValidator = new Mock<IValidator<Product>>();
	}

	public static Product GetValidProduct()
	{
		return new Product()
		{
			Id = 1,
			CategoryId = 1,
			Name = "Laptop"
		};
	}

	[Fact]
	public void Add_ValidatorIsCalled_Passes()
	{
		var data = new Product();

		_mockValidator.Setup(x => x.Validate(data))
			.Returns(ValidationUtils.PassingValidationResult);
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);

		var service = new ProductService(_mockDataAccess.Object, _mockValidator.Object);

		var result = service.Add(data);

		result.Should().Be(1);
		_mockValidator.Verify(x => x.Validate(data));
		_mockDataAccess.Verify(x => x.Add(data));
	}

	[Fact]
	public void Add_ValidatorIsCalled_Fails()
	{
		var data = new Product();

		_mockValidator.Setup(x => x.Validate(data))
			.Returns(ValidationUtils.FailingValidationResult);
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);

		var service = new ProductService(_mockDataAccess.Object, _mockValidator.Object);

		var result = service.Add(data);

		result.Should().Be(default);
		_mockValidator.Verify(x => x.Validate(data));
		_mockDataAccess.Verify(x => x.Add(data), Times.Never());
	}

	[Theory]
	[InlineData(1)]
	[InlineData(10)]
	[InlineData(100)]
	public void Add_ValidProduct_ValidationPasses_ReturnsValidId(int id)
	{
		var data = GetValidProduct();
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(id);

		var service = new ProductService(_mockDataAccess.Object, _validator);

		var result = service.Add(data);

		result.Should().Be(id);
		_mockDataAccess.Verify(x => x.Add(data));
	}

	[Fact]
	public void Add_InvalidProduct_ValidationFails_ReturnsDefault()
	{
		var data = new Product();
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);

		var service = new ProductService(_mockDataAccess.Object, _validator);

		var result = service.Add(data);

		result.Should().Be(default);
		_mockDataAccess.Verify(x => x.Add(data), Times.Never());
	}

	private static IEnumerable<object[]> GetProductById()
	{
		yield return new object[] { 1, new Product() { Id = 1, CategoryId = 1, Name = "Dell laptop" } };
		yield return new object[] { 10, new Product() { Id = 10, CategoryId = 1, Name = "Asus laptop" } };
		yield return new object[] { 20, null };
	}

	[Theory]
	[MemberData(nameof(GetProductById))]
	public void GetById_ProvidedId_ReturnsExpected(int id, Product? data)
	{
		_mockDataAccess.Setup(x => x.Get(id))
			.Returns(data);

		var service = new ProductService(_mockDataAccess.Object, _validator);

		var result = service.Get(id);

		result.Should().Be(data);
		_mockDataAccess.Verify(x => x.Get(id));
	}

	private static IEnumerable<object[]> UpdateProducts()
	{
		yield return new object[] { true, Times.Once(), GetValidProduct() };
		yield return new object[] { false, Times.Never(), new Product() };
	}

	[Theory]
	[MemberData(nameof(UpdateProducts))]
	public void Update_ProvidedProduct_ReturnsProvided(bool status, Times times, Product data)
	{
		_mockDataAccess.Setup(x => x.Update(data))
			.Returns(status);

		var service = new ProductService(_mockDataAccess.Object, _validator);

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

		var service = new ProductService(_mockDataAccess.Object, _validator);

		var result = service.Delete(1);

		result.Should().Be(status);
		_mockDataAccess.Verify(x => x.Delete(It.IsAny<int>()));
	}

	private static IEnumerable<object[]> GetAll()
	{
		yield return new object[] { new List<Product>() };
		yield return new object[] { new List<Product>() {
				new Product() { Id = 1, CategoryId = 1, Name = "Dell laptop" },
				new Product() { Id = 2, CategoryId = 1, Name = "Asus laptop" }
			}
		};
	}

	[Theory]
	[MemberData(nameof(GetAll))]
	public void GetAll_ProvidedInput_ReturnsProvided(List<Product> data)
	{
		_mockDataAccess.Setup(x => x.GetAll())
			.Returns(data);

		var service = new ProductService(_mockDataAccess.Object, _validator);

		var result = service.GetAll();

		result.Should().BeEquivalentTo(data);
		_mockDataAccess.Verify(x => x.GetAll());
	}
}

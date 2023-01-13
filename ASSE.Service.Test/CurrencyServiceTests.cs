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
public class CurrencyServiceTests
{
	private readonly IValidator<Currency> _validator;
	private readonly Mock<IValidator<Currency>> _mockValidator;
	private readonly Mock<ICurrencyDataAccess> _mockDataAccess;

	public CurrencyServiceTests()
	{
		_validator = new CurrencyValidator();
		_mockDataAccess = new Mock<ICurrencyDataAccess>();
		_mockValidator = new Mock<IValidator<Currency>>();
	}

	public static Currency GetValidCurrency()
	{
		return new Currency()
		{
			Id = 1,
			Name = "Euro"
		};
	}

	[Fact]
	public void Add_ValidatorIsCalled_Passes()
	{
		var data = new Currency();

		_mockValidator.Setup(x => x.Validate(data))
			.Returns(ValidationUtils.PassingValidationResult);
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);

		var service = new CurrencyService(_mockDataAccess.Object, _mockValidator.Object);

		var result = service.Add(data);

		result.Should().Be(1);
		_mockValidator.Verify(x => x.Validate(data));
		_mockDataAccess.Verify(x => x.Add(data));
	}

	[Fact]
	public void Add_ValidatorIsCalled_Fails()
	{
		var data = new Currency();

		_mockValidator.Setup(x => x.Validate(data))
			.Returns(ValidationUtils.FailingValidationResult);
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);

		var service = new CurrencyService(_mockDataAccess.Object, _mockValidator.Object);

		var result = service.Add(data);

		result.Should().Be(default);
		_mockValidator.Verify(x => x.Validate(data));
		_mockDataAccess.Verify(x => x.Add(data), Times.Never());
	}

	[Theory]
	[InlineData(1)]
	[InlineData(10)]
	[InlineData(100)]
	public void Add_ValidCurrency_ValidationPasses_ReturnsValidId(int id)
	{
		var data = GetValidCurrency();
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(id);

		var service = new CurrencyService(_mockDataAccess.Object, _validator);

		var result = service.Add(data);

		result.Should().Be(id);
		_mockDataAccess.Verify(x => x.Add(data));
	}

	[Fact]
	public void Add_InvalidCurrency_ValidationFails_ReturnsDefault()
	{
		var data = new Currency();
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);

		var service = new CurrencyService(_mockDataAccess.Object, _validator);

		var result = service.Add(data);

		result.Should().Be(default);
		_mockDataAccess.Verify(x => x.Add(data), Times.Never());
	}

	private static IEnumerable<object[]> GetCurrencyById()
	{
		yield return new object[] { 1, new Currency() { Id = 1, Name = "Euro" } };
		yield return new object[] { 10, new Currency() { Id = 10, Name = "Ron" } };
		yield return new object[] { 20, null };
	}

	[ComplexTheory]
	[MemberData(nameof(GetCurrencyById))]
	public void GetById_ProvidedId_ReturnsExpected(int id, Currency? data)
	{
		_mockDataAccess.Setup(x => x.Get(id))
			.Returns(data);

		var service = new CurrencyService(_mockDataAccess.Object, _validator);

		var result = service.Get(id);

		result.Should().Be(data);
		_mockDataAccess.Verify(x => x.Get(id));
	}

	private static IEnumerable<object[]> UpdateCurrencies()
	{
		yield return new object[] { true, Times.Once(), GetValidCurrency() };
		yield return new object[] { false, Times.Never(), new Currency() { Id = 1 } };
		yield return new object[] { false, Times.Never(), new Currency() };
	}

	[ComplexTheory]
	[MemberData(nameof(UpdateCurrencies))]
	public void Update_ProvidedCurrency_ReturnsProvided(bool status, Times times, Currency data)
	{
		_mockDataAccess.Setup(x => x.Update(data))
			.Returns(status);

		var service = new CurrencyService(_mockDataAccess.Object, _validator);

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

		var service = new CurrencyService(_mockDataAccess.Object, _validator);

		var result = service.Delete(1);

		result.Should().Be(status);
		_mockDataAccess.Verify(x => x.Delete(It.IsAny<int>()));
	}

	private static IEnumerable<object[]> GetAll()
	{
		yield return new object[] { new List<Currency>() };
		yield return new object[] { new List<Currency>() {
				new Currency() { Id = 1, Name = "Euro" },
				new Currency() { Id = 2, Name = "Ron" }
			}
		};
	}

	[ComplexTheory]
	[MemberData(nameof(GetAll))]
	public void GetAll_ProvidedInput_ReturnsProvided(List<Currency> data)
	{
		_mockDataAccess.Setup(x => x.GetAll())
			.Returns(data);

		var service = new CurrencyService(_mockDataAccess.Object, _validator);

		var result = service.GetAll();

		result.Should().BeEquivalentTo(data);
		_mockDataAccess.Verify(x => x.GetAll());
	}
}

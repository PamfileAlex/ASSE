using ASSE.DomainModel.Models;
using ASSE.DomainModel.Validators;
using FluentAssertions;

namespace ASSE.DomainModel.Test;
public class CurrencyValidatorTests
{
	private readonly CurrencyValidator _validator;

	public CurrencyValidatorTests()
	{
		_validator = new CurrencyValidator();
	}

	[Fact]
	public void Validate_ValidCurrency_NoErrors()
	{
		var currency = new Currency()
		{
			Id = 1,
			Name = "RON"
		};

		var actual = _validator.Validate(currency);

		actual.IsValid.Should().BeTrue();
		actual.Errors.Should().BeEmpty();
	}

	[Fact]
	public void Validate_DefaultCurrency_Fails()
	{
		var currency = new Currency();

		var actual = _validator.Validate(currency);

		actual.IsValid.Should().BeFalse();
		actual.Errors.Should().HaveCountGreaterThan(0);
	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData(" ")]
	public void Validate_InvalidName_Fails(string name)
	{
		var currency = new Currency()
		{
			Name = name,
		};

		var actual = _validator.Validate(currency);

		actual.IsValid.Should().BeFalse();
		actual.Errors.Should().HaveCount(1);
	}
}

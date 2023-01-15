using ASSE.DomainModel.Models;
using ASSE.DomainModel.Validators;
using FluentValidation.TestHelper;

namespace ASSE.DomainModel.Tests;
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

		var result = _validator.TestValidate(currency);

		//result.IsValid.Should().BeTrue();
		result.ShouldNotHaveAnyValidationErrors();
	}

	[Fact]
	public void Validate_DefaultCurrency_Fails()
	{
		var currency = new Currency();

		var result = _validator.TestValidate(currency);

		//result.IsValid.Should().BeFalse();
		result.ShouldHaveAnyValidationError();
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

		var result = _validator.TestValidate(currency);

		//result.IsValid.Should().BeFalse();
		result.ShouldHaveValidationErrorFor(currency => currency.Name);
	}
}

using ASSE.DomainModel.Models;
using ASSE.DomainModel.Validators;
using FluentValidation.TestHelper;

namespace ASSE.DomainModel.Test;

public class UserValidatorTests
{
	private readonly UserValidator _validator;

	public UserValidatorTests()
	{
		_validator = new UserValidator();
	}

	[Fact]
	public void Validate_ValidUser_NoErrors()
	{
		var user = new User()
		{
			Id = 1,
			FirstName = "Max",
			LastName = "Verstappen",
			Score = 1,
		};

		var result = _validator.TestValidate(user);

		//result.IsValid.Should().BeTrue();
		result.ShouldNotHaveAnyValidationErrors();
	}

	[Fact]
	public void Validate_DefaultUser_Fails()
	{
		var user = new User();

		var result = _validator.TestValidate(user);

		//result.IsValid.Should().BeFalse();
		result.ShouldHaveAnyValidationError();
	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData(" ")]
	public void Validate_InvalidFirstName_Fails(string firstName)
	{
		var user = new User()
		{
			FirstName = firstName,
			LastName = "Verstappen",
			Score = 1,
		};

		var result = _validator.TestValidate(user);

		//result.IsValid.Should().BeFalse();
		result.ShouldHaveValidationErrorFor(user => user.FirstName);
	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData(" ")]
	public void Validate_InvalidLastName_Fails(string lastName)
	{
		var user = new User()
		{
			FirstName = "Max",
			LastName = lastName,
			Score = 1,
		};

		var result = _validator.TestValidate(user);

		//result.IsValid.Should().BeFalse();
		result.ShouldHaveValidationErrorFor(user => user.LastName);
	}

	[Theory]
	[InlineData(0.0)]
	[InlineData(-1.0)]
	public void Validate_InvalidScore_Fails(double score)
	{
		var user = new User()
		{
			FirstName = "Max",
			LastName = "Verstappen",
			Score = score,
		};

		var result = _validator.TestValidate(user);

		//result.IsValid.Should().BeFalse();
		result.ShouldHaveValidationErrorFor(user => user.Score);
	}
}
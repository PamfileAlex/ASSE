using ASSE.DomainModel.Models;
using ASSE.DomainModel.Validators;
using FluentValidation.TestHelper;

namespace ASSE.DomainModel.Test;
public class RoleValidatorTests
{
	private readonly RoleValidator _validator;

	public RoleValidatorTests()
	{
		_validator = new RoleValidator();
	}

	[Fact]
	public void Validate_ValidRole_NoErrors()
	{
		var role = new Role()
		{
			Id = 1,
			Name = "Admin"
		};

		var result = _validator.TestValidate(role);

		//result.IsValid.Should().BeTrue();
		result.ShouldNotHaveAnyValidationErrors();
	}

	[Fact]
	public void Validate_DefaultRole_Fails()
	{
		var role = new Role();

		var result = _validator.TestValidate(role);

		//result.IsValid.Should().BeFalse();
		result.ShouldHaveAnyValidationError();
	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData(" ")]
	public void Validate_InvalidName_Fails(string name)
	{
		var role = new Role()
		{
			Name = name,
		};

		var result = _validator.TestValidate(role);

		//result.IsValid.Should().BeFalse();
		result.ShouldHaveValidationErrorFor(role => role.Name);
	}
}

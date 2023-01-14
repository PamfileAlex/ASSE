using ASSE.DomainModel.Models;
using ASSE.DomainModel.Validators;
using FluentValidation.TestHelper;

namespace ASSE.DomainModel.Test;
public class UserRoleValidatorTests
{
	private readonly UserRoleValidator _validator;

	public UserRoleValidatorTests()
	{
		_validator = new UserRoleValidator();
	}

	[Fact]
	public void Validate_ValidUserRole_NoErrors()
	{
		var data = new UserRole(1, 1);

		var result = _validator.TestValidate(data);

		result.ShouldNotHaveAnyValidationErrors();
	}

	[Fact]
	public void Validate_DefaultUserRole_Fails()
	{
		var data = new UserRole();

		var result = _validator.TestValidate(data);

		result.ShouldHaveAnyValidationError();
	}

	[Fact]
	public void Validate_NoUserId_Fails()
	{
		var data = new UserRole()
		{
			RoleId = 1
		};

		var result = _validator.TestValidate(data);

		result.ShouldHaveValidationErrorFor(x => x.UserId);
	}

	[Fact]
	public void Validate_NoRoleId_Fails()
	{
		var data = new UserRole()
		{
			UserId = 1
		};

		var result = _validator.TestValidate(data);

		result.ShouldHaveValidationErrorFor(x => x.RoleId);
	}
}

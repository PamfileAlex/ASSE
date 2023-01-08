using ASSE.DomainModel.Models;
using ASSE.DomainModel.Validators;
using FluentAssertions;
using FluentValidation;

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

		var actual = _validator.Validate(role);

		actual.IsValid.Should().BeTrue();
		actual.Errors.Should().BeEmpty();
	}

	[Fact]
	public void Validate_DefaultRole_Fails()
	{
		var role = new Role();

		var actual = _validator.Validate(role);

		actual.IsValid.Should().BeFalse();
		actual.Errors.Should().HaveCountGreaterThan(0);
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

		var actual = _validator.Validate(role);

		actual.IsValid.Should().BeFalse();
		actual.Errors.Should().HaveCount(1);
	}
}

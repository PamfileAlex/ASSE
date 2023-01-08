using ASSE.DomainModel.Models;
using ASSE.DomainModel.Validators;
using FluentAssertions;

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

		var actual = _validator.Validate(user);

		actual.IsValid.Should().BeTrue();
		actual.Errors.Should().BeEmpty();
	}

	[Fact]
	public void Validate_NoFirstName_ReturnsFalse()
	{
		var user = new User()
		{
			LastName = "Verstappen",
			Score = 1,
		};

		var actual = _validator.Validate(user);

		actual.IsValid.Should().BeFalse();
		actual.Errors.Should().HaveCount(1);
	}
}
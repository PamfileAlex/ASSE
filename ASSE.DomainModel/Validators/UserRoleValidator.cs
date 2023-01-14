using ASSE.DomainModel.Models;
using FluentValidation;

namespace ASSE.DomainModel.Validators;
public class UserRoleValidator : AbstractValidator<UserRole>
{
	public UserRoleValidator()
	{
		RuleFor(x => x.UserId).NotEmpty();
		RuleFor(x => x.RoleId).NotEmpty();
	}
}

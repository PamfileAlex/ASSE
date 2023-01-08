using ASSE.DomainModel.Models;
using FluentValidation;

namespace ASSE.DomainModel.Validators;
public class RoleValidator : AbstractValidator<Role>
{
	public RoleValidator()
	{
		RuleFor(x => x.Name).NotEmpty();
	}
}

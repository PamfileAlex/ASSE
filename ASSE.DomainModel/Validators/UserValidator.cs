using ASSE.DomainModel.Models;
using FluentValidation;

namespace ASSE.DomainModel.Validators;
public class UserValidator : AbstractValidator<User>
{
	public UserValidator()
	{
		//RuleFor(x => x.Id).NotEmpty();
		RuleFor(x => x.FirstName).NotEmpty();
		RuleFor(x => x.LastName).NotEmpty();
		RuleFor(x => x.Score).NotEmpty();
	}
}

using ASSE.DomainModel.Models;
using FluentValidation;

namespace ASSE.DomainModel.Validators;
public class CategoryValidator : AbstractValidator<Category>
{
	public CategoryValidator()
	{
		RuleFor(x => x.ParentId).NotEmpty();
		RuleFor(x => x.Name).NotEmpty();
	}
}

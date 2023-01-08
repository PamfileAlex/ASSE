using ASSE.DomainModel.Models;
using FluentValidation;

namespace ASSE.DomainModel.Validators;
public class ProductValidator : AbstractValidator<Product>
{
	public ProductValidator()
	{
		RuleFor(x => x.CategoryId).NotEmpty();
		RuleFor(x => x.Name).NotEmpty();
	}
}

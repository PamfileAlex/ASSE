using ASSE.DomainModel.Models;
using FluentValidation;

namespace ASSE.DomainModel.Validators;
public class AuctionValidator : AbstractValidator<Auction>
{
	public AuctionValidator()
	{
		RuleFor(x => x.OwnerId).NotEmpty();
		RuleFor(x => x.ProductId).NotEmpty();
		RuleFor(x => x.CurrencyId).NotEmpty();
		RuleFor(x => x.BuyerId).NotEmpty();
		RuleFor(x => x.Description).NotEmpty();
		RuleFor(x => x.StartDate).NotEmpty().GreaterThanOrEqualTo(x => DateTime.Now);
		RuleFor(x => x.EndDate).NotEmpty().GreaterThan(x => x.StartDate);
		RuleFor(x => x.StartingPrice).NotEmpty().GreaterThan(0.0);
		RuleFor(x => x.CurrentPrice).NotEmpty().GreaterThan(x => x.StartingPrice);
	}
}

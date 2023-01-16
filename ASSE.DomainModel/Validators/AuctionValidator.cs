// --------------------------------------------------------------------------------------
// <copyright file="AuctionValidator.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.Core.Services;
using ASSE.DomainModel.Models;
using FluentValidation;

namespace ASSE.DomainModel.Validators;

/// <summary>
/// Validator for <see cref="Auction"/>.
/// </summary>
public class AuctionValidator : AbstractValidator<Auction>
{
	private readonly IDateTimeProvider _dateTimeProvider;

	/// <summary>
	/// Initializes a new instance of the <see cref="AuctionValidator"/> class.
	/// </summary>
	/// <param name="dateTimeProvider"><see cref="IDateTimeProvider"/> instance needed for mocking.</param>
	public AuctionValidator(IDateTimeProvider dateTimeProvider)
	{
		_dateTimeProvider = dateTimeProvider;

		RuleFor(x => x.OwnerId).NotEmpty();
		RuleFor(x => x.ProductId).NotEmpty();
		RuleFor(x => x.CurrencyId).NotEmpty();
		RuleFor(x => x.Description).NotEmpty();
		RuleFor(x => x.StartDate).NotEmpty().GreaterThanOrEqualTo(x => _dateTimeProvider.Now);
		RuleFor(x => x.EndDate).NotEmpty().GreaterThan(x => x.StartDate);
		RuleFor(x => x.StartingPrice).NotEmpty().GreaterThan(0.0);
		RuleFor(x => x.CurrentPrice).GreaterThan(x => x.StartingPrice).When(x => x.CurrentPrice is not null);
	}
}

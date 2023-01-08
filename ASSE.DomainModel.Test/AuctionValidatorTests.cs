using ASSE.Core.Services;
using ASSE.DomainModel.Models;
using ASSE.DomainModel.Validators;
using FluentValidation.TestHelper;
using Moq;

namespace ASSE.DomainModel.Test;
public class AuctionValidatorTests
{
	private readonly AuctionValidator _validator;
	private readonly Mock<IDateTimeProvider> _dateTimeProvider;

	public AuctionValidatorTests()
	{
		_dateTimeProvider = new Mock<IDateTimeProvider>();
		_validator = new AuctionValidator(_dateTimeProvider.Object);
	}

	[Fact]
	public void Validate_ValidAuction_NoErrors()
	{
		_dateTimeProvider.Setup(x => x.Now).Returns(new DateTime(2020, 11, 1));

		var auction = new Auction()
		{
			Id = 1,
			OwnerId = 1,
			ProductId = 1,
			CurrencyId = 1,
			BuyerId = 1,
			Description = "Description",
			StartDate = _dateTimeProvider.Object.Now,
			EndDate = new DateTime(2020, 12, 1),
			StartingPrice = 100,
			CurrentPrice = 200,
			IsActive = true
		};

		var result = _validator.TestValidate(auction);
		result.ShouldNotHaveAnyValidationErrors();
	}

	private static IEnumerable<object[]> GetValidStartDateTimes()
	{
		yield return new object[] { new DateTime(2020, 11, 1) };
		yield return new object[] { new DateTime(2020, 11, 2) };
	}

	[Theory]
	[MemberData(nameof(GetValidStartDateTimes))]
	public void Validate_ValidStartDate_NoErrorsForStartDate(DateTime dateTime)
	{
		_dateTimeProvider.Setup(x => x.Now).Returns(new DateTime(2020, 11, 1));

		var auction = new Auction()
		{
			StartDate = dateTime
		};

		var result = _validator.TestValidate(auction);

		result.ShouldNotHaveValidationErrorFor(auction => auction.StartDate);
	}

	[Fact]
	public void Validate_DefaultAuction_Fails()
	{
		var auction = new Auction();

		var result = _validator.TestValidate(auction);

		result.ShouldHaveAnyValidationError();
	}

	[Fact]
	public void Validate_InvalidOwnerId_Fails()
	{
		var auction = new Auction();

		var result = _validator.TestValidate(auction);

		result.ShouldHaveValidationErrorFor(auction => auction.OwnerId);
	}

	[Fact]
	public void Validate_InvalidProductId_Fails()
	{
		var auction = new Auction();

		var result = _validator.TestValidate(auction);

		result.ShouldHaveValidationErrorFor(auction => auction.ProductId);
	}

	[Fact]
	public void Validate_InvalidCurrencyId_Fails()
	{
		var auction = new Auction();

		var result = _validator.TestValidate(auction);

		result.ShouldHaveValidationErrorFor(auction => auction.CurrencyId);
	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData(" ")]
	public void Validate_InvalidDescription_Fails(string description)
	{
		var auction = new Auction()
		{
			Description = description
		};

		var result = _validator.TestValidate(auction);

		result.ShouldHaveValidationErrorFor(auction => auction.Description);
	}


	private static IEnumerable<object[]> GetInvalidStartDateTimes()
	{
		//yield return new object[] { null };
		yield return new object[] { default(DateTime) };
		yield return new object[] { new DateTime(2020, 10, 1) };
	}

	[Theory]
	[MemberData(nameof(GetInvalidStartDateTimes))]
	public void Validate_InvalidStartDate_Fails(DateTime dateTime)
	{
		_dateTimeProvider.Setup(x => x.Now).Returns(new DateTime(2020, 11, 1));
		var auction = new Auction()
		{
			StartDate = dateTime,
		};

		var result = _validator.TestValidate(auction);

		result.ShouldHaveValidationErrorFor(auction => auction.StartDate);
	}

	private static IEnumerable<object[]> GetInvalidEndDateTimes()
	{
		//yield return new object[] { null };
		yield return new object[] { default(DateTime) };
		yield return new object[] { new DateTime(2020, 10, 1) };
		yield return new object[] { new DateTime(2020, 11, 1) };
	}

	[Theory]
	[MemberData(nameof(GetInvalidEndDateTimes))]
	public void Validate_InvalidEndDate_Fails(DateTime dateTime)
	{
		_dateTimeProvider.Setup(x => x.Now).Returns(new DateTime(2020, 11, 1));
		var auction = new Auction()
		{
			StartDate = _dateTimeProvider.Object.Now,
			EndDate = dateTime,
		};

		var result = _validator.TestValidate(auction);

		result.ShouldHaveValidationErrorFor(auction => auction.EndDate);
	}

	[Theory]
	[InlineData(0.0)]
	[InlineData(-1.0)]
	public void Validate_InvalidStartingPrice_Fails(double startingPrice)
	{
		var auction = new Auction()
		{
			StartingPrice = startingPrice,
		};

		var result = _validator.TestValidate(auction);

		result.ShouldHaveValidationErrorFor(auction => auction.StartingPrice);
	}
}

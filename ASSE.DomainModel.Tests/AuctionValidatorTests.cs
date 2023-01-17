// --------------------------------------------------------------------------------------
// <copyright file="AuctionValidatorTests.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.Core.Services;
using ASSE.DomainModel.Models;
using ASSE.DomainModel.Validators;
using FluentValidation.TestHelper;
using Moq;

namespace ASSE.DomainModel.Tests;

/// <summary>
/// Tests for <see cref="AuctionValidator"/>.
/// </summary>
public class AuctionValidatorTests
{
	private readonly AuctionValidator _validator;
	private readonly Mock<IDateTimeProvider> _dateTimeProvider;

	/// <summary>
	/// Initializes a new instance of the <see cref="AuctionValidatorTests"/> class.
	/// </summary>
	public AuctionValidatorTests()
	{
		_dateTimeProvider = new Mock<IDateTimeProvider>();
		_validator = new AuctionValidator(_dateTimeProvider.Object);
	}

	/// <summary>
	/// Test for valid <see cref="Auction"/>.
	/// </summary>
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
			IsActive = true,
		};

		var result = _validator.TestValidate(auction);
		result.ShouldNotHaveAnyValidationErrors();
	}

	private static IEnumerable<object[]> GetValidStartDateTimes()
	{
		yield return new object[] { new DateTime(2020, 11, 1) };
		yield return new object[] { new DateTime(2020, 11, 2) };
	}

	/// <summary>
	/// Test for valid <see cref="Auction.StartDate"/>.
	/// </summary>
	/// <param name="dateTime"><see cref="DateTime"/> instance.</param>
	[Theory]
	[MemberData(nameof(GetValidStartDateTimes))]
	public void Validate_ValidStartDate_NoErrorsForStartDate(DateTime dateTime)
	{
		_dateTimeProvider.Setup(x => x.Now).Returns(new DateTime(2020, 11, 1));

		var auction = new Auction()
		{
			StartDate = dateTime,
		};

		var result = _validator.TestValidate(auction);

		result.ShouldNotHaveValidationErrorFor(auction => auction.StartDate);
	}

	/// <summary>
	/// Test that default <see cref="Auction"/> fails.
	/// </summary>
	[Fact]
	public void Validate_DefaultAuction_Fails()
	{
		var auction = new Auction();

		var result = _validator.TestValidate(auction);

		result.ShouldHaveAnyValidationError();
	}

	/// <summary>
	/// Test that invalid <see cref="Auction.OwnerId"/> fails.
	/// </summary>
	[Fact]
	public void Validate_InvalidOwnerId_Fails()
	{
		var auction = new Auction();

		var result = _validator.TestValidate(auction);

		result.ShouldHaveValidationErrorFor(auction => auction.OwnerId);
	}

	/// <summary>
	/// Test that invalid <see cref="Auction.ProductId"/> fails.
	/// </summary>
	[Fact]
	public void Validate_InvalidProductId_Fails()
	{
		var auction = new Auction();

		var result = _validator.TestValidate(auction);

		result.ShouldHaveValidationErrorFor(auction => auction.ProductId);
	}

	/// <summary>
	/// Test that invalid <see cref="Auction.CurrencyId"/> fails.
	/// </summary>
	[Fact]
	public void Validate_InvalidCurrencyId_Fails()
	{
		var auction = new Auction();

		var result = _validator.TestValidate(auction);

		result.ShouldHaveValidationErrorFor(auction => auction.CurrencyId);
	}

	/// <summary>
	/// Test that invalid <see cref="Auction.Description"/> fails.
	/// </summary>
	/// <param name="description">Parametrized description value.</param>
	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData(" ")]
	public void Validate_InvalidDescription_Fails(string description)
	{
		var auction = new Auction()
		{
			Description = description,
		};

		var result = _validator.TestValidate(auction);

		result.ShouldHaveValidationErrorFor(auction => auction.Description);
	}

	private static IEnumerable<object[]> GetInvalidStartDateTimes()
	{
		yield return new object[] { default(DateTime) };
		yield return new object[] { new DateTime(2020, 10, 1) };
	}

	/// <summary>
	/// Test that invalid <see cref="Auction.StartDate"/> fails.
	/// </summary>
	/// <param name="dateTime">Parametrized start date time value.</param>
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
		yield return new object[] { default(DateTime) };
		yield return new object[] { new DateTime(2020, 10, 1) };
		yield return new object[] { new DateTime(2020, 11, 1) };
	}

	/// <summary>
	/// Test that invalid <see cref="Auction.EndDate"/> fails.
	/// </summary>
	/// <param name="dateTime">Parametrized end date time value.</param>
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

	/// <summary>
	/// Test that invalid <see cref="Auction.StartingPrice"/> fails.
	/// </summary>
	/// <param name="startingPrice">Parametrized starting price value.</param>
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

	/// <summary>
	/// Test that invalid <see cref="Auction.CurrentPrice"/> fails.
	/// </summary>
	/// <param name="currentPrice">Parametrized current price value.</param>
	[Theory]
	[InlineData(0.0)]
	[InlineData(-1.0)]
	[InlineData(500.0)]
	public void Validate_InvalidCurrentPrice_Fails(double currentPrice)
	{
		var auction = new Auction()
		{
			StartingPrice = 500,
			CurrentPrice = currentPrice,
		};

		var result = _validator.TestValidate(auction);

		result.ShouldHaveValidationErrorFor(auction => auction.CurrentPrice);
	}
}

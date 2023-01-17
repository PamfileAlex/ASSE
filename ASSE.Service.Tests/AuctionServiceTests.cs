// --------------------------------------------------------------------------------------
// <copyright file="AuctionServiceTests.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.Core.Services;
using ASSE.Core.Test;
using ASSE.Core.Test.xUnit;
using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.DomainModel.Validators;
using ASSE.Service.Implementations;
using FluentAssertions;
using FluentValidation;
using Moq;
using Serilog;

namespace ASSE.Service.Tests;

/// <summary>
/// Tests for <see cref="AuctionService"/>.
/// </summary>
public class AuctionServiceTests
{
	private readonly IValidator<Auction> _validator;
	private readonly IConfigProvider _configProvider;
	private readonly IDateTimeProvider _dateTimeProvider;
	private readonly Mock<ILogger> _mockLogger;
	private readonly Mock<IConfigProvider> _mockConfigProvider;
	private readonly Mock<IValidator<Auction>> _mockValidator;
	private readonly Mock<IAuctionDataAccess> _mockDataAccess;
	private readonly Mock<IDateTimeProvider> _mockDateTimeProvider;

	/// <summary>
	/// Initializes a new instance of the <see cref="AuctionServiceTests"/> class.
	/// </summary>
	public AuctionServiceTests()
	{
		_mockLogger = new Mock<ILogger>();
		_configProvider = new ConfigProvider(_mockLogger.Object);
		_dateTimeProvider = new DateTimeProvider();
		_mockConfigProvider = new Mock<IConfigProvider>();
		_mockDateTimeProvider = new Mock<IDateTimeProvider>();
		_mockDateTimeProvider.Setup(x => x.Now)
			.Returns(new DateTime(2023, 1, 1));
		_validator = new AuctionValidator(_mockDateTimeProvider.Object);
		_mockDataAccess = new Mock<IAuctionDataAccess>();
		_mockValidator = new Mock<IValidator<Auction>>();
	}

	/// <summary>
	/// Gets a new valid <see cref="Auction"/>.
	/// </summary>
	/// <returns>Returns a valid auction.</returns>
	public static Auction GetValidAuction()
	{
		return new Auction()
		{
			Id = 1,
			OwnerId = 1,
			ProductId = 1,
			CurrencyId = 1,
			Description = "Description",
			StartDate = new DateTime(2023, 1, 1),
			EndDate = new DateTime(2023, 1, 2),
			StartingPrice = 10,
			IsActive = true,
		};
	}

	/// <summary>
	/// Gets a new valid <see cref="Auction"/> for update.
	/// </summary>
	/// <param name="buyerId">Buyer identity param.</param>
	/// <param name="currentPrice">Current price param.</param>
	/// <returns>Returns a valid auction.</returns>
	public static Auction GetValidUpdateAuction(int? buyerId = 2, double? currentPrice = 15.0)
	{
		var auction = GetValidAuction();
		auction.BuyerId = buyerId;
		auction.CurrentPrice = currentPrice;
		return auction;
	}

	/// <summary>
	/// Test that validator is called on Add method on passing validator.
	/// </summary>
	[Fact]
	public void Add_ValidatorIsCalled_Passes()
	{
		var data = new Auction();

		_mockValidator.Setup(x => x.Validate(data))
			.Returns(ValidationUtils.PassingValidationResult);
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);
		_mockDataAccess.Setup(x => x.GetAllActiveByOwnerId(It.IsAny<int>()))
			.Returns(new List<Auction>());
		_mockConfigProvider.Setup(x => x.MaxAuctions)
			.Returns(1);

		var service = new AuctionService(_mockDataAccess.Object, _mockValidator.Object, _mockConfigProvider.Object, _mockDateTimeProvider.Object);

		var result = service.Add(data);

		result.Should().Be(1);
		_mockValidator.Verify(x => x.Validate(data));
		_mockDataAccess.Verify(x => x.Add(data));
		_mockDataAccess.Verify(x => x.GetAllActiveByOwnerId(It.IsAny<int>()));
		_mockConfigProvider.Verify(x => x.MaxAuctions);
	}

	/// <summary>
	/// Test that validator is called on Add method on failing validator.
	/// </summary>
	[Fact]
	public void Add_ValidatorIsCalled_Fails()
	{
		var data = new Auction();

		_mockValidator.Setup(x => x.Validate(data))
			.Returns(ValidationUtils.FailingValidationResult);
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);

		var service = new AuctionService(_mockDataAccess.Object, _mockValidator.Object, _mockConfigProvider.Object, _mockDateTimeProvider.Object);

		var result = service.Add(data);

		result.Should().Be(default);
		_mockValidator.Verify(x => x.Validate(data));
		_mockDataAccess.Verify(x => x.Add(data), Times.Never());
	}

	/// <summary>
	/// Test that Add method returns expected id.
	/// </summary>
	/// <param name="id">Parametrized id value.</param>
	[Theory]
	[InlineData(1)]
	[InlineData(10)]
	[InlineData(100)]
	public void Add_ValidAuction_ValidationPasses_ReturnsValidId(int id)
	{
		var data = GetValidAuction();
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(id);
		_mockDataAccess.Setup(x => x.GetAllActiveByOwnerId(It.IsAny<int>()))
			.Returns(new List<Auction>());

		var service = new AuctionService(_mockDataAccess.Object, _validator, _configProvider, _mockDateTimeProvider.Object);

		var result = service.Add(data);

		result.Should().Be(id);
		_mockDataAccess.Verify(x => x.Add(data));
		_mockDataAccess.Verify(x => x.GetAllActiveByOwnerId(It.IsAny<int>()));
	}

	/// <summary>
	/// Test that Add method returns default when validator fails for invalid <see cref="Auction"/>.
	/// </summary>
	[Fact]
	public void Add_InvalidAuction_ValidationFails_ReturnsDefault()
	{
		var data = new Auction();
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object, _mockDateTimeProvider.Object);

		var result = service.Add(data);

		result.Should().Be(default);
		_mockDataAccess.Verify(x => x.Add(data), Times.Never());
	}

	private static IEnumerable<object?[]> GetAuctionById()
	{
		yield return new object?[] { 1, GetValidAuction() };
		var data = GetValidAuction();
		data.Id = 10;
		yield return new object?[] { 10, data };
		yield return new object?[] { 20, null };
	}

	/// <summary>
	/// Test GetById method.
	/// </summary>
	/// <param name="id">Parametrized id value.</param>
	/// <param name="data">Parametrized data value.</param>
	[ComplexTheory]
	[MemberData(nameof(GetAuctionById))]
	public void GetById_ProvidedId_ReturnsExpected(int id, Auction? data)
	{
		_mockDataAccess.Setup(x => x.Get(id))
			.Returns(data);

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object, _mockDateTimeProvider.Object);

		var result = service.Get(id);

		result.Should().Be(data);
		_mockDataAccess.Verify(x => x.Get(id));
	}

	private static IEnumerable<object[]> UpdateAuctions()
	{
		yield return new object[] { true, Times.Once(), GetValidAuction() };
		yield return new object[] { false, Times.Never(), new Auction() { Id = 1 } };
		yield return new object[] { false, Times.Never(), new Auction() };
	}

	/// <summary>
	/// Test for Update method.
	/// </summary>
	/// <param name="expected">Expected result.</param>
	/// <param name="times">Times the data access Update method gets called.</param>
	/// <param name="data">Parametrized data value.</param>
	[ComplexTheory]
	[MemberData(nameof(UpdateAuctions))]
	public void Update_ProvidedAuction_ReturnsProvided(bool expected, Times times, Auction data)
	{
		_mockDataAccess.Setup(x => x.Update(data))
			.Returns(expected);

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object, _mockDateTimeProvider.Object);

		var result = service.Update(data);

		result.Should().Be(expected);
		_mockDataAccess.Verify(x => x.Update(data), times);
	}

	/// <summary>
	/// Test Update method with valid <see cref="Auction"/>.
	/// </summary>
	[Fact]
	public void Update_ValidatePrice_ValidAuction()
	{
		var data = GetValidUpdateAuction();

		_mockDataAccess.Setup(x => x.Update(data))
			.Returns(true);
		_mockDataAccess.Setup(x => x.Get(data.Id))
			.Returns(GetValidAuction());

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object, _mockDateTimeProvider.Object);

		var result = service.Update(data);

		result.Should().Be(true);
		_mockDataAccess.Verify(x => x.Update(data));
		_mockDataAccess.Verify(x => x.Get(data.Id));
	}

	/// <summary>
	/// Test Update method early exit for Validate Price method.
	/// </summary>
	[Fact]
	public void Update_ValidatePrice_EarlyExit()
	{
		var data = GetValidAuction();

		_mockDataAccess.Setup(x => x.Update(data))
			.Returns(true);

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object, _mockDateTimeProvider.Object);

		var result = service.Update(data);

		result.Should().Be(true);
		_mockDataAccess.Verify(x => x.Update(data));
		_mockDataAccess.Verify(x => x.Get(data.Id), Times.Never());
	}

	private static IEnumerable<object?[]> Update_ValidatePrice_InvalidAuctions()
	{
		yield return new object?[] { GetValidUpdateAuction(), null, Times.Once() };
		yield return new object?[] { GetValidUpdateAuction(buyerId: null), null, Times.Never() };
		yield return new object?[] { GetValidUpdateAuction(currentPrice: null), null, Times.Never() };
		yield return new object?[] { GetValidUpdateAuction(), new Auction() { CurrentPrice = 15.0 }, Times.Once() };
		yield return new object?[] { GetValidUpdateAuction(), new Auction() { CurrentPrice = 1.0 }, Times.Once() };
	}

	/// <summary>
	/// Test Update Validate Price invalid <see cref="Auction"/>.
	/// </summary>
	/// <param name="data">Parametrized data value.</param>
	/// <param name="previous">Parametrized previous data value.</param>
	/// <param name="getTimes">Times the Get method of data access gets called.</param>
	[ComplexTheory]
	[MemberData(nameof(Update_ValidatePrice_InvalidAuctions))]
	public void Update_ValidatePrice_Invalid(Auction data, Auction? previous, Times getTimes)
	{
		_mockDataAccess.Setup(x => x.Update(data))
			.Returns(true);
		_mockDataAccess.Setup(x => x.Get(data.Id))
			.Returns(previous);

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object, _mockDateTimeProvider.Object);

		var result = service.Update(data);

		result.Should().Be(false);
		_mockDataAccess.Verify(x => x.Update(data), Times.Never());
		_mockDataAccess.Verify(x => x.Get(data.Id), getTimes);
	}

	/// <summary>
	/// Test Delete method.
	/// </summary>
	/// <param name="expected">Expected result.</param>
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	public void Delete_AnyId_ReturnsExpected(bool expected)
	{
		_mockDataAccess.Setup(x => x.Delete(It.IsAny<int>()))
			.Returns(expected);

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object, _mockDateTimeProvider.Object);

		var result = service.Delete(1);

		result.Should().Be(expected);
		_mockDataAccess.Verify(x => x.Delete(It.IsAny<int>()));
	}

	private static IEnumerable<object[]> GetAll()
	{
		yield return new object[] { new List<Auction>() };
		yield return new object[]
		{
			new List<Auction>()
			{
				GetValidAuction(),
				GetValidAuction(),
			},
		};
	}

	/// <summary>
	/// Test GetAll method.
	/// </summary>
	/// <param name="data">Parametrized list of auctions.</param>
	[ComplexTheory]
	[MemberData(nameof(GetAll))]
	public void GetAll_ProvidedInput_ReturnsProvided(List<Auction> data)
	{
		_mockDataAccess.Setup(x => x.GetAll())
			.Returns(data);

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object, _mockDateTimeProvider.Object);

		var result = service.GetAll();

		result.Should().BeEquivalentTo(data);
		_mockDataAccess.Verify(x => x.GetAll());
	}

	/// <summary>
	/// Test GetAllActive method.
	/// </summary>
	/// <param name="data">Parametrized list of auctions.</param>
	[ComplexTheory]
	[MemberData(nameof(GetAll))]
	public void GetAllActive_ProvidedInput_ReturnsProvided(List<Auction> data)
	{
		_mockDataAccess.Setup(x => x.GetAllActive())
			.Returns(data);

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object, _mockDateTimeProvider.Object);

		var result = service.GetAllActive();

		result.Should().BeEquivalentTo(data);
		_mockDataAccess.Verify(x => x.GetAllActive());
	}

	/// <summary>
	/// Test GetAllActiveByOwnerId method.
	/// </summary>
	/// <param name="data">Parametrized list of auctions.</param>
	[ComplexTheory]
	[MemberData(nameof(GetAll))]
	public void GetAllActiveByOwnerId_ProvidedInput_ReturnsProvided(List<Auction> data)
	{
		_mockDataAccess.Setup(x => x.GetAllActiveByOwnerId(It.IsAny<int>()))
			.Returns(data);

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object, _mockDateTimeProvider.Object);

		var result = service.GetAllActiveByOwnerId(1);

		result.Should().BeEquivalentTo(data);
		_mockDataAccess.Verify(x => x.GetAllActiveByOwnerId(It.IsAny<int>()));
	}

	private static IEnumerable<object[]> ValidateLevenshteinDistanceData()
	{
		yield return new object[] { true, GetValidAuction(), new List<Auction>() };
		yield return new object[] { false, GetValidAuction(), new List<Auction>() { GetValidAuction() } };
	}

	/// <summary>
	/// Test ValidateLevenshteinDistance method.
	/// </summary>
	/// <param name="expected">Expected result.</param>
	/// <param name="auction">Parametrized auction data.</param>
	/// <param name="auctions">Parametrized list of auctions.</param>
	[ComplexTheory]
	[MemberData(nameof(ValidateLevenshteinDistanceData))]
	public void ValidateLevenshteinDistance(bool expected, Auction auction, List<Auction> auctions)
	{
		_mockDataAccess.Setup(x => x.GetAllActiveByOwnerId(auction.OwnerId))
			.Returns(auctions);

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object, _mockDateTimeProvider.Object);

		var result = service.ValidateLevenshteinDistance(auction);

		result.Should().Be(expected);
		_mockDataAccess.Verify(x => x.GetAllActiveByOwnerId(auction.OwnerId));
	}

	/// <summary>
	/// Test that Add fails on valid <see cref="Auction"/> because of LevenshteinDistance check.
	/// </summary>
	[Fact]
	public void Add_ValidAuction_FailsLevenshteinDistance()
	{
		var data = GetValidAuction();

		_mockDataAccess.Setup(x => x.GetAllActiveByOwnerId(data.OwnerId))
			.Returns(new List<Auction>() { GetValidAuction() });

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object, _mockDateTimeProvider.Object);

		var result = service.Add(data);

		result.Should().Be(default);
		_mockDataAccess.Verify(x => x.GetAllActiveByOwnerId(data.OwnerId));
		_mockDataAccess.Verify(x => x.Add(data), Times.Never());
	}

	private static IEnumerable<object[]> ValidateMaxAuctionsData()
	{
		yield return new object[] { true, new List<Auction>() };
		yield return new object[] { true, new List<Auction>() { new Auction() } };
		yield return new object[] { false, new List<Auction>() { new Auction(), new Auction() } };
	}

	/// <summary>
	/// Test ValidateMaxAuctions method.
	/// </summary>
	/// <param name="expected">Expected result.</param>
	/// <param name="auctions">List of auctions.</param>
	[ComplexTheory]
	[MemberData(nameof(ValidateMaxAuctionsData))]
	public void ValidateMaxAuctions(bool expected, List<Auction> auctions)
	{
		var data = GetValidAuction();

		_mockDataAccess.Setup(x => x.GetAllActiveByOwnerId(data.OwnerId))
			.Returns(auctions);
		_mockConfigProvider.Setup(x => x.MaxAuctions)
			.Returns(2);

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object, _mockDateTimeProvider.Object);

		var result = service.ValidateMaxAuctions(data);

		result.Should().Be(expected);
		_mockDataAccess.Verify(x => x.GetAllActiveByOwnerId(data.OwnerId));
		_mockConfigProvider.Verify(x => x.MaxAuctions);
	}

	/// <summary>
	/// Test that Add fails on valid <see cref="Auction"/> because of maximum number of auctions reached check.
	/// </summary>
	[Fact]
	public void Add_ValidAuction_FailsMaxAuctions()
	{
		var data = GetValidAuction();

		_mockDataAccess.Setup(x => x.GetAllActiveByOwnerId(data.OwnerId))
			.Returns(new List<Auction>() { new Auction() });
		_mockConfigProvider.Setup(x => x.MaxAuctions)
			.Returns(1);

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object, _mockDateTimeProvider.Object);

		var result = service.Add(data);

		result.Should().Be(default);
		_mockDataAccess.Verify(x => x.GetAllActiveByOwnerId(data.OwnerId));
		_mockDataAccess.Verify(x => x.Add(data), Times.Never());
		_mockConfigProvider.Verify(x => x.MaxAuctions);
	}

	private static IEnumerable<object?[]> CheckAndClose_EarlyExit_Data()
	{
		yield return new object?[] { null };
		yield return new object?[] { new Auction() { IsActive = false } };
	}

	/// <summary>
	/// Tests early exit condition for <see cref="AuctionService.CheckAndClose(Auction?)"/>.
	/// </summary>
	/// <param name="data">Parametrized data param.</param>
	[ComplexTheory]
	[MemberData(nameof(CheckAndClose_EarlyExit_Data))]
	public void CheckAndClose_EarlyExit(Auction? data)
	{
		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object, _mockDateTimeProvider.Object);

		service.CheckAndClose(data);

		_mockDataAccess.Verify(x => x.Update(It.IsAny<Auction>()), Times.Never());
		_mockDateTimeProvider.Verify(x => x.Now, Times.Never());
	}

	/// <summary>
	/// Test that for auction that ended, the update method is called.
	/// </summary>
	[Fact]
	public void CheckAndClose_CallsUpdate()
	{
		var data = GetValidAuction();
		_mockDataAccess.Setup(x => x.Update(data))
			.Returns(true);
		_mockDateTimeProvider.Setup(x => x.Now)
			.Returns(new DateTime(2023, 2, 2));

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object, _mockDateTimeProvider.Object);

		service.CheckAndClose(data);

		_mockDataAccess.Verify(x => x.Update(It.IsAny<Auction>()));
		_mockDateTimeProvider.Verify(x => x.Now);
	}
}

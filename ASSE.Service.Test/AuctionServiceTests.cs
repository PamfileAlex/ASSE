using ASSE.Core.Services;
using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.DomainModel.Validators;
using ASSE.Service.Implementations;
using ASSE.Core.Test.xUnit;
using FluentAssertions;
using FluentValidation;
using Moq;
using ASSE.Core.Test;
using Serilog;

namespace ASSE.Service.Tests;
public class AuctionServiceTests
{
	private readonly IValidator<Auction> _validator;
	private readonly IConfigProvider _configProvider;
	private readonly Mock<ILogger> _mockLogger;
	private readonly Mock<IConfigProvider> _mockConfigProvider;
	private readonly Mock<IValidator<Auction>> _mockValidator;
	private readonly Mock<IAuctionDataAccess> _mockDataAccess;
	private readonly Mock<IDateTimeProvider> _mockDateTimeProvider;

	public AuctionServiceTests()
	{
		_mockLogger = new Mock<ILogger>();
		_configProvider = new ConfigProvider(_mockLogger.Object);
		_mockConfigProvider = new Mock<IConfigProvider>();
		_mockDateTimeProvider = new Mock<IDateTimeProvider>();
		_mockDateTimeProvider.Setup(x => x.Now)
			.Returns(new DateTime(2023, 1, 1));
		_validator = new AuctionValidator(_mockDateTimeProvider.Object);
		_mockDataAccess = new Mock<IAuctionDataAccess>();
		_mockValidator = new Mock<IValidator<Auction>>();
	}

	public static Auction GetValidAuction()
	{
		return new Auction()
		{
			Id = 1,
			OwnerId = 1,
			ProductId = 1,
			CurrencyId = 1,
			//BuyerId = 1,
			Description = "Description",
			StartDate = new DateTime(2023, 1, 1),
			EndDate = new DateTime(2023, 1, 2),
			StartingPrice = 10,
			//CurrentPrice = 15,
			IsActive = true
		};
	}

	public static Auction GetValidUpdateAuction(int? buyerId = 2, double? currentPrice = 15.0)
	{
		var auction = GetValidAuction();
		auction.BuyerId = buyerId;
		auction.CurrentPrice = currentPrice;
		return auction;
	}

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

		var service = new AuctionService(_mockDataAccess.Object, _mockValidator.Object, _mockConfigProvider.Object);

		var result = service.Add(data);

		result.Should().Be(1);
		_mockValidator.Verify(x => x.Validate(data));
		_mockDataAccess.Verify(x => x.Add(data));
		_mockDataAccess.Verify(x => x.GetAllActiveByOwnerId(It.IsAny<int>()));
		_mockConfigProvider.Verify(x => x.MaxAuctions);
	}

	[Fact]
	public void Add_ValidatorIsCalled_Fails()
	{
		var data = new Auction();

		_mockValidator.Setup(x => x.Validate(data))
			.Returns(ValidationUtils.FailingValidationResult);
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);

		var service = new AuctionService(_mockDataAccess.Object, _mockValidator.Object, _mockConfigProvider.Object);

		var result = service.Add(data);

		result.Should().Be(default);
		_mockValidator.Verify(x => x.Validate(data));
		_mockDataAccess.Verify(x => x.Add(data), Times.Never());
	}

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

		var service = new AuctionService(_mockDataAccess.Object, _validator, _configProvider);

		var result = service.Add(data);

		result.Should().Be(id);
		_mockDataAccess.Verify(x => x.Add(data));
		_mockDataAccess.Verify(x => x.GetAllActiveByOwnerId(It.IsAny<int>()));
	}

	[Fact]
	public void Add_InvalidAuction_ValidationFails_ReturnsDefault()
	{
		var data = new Auction();
		_mockDataAccess.Setup(x => x.Add(data))
			.Returns(1);

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object);

		var result = service.Add(data);

		result.Should().Be(default);
		_mockDataAccess.Verify(x => x.Add(data), Times.Never());
	}

	private static IEnumerable<object[]> GetAuctionById()
	{
		yield return new object[] { 1, GetValidAuction() };
		var data = GetValidAuction();
		data.Id = 10;
		yield return new object[] { 10, data };
		yield return new object[] { 20, null };
	}

	[ComplexTheory]
	[MemberData(nameof(GetAuctionById))]
	public void GetById_ProvidedId_ReturnsExpected(int id, Auction? data)
	{
		_mockDataAccess.Setup(x => x.Get(id))
			.Returns(data);

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object);

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

	[ComplexTheory]
	[MemberData(nameof(UpdateAuctions))]
	public void Update_ProvidedAuction_ReturnsProvided(bool status, Times times, Auction data)
	{
		_mockDataAccess.Setup(x => x.Update(data))
			.Returns(status);

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object);

		var result = service.Update(data);

		result.Should().Be(status);
		_mockDataAccess.Verify(x => x.Update(data), times);
	}

	[Fact]
	public void Update_ValidatePrice_ValidAuction()
	{
		var data = GetValidUpdateAuction();

		_mockDataAccess.Setup(x => x.Update(data))
			.Returns(true);
		_mockDataAccess.Setup(x => x.Get(data.Id))
			.Returns(GetValidAuction());

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object);

		var result = service.Update(data);

		result.Should().Be(true);
		_mockDataAccess.Verify(x => x.Update(data));
		_mockDataAccess.Verify(x => x.Get(data.Id));
	}

	[Fact]
	public void Update_ValidatePrice_EarlyExit()
	{
		var data = GetValidAuction();

		_mockDataAccess.Setup(x => x.Update(data))
			.Returns(true);

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object);

		var result = service.Update(data);

		result.Should().Be(true);
		_mockDataAccess.Verify(x => x.Update(data));
		_mockDataAccess.Verify(x => x.Get(data.Id), Times.Never());
	}

	private static IEnumerable<object[]> Update_ValidatePrice_InvalidAuctions()
	{
		yield return new object[] { GetValidUpdateAuction(), null, Times.Once() };
		yield return new object[] { GetValidUpdateAuction(buyerId: null), null, Times.Never() };
		yield return new object[] { GetValidUpdateAuction(currentPrice: null), null, Times.Never() };
		yield return new object[] { GetValidUpdateAuction(), new Auction() { CurrentPrice = 15.0 }, Times.Once() };
		yield return new object[] { GetValidUpdateAuction(), new Auction() { CurrentPrice = 1.0 }, Times.Once() };
	}

	[ComplexTheory]
	[MemberData(nameof(Update_ValidatePrice_InvalidAuctions))]
	public void Update_ValidatePrice_Invalid(Auction data, Auction previous, Times getTimes)
	{
		_mockDataAccess.Setup(x => x.Update(data))
			.Returns(true);
		_mockDataAccess.Setup(x => x.Get(data.Id))
			.Returns(previous);

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object);

		var result = service.Update(data);

		result.Should().Be(false);
		_mockDataAccess.Verify(x => x.Update(data), Times.Never());
		_mockDataAccess.Verify(x => x.Get(data.Id), getTimes);
	}

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	public void Delete_AnyId_ReturnsExpected(bool status)
	{
		_mockDataAccess.Setup(x => x.Delete(It.IsAny<int>()))
			.Returns(status);

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object);

		var result = service.Delete(1);

		result.Should().Be(status);
		_mockDataAccess.Verify(x => x.Delete(It.IsAny<int>()));
	}

	private static IEnumerable<object[]> GetAll()
	{
		yield return new object[] { new List<Auction>() };
		yield return new object[] { new List<Auction>() {
				GetValidAuction(),
				GetValidAuction()
			}
		};
	}

	[ComplexTheory]
	[MemberData(nameof(GetAll))]
	public void GetAll_ProvidedInput_ReturnsProvided(List<Auction> data)
	{
		_mockDataAccess.Setup(x => x.GetAll())
			.Returns(data);

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object);

		var result = service.GetAll();

		result.Should().BeEquivalentTo(data);
		_mockDataAccess.Verify(x => x.GetAll());
	}

	[ComplexTheory]
	[MemberData(nameof(GetAll))]
	public void GetAllActive_ProvidedInput_ReturnsProvided(List<Auction> data)
	{
		_mockDataAccess.Setup(x => x.GetAllActive())
			.Returns(data);

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object);

		var result = service.GetAllActive();

		result.Should().BeEquivalentTo(data);
		_mockDataAccess.Verify(x => x.GetAllActive());
	}

	[ComplexTheory]
	[MemberData(nameof(GetAll))]
	public void GetAllActiveByOwnerId_ProvidedInput_ReturnsProvided(List<Auction> data)
	{
		_mockDataAccess.Setup(x => x.GetAllActiveByOwnerId(It.IsAny<int>()))
			.Returns(data);

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object);

		var result = service.GetAllActiveByOwnerId(1);

		result.Should().BeEquivalentTo(data);
		_mockDataAccess.Verify(x => x.GetAllActiveByOwnerId(It.IsAny<int>()));
	}

	private static IEnumerable<object[]> ValidateLevenshteinDistanceData()
	{
		yield return new object[] { true, GetValidAuction(), new List<Auction>() };
		yield return new object[] { false, GetValidAuction(), new List<Auction>() { GetValidAuction() } };
	}

	[ComplexTheory]
	[MemberData(nameof(ValidateLevenshteinDistanceData))]
	public void ValidateLevenshteinDistance(bool status, Auction auction, List<Auction> auctions)
	{
		_mockDataAccess.Setup(x => x.GetAllActiveByOwnerId(auction.OwnerId))
			.Returns(auctions);

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object);

		var result = service.ValidateLevenshteinDistance(auction);

		result.Should().Be(status);
		_mockDataAccess.Verify(x => x.GetAllActiveByOwnerId(auction.OwnerId));
	}

	[Fact]
	public void Add_ValidAuction_FailsLevenshteinDistance()
	{
		var data = GetValidAuction();

		_mockDataAccess.Setup(x => x.GetAllActiveByOwnerId(data.OwnerId))
			.Returns(new List<Auction>() { GetValidAuction() });

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object);

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

	[ComplexTheory]
	[MemberData(nameof(ValidateMaxAuctionsData))]
	public void ValidateMaxAuctions(bool status, List<Auction> auctions)
	{
		var data = GetValidAuction();

		_mockDataAccess.Setup(x => x.GetAllActiveByOwnerId(data.OwnerId))
			.Returns(auctions);
		_mockConfigProvider.Setup(x => x.MaxAuctions)
			.Returns(2);

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object);

		var result = service.ValidateMaxAuctions(data);

		result.Should().Be(status);
		_mockDataAccess.Verify(x => x.GetAllActiveByOwnerId(data.OwnerId));
		_mockConfigProvider.Verify(x => x.MaxAuctions);
	}

	[Fact]
	public void Add_ValidAuction_FailsMaxAuctions()
	{
		var data = GetValidAuction();

		_mockDataAccess.Setup(x => x.GetAllActiveByOwnerId(data.OwnerId))
			.Returns(new List<Auction>() { new Auction() });
		_mockConfigProvider.Setup(x => x.MaxAuctions)
			.Returns(1);

		var service = new AuctionService(_mockDataAccess.Object, _validator, _mockConfigProvider.Object);

		var result = service.Add(data);

		result.Should().Be(default);
		_mockDataAccess.Verify(x => x.GetAllActiveByOwnerId(data.OwnerId));
		_mockDataAccess.Verify(x => x.Add(data), Times.Never());
		_mockConfigProvider.Verify(x => x.MaxAuctions);
	}
}

using ASSE.Core.Services;
using FluentAssertions;
using Moq;
using Serilog;

namespace ASSE.Core.Tests;
public class ConfigProviderTests
{
	private readonly IConfigProvider _configProvider;
	private readonly IMock<ILogger> _mockLogger;

	public ConfigProviderTests()
	{
		_mockLogger = new Mock<ILogger>();
		_configProvider = new ConfigProvider(_mockLogger.Object);
	}

	[Fact]
	public void GetConnectionString_EmptyString_ReturnsNull()
	{
		var key = string.Empty;

		var result = _configProvider.GetConnectionString(key);

		result.Should().BeNull();
	}

	[Fact]
	public void GetConnectionString_PostgresConnection_ReturnsNotNull()
	{
		var key = "PostgresConnection";

		var result = _configProvider.GetConnectionString(key);

		result.Should().NotBeNull();
	}

	[Fact]
	public void PostgresConnectionString_ReturnsNotNull()
	{
		var result = _configProvider.PostgresConnectionString;

		result.Should().NotBeNull();
	}

	[Theory]
	[InlineData("", null)]
	[InlineData("MaxAuctions", "3")]
	[InlineData("InitialScore", "500")]
	public void GetValue_ProvidedKey_ReturnsExpected(string key, string? expected)
	{
		var result = _configProvider.GetValue(key);

		result.Should().Be(expected);
	}

	[Fact]
	public void GetValueInt_MaxAuctions_ReturnsExpected()
	{
		var key = "MaxAuctions";

		var result = _configProvider.GetValue<int>(key);

		result.Should().Be(3);
	}

	[Fact]
	public void GetValueDouble_InitialScore_ReturnsExpected()
	{
		var key = "InitialScore";

		var result = _configProvider.GetValue<double>(key);

		result.Should().Be(500.0);
	}

	[Fact]
	public void GetValueT_NotFound_ReturnsNull()
	{
		var key = string.Empty;

		var result = _configProvider.GetValue<int>(key);

		result.Should().BeNull();
	}

	[Fact]
	public void GetValueT_CantConvert_ReturnsNull()
	{
		var key = "TestValue";

		var result = _configProvider.GetValue<int>(key);

		result.Should().BeNull();
	}

	[Fact]
	public void MaxAuctionsProperty_ReturnsExpected()
	{
		var result = _configProvider.MaxAuctions;

		result.Should().Be(3);
	}

	[Fact]
	public void InitialScoreProperty_ReturnsExpected()
	{
		var result = _configProvider.InitialScore;

		result.Should().Be(500.0);
	}
}

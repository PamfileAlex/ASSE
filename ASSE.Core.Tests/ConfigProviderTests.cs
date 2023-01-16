// --------------------------------------------------------------------------------------
// <copyright file="ConfigProviderTests.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.Core.Services;
using FluentAssertions;
using Moq;
using Serilog;

namespace ASSE.Core.Tests;

/// <summary>
/// Class to test <see cref="IConfigProvider"/>.
/// </summary>
public class ConfigProviderTests
{
	private readonly IConfigProvider _configProvider;
	private readonly IMock<ILogger> _mockLogger;

	/// <summary>
	/// Initializes a new instance of the <see cref="ConfigProviderTests"/> class.
	/// </summary>
	public ConfigProviderTests()
	{
		_mockLogger = new Mock<ILogger>();
		_configProvider = new ConfigProvider(_mockLogger.Object);
	}

	/// <summary>
	/// Test for <see cref="IConfigProvider.GetConnectionString(string)"/> with empty string as key.
	/// </summary>
	[Fact]
	public void GetConnectionString_EmptyString_ReturnsNull()
	{
		var key = string.Empty;

		var result = _configProvider.GetConnectionString(key);

		result.Should().BeNull();
	}

	/// <summary>
	/// Test for <see cref="IConfigProvider.GetConnectionString(string)"/> with PostgresConnection as key.
	/// </summary>
	[Fact]
	public void GetConnectionString_PostgresConnection_ReturnsNotNull()
	{
		var key = "PostgresConnection";

		var result = _configProvider.GetConnectionString(key);

		result.Should().NotBeNull();
	}

	/// <summary>
	/// Test for <see cref="IConfigProvider.PostgresConnectionString"/>.
	/// </summary>
	[Fact]
	public void PostgresConnectionString_ReturnsNotNull()
	{
		var result = _configProvider.PostgresConnectionString;

		result.Should().NotBeNull();
	}

	/// <summary>
	/// Test for <see cref="IConfigProvider.GetValue(string)"/>.
	/// </summary>
	/// <param name="key">Key in config file.</param>
	/// <param name="expected">Expected value from config file.</param>
	[Theory]
	[InlineData("", null)]
	[InlineData("MaxAuctions", "3")]
	[InlineData("InitialScore", "500")]
	public void GetValue_ProvidedKey_ReturnsExpected(string key, string? expected)
	{
		var result = _configProvider.GetValue(key);

		result.Should().Be(expected);
	}

	/// <summary>
	/// Test for <see cref="IConfigProvider.MaxAuctions"/>.
	/// </summary>
	[Fact]
	public void GetValueInt_MaxAuctions_ReturnsExpected()
	{
		var key = "MaxAuctions";

		var result = _configProvider.GetValue<int>(key);

		result.Should().Be(3);
	}

	/// <summary>
	/// Test for <see cref="IConfigProvider.InitialScore"/>.
	/// </summary>
	[Fact]
	public void GetValueDouble_InitialScore_ReturnsExpected()
	{
		var key = "InitialScore";

		var result = _configProvider.GetValue<double>(key);

		result.Should().Be(500.0);
	}

	/// <summary>
	/// Test for <see cref="IConfigProvider.GetValue{T}(string)"/> empty string as key.
	/// </summary>
	[Fact]
	public void GetValueT_NotFound_ReturnsNull()
	{
		var key = string.Empty;

		var result = _configProvider.GetValue<int>(key);

		result.Should().BeNull();
	}

	/// <summary>
	///  Test for <see cref="IConfigProvider.GetValue{T}(string)"/> key not in config file.
	/// </summary>
	[Fact]
	public void GetValueT_CantConvert_ReturnsNull()
	{
		var key = "TestValue";

		var result = _configProvider.GetValue<int>(key);

		result.Should().BeNull();
	}

	/// <summary>
	/// Test that <see cref="IConfigProvider.MaxAuctions"/> returns expected.
	/// </summary>
	[Fact]
	public void MaxAuctionsProperty_ReturnsExpected()
	{
		var result = _configProvider.MaxAuctions;

		result.Should().Be(3);
	}

	/// <summary>
	/// Test that <see cref="IConfigProvider.InitialScore"/> returns expected.
	/// </summary>
	[Fact]
	public void InitialScoreProperty_ReturnsExpected()
	{
		var result = _configProvider.InitialScore;

		result.Should().Be(500.0);
	}
}

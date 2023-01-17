// --------------------------------------------------------------------------------------
// <copyright file="ConfigProvider.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using System.Configuration;
using Serilog;

namespace ASSE.Core.Services;

/// <summary>
/// <see cref="IConfigProvider"/> implementation specific for App.config
/// using <see cref="ConfigurationManager"/>.
/// </summary>
public class ConfigProvider : IConfigProvider
{
	private readonly ILogger _logger;

	/// <summary>
	/// Initializes a new instance of the <see cref="ConfigProvider"/> class.
	/// </summary>
	/// <param name="logger">Serilog <see cref="ILogger"/> instance.</param>
	public ConfigProvider(ILogger logger)
	{
		_logger = logger;
	}

	/// <inheritdoc/>
	public string? PostgresConnectionString => GetConnectionString("PostgresConnection");

	/// <inheritdoc/>
	public int MaxAuctions => GetValue<int>("MaxAuctions") ?? default;

	/// <inheritdoc/>
	public double InitialScore => GetValue<double>("InitialScore") ?? default;

	/// <inheritdoc/>
	public int NumberOfDays => GetValue<int>("NumberOfDays") ?? default;

	/// <inheritdoc/>
	public double MinimumScore => GetValue<double>("MinimumScore") ?? default;

	/// <inheritdoc/>
	public string? GetConnectionString(string key)
	{
		var conn = ConfigurationManager.ConnectionStrings[key];
		if (conn is null)
		{
			return null;
		}

		return conn.ConnectionString;
	}

	/// <inheritdoc/>
	public string? GetValue(string key)
	{
		_logger.Debug("Attempting to get value from config file with key {key}", key);
		return ConfigurationManager.AppSettings.Get(key);
	}

	/// <inheritdoc/>
	public T? GetValue<T>(string key)
		where T : struct
	{
		string? value = GetValue(key);
		if (value is null)
		{
			return null;
		}

		try
		{
			return (T)Convert.ChangeType(value, typeof(T));
		}
		catch
		{
			_logger.Warning("Failed to get value from config file with key {key}", key);
			return null;
		}
	}
}

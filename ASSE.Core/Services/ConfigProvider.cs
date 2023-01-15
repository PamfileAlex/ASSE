using System.Configuration;
using Serilog;

namespace ASSE.Core.Services;
public class ConfigProvider : IConfigProvider
{
	private readonly ILogger _logger;

	public string? PostgresConnectionString => GetConnectionString("PostgresConnection");
	public int MaxAuctions => GetValue<int>("MaxAuctions");
	public double InitialScore => GetValue<double>("InitialScore");

	public ConfigProvider(ILogger logger)
	{
		_logger = logger;
	}

	public string? GetConnectionString(string key)
	{
		var conn = ConfigurationManager.ConnectionStrings[key];
		if (conn is null)
		{
			return null;
		}
		return conn.ConnectionString;
	}

	public string? GetValue(string key)
	{
		_logger.Debug("Attempting to get value from config file with key {key}", key);
		return ConfigurationManager.AppSettings.Get(key);
	}

	public T GetValue<T>(string key) where T : struct
	{
		try
		{
			string value = GetValue(key);
			return (T)Convert.ChangeType(value, typeof(T));
		}
		catch
		{
			_logger.Warning("Failed to get value from config file with key {key}", key);
			return default;
		}
	}
}

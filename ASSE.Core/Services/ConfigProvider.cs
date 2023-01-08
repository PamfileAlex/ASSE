using System.Configuration;

namespace ASSE.Core.Services;
public class ConfigProvider : IConfigProvider
{
	public string PostgresConnectionString => ConfigurationManager.ConnectionStrings["SQLiteConnection"].ConnectionString;
	public int MaxAuctions => GetValue<int>("MaxAuctions");
	public double InitialScore => GetValue<double>("InitialScore");

	public string? GetValue(string key)
	{
		return ConfigurationManager.AppSettings.Get(key);
	}

	public T GetValue<T>(string key) where T : struct
	{
		try
		{
			string value = ConfigurationManager.AppSettings.Get(key);
			return (T)Convert.ChangeType(value, typeof(T));
		}
		catch
		{
			return default;
		}
	}
}

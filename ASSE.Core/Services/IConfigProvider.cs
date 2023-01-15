namespace ASSE.Core.Services;
public interface IConfigProvider
{
	string? PostgresConnectionString { get; }
	int MaxAuctions { get; }
	double InitialScore { get; }
	string? GetConnectionString(string key);
	string? GetValue(string key);
	T GetValue<T>(string key) where T : struct;
}

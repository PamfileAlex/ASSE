// --------------------------------------------------------------------------------------
// <copyright file="IConfigProvider.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using System.Data;

namespace ASSE.Core.Services;

/// <summary>
/// Represents a provider for different types of configuration files.
/// Implemented by specific configuration providers.
/// </summary>
public interface IConfigProvider
{
	/// <summary>
	/// Gets postgres connection string from the config file.
	/// </summary>
	string? PostgresConnectionString { get; }

	/// <summary>
	/// Gets number of maximum permitted auctions for a given user from the config file.
	/// </summary>
	int MaxAuctions { get; }

	/// <summary>
	/// Gets the initial score for users from the config file.
	/// </summary>
	double InitialScore { get; }

	/// <summary>
	/// Gets the connection tring for the database used by <see cref="IDbConnection"/>.
	/// </summary>
	/// <param name="key">The value for key used to search for the connection string.</param>
	/// <returns>The connection string for given key if key is valid, otherwise <see langword="null"/>.</returns>
	string? GetConnectionString(string key);

	/// <summary>
	/// Gets a value as string from the configuration file.
	/// </summary>
	/// <param name="key">The value for key used to search.</param>
	/// <returns>The value as string if key is valid, otherwise <see langword="null"/>.</returns>
	string? GetValue(string key);

	/// <summary>
	/// Gets a value as <typeparamref name="T"/> from the configuration file.
	/// </summary>
	/// <typeparam name="T">The type to try to convert string value to.</typeparam>
	/// <param name="key">The value for key used to search.</param>
	/// <returns>The value as <typeparamref name="T"/> if key is valid and conversion can be made, otherwise <see langword="null"/>.</returns>
	T? GetValue<T>(string key)
		where T : struct;
}

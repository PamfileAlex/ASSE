// --------------------------------------------------------------------------------------
// <copyright file="IDbConnectionProvider.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using System.Data;
using ASSE.Core.Services;

namespace ASSE.DataMapper.Services;

/// <summary>
/// Represents a provider for different types of database connections.
/// Implemented by specific connection providers for different types of databases.
/// </summary>
public interface IDbConnectionProvider : IService
{
	/// <summary>
	/// Returns a new <see cref="IDbConnection"/> based on database.
	/// </summary>
	/// <returns>new <see cref="IDbConnection"/> instance.</returns>
	IDbConnection GetNewConnection();
}

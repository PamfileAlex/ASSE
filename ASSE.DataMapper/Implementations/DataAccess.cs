// --------------------------------------------------------------------------------------
// <copyright file="DataAccess.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.DataMapper.Interfaces;
using ASSE.DataMapper.Services;
using Serilog;

namespace ASSE.DataMapper.Implementations;

/// <summary>
/// Abstract implementation of <see cref="IDataAccess"/>.
/// </summary>
public abstract class DataAccess : IDataAccess
{
	/// <summary>
	/// Initializes a new instance of the <see cref="DataAccess"/> class.
	/// </summary>
	/// <param name="dbConnectionProvider">Database connection provider instance.</param>
	/// <param name="logger">Serilog <see cref="ILogger"/> instance.</param>
	protected DataAccess(IDbConnectionProvider dbConnectionProvider, ILogger logger)
	{
		DbConnectionProvider = dbConnectionProvider;
		Logger = logger;
	}

	/// <summary>
	/// Gets database connection provider.
	/// </summary>
	protected IDbConnectionProvider DbConnectionProvider { get; }

	/// <summary>
	/// Gets serilog logger.
	/// </summary>
	protected ILogger Logger { get; }
}
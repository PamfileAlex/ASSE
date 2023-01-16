// --------------------------------------------------------------------------------------
// <copyright file="AuctionDataAccess.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using System.Data;
using ASSE.DataMapper.Interfaces;
using ASSE.DataMapper.Services;
using ASSE.DomainModel.Models;
using Dapper;
using Serilog;

namespace ASSE.DataMapper.Implementations;

/// <summary>
/// Implementation of data access for <see cref="Auction"/>.
/// </summary>
public class AuctionDataAccess : DataAccess<Auction>, IAuctionDataAccess
{
	/// <summary>
	/// Initializes a new instance of the <see cref="AuctionDataAccess"/> class.
	/// </summary>
	/// <param name="dbConnectionProvider">Database connection provider instance.</param>
	/// <param name="logger">Serilog <see cref="ILogger"/> instance.</param>
	public AuctionDataAccess(IDbConnectionProvider dbConnectionProvider, ILogger logger)
		: base(dbConnectionProvider, logger)
	{
	}

	/// <inheritdoc/>
	public List<Auction> GetAllActive()
	{
		using IDbConnection connection = _dbConnectionProvider.GetNewConnection();
		connection.Open();
		return GetAllActive(connection);
	}

	/// <inheritdoc/>
	public List<Auction> GetAllActive(IDbConnection connection, IDbTransaction? transaction = null)
	{
		_logger.Debug("Getting all active");

		string sql = @"SELECT * FROM Auctions
							WHERE IsActive=TRUE";

		return connection.Query<Auction>(sql, transaction: transaction).AsList();
	}

	/// <inheritdoc/>
	public List<Auction> GetAllActiveByOwnerId(int ownerId)
	{
		using IDbConnection connection = _dbConnectionProvider.GetNewConnection();
		connection.Open();
		return GetAllActiveByOwnerId(ownerId, connection);
	}

	/// <inheritdoc/>
	public List<Auction> GetAllActiveByOwnerId(int ownerId, IDbConnection connection, IDbTransaction? transaction = null)
	{
		_logger.Debug("Getting all active by ownerId: {ownerId}", ownerId);

		string sql = @"SELECT * FROM Auctions
							WHERE IsActive=TRUE
							AND OwnerId=@ownerId";

		return connection.Query<Auction>(sql, new { ownerId }, transaction).AsList();
	}
}

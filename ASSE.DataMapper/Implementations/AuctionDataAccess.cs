﻿using System.Data;
using ASSE.DataMapper.Interfaces;
using ASSE.DataMapper.Services;
using ASSE.DomainModel.Models;
using Dapper;

namespace ASSE.DataMapper.Implementations;
public class AuctionDataAccess : DataAccess<Auction>, IAuctionDataAccess
{
	public AuctionDataAccess(IDbConnectionProvider dbConnectionProvider)
		: base(dbConnectionProvider)
	{
	}

	public List<Auction> GetAllActive()
	{
		using IDbConnection connection = _dbConnectionProvider.GetNewConnection();
		connection.Open();
		return GetAllActive(connection);
	}

	public List<Auction> GetAllActive(IDbConnection connection, IDbTransaction? transaction = null)
	{
		string sql = @"SELECT * FROM Auctions
							WHERE IsActive=TRUE";

		return connection.Query<Auction>(sql, transaction: transaction).AsList();
	}

	public List<Auction> GetAllActiveByOwnerId(int ownerId)
	{
		using IDbConnection connection = _dbConnectionProvider.GetNewConnection();
		connection.Open();
		return GetAllActiveByOwnerId(ownerId, connection);
	}

	public List<Auction> GetAllActiveByOwnerId(int ownerId, IDbConnection connection, IDbTransaction? transaction = null)
	{
		string sql = @"SELECT * FROM Auctions
							WHERE IsActive=TRUE
							AND OwnerId=@ownerId";

		return connection.Query<Auction>(sql, new { ownerId }, transaction).AsList();
	}
}

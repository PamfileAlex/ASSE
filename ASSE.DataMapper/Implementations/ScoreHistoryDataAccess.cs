// --------------------------------------------------------------------------------------
// <copyright file="ScoreHistoryDataAccess.cs" company="Transilvania University of Brasov">
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
/// Implementation of data access for <see cref="ScoreHistory"/>.
/// </summary>
public class ScoreHistoryDataAccess : DataAccess<ScoreHistory>, IScoreHistoryDataAccess
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ScoreHistoryDataAccess"/> class.
	/// </summary>
	/// <param name="dbConnectionProvider">Database connection provider instance.</param>
	/// <param name="logger">Serilog <see cref="ILogger"/> instance.</param>
	public ScoreHistoryDataAccess(IDbConnectionProvider dbConnectionProvider, ILogger logger)
		: base(dbConnectionProvider, logger)
	{
	}

	/// <inheritdoc/>
	public List<ScoreHistory> GetAllByUserId(int userId)
	{
		using IDbConnection connection = DbConnectionProvider.GetNewConnection();
		connection.Open();
		return GetAllByUserId(userId, connection);
	}

	/// <inheritdoc/>
	public List<ScoreHistory> GetAllByUserId(int userId, IDbConnection connection, IDbTransaction? transaction = null)
	{
		Logger.Debug("Getting all score history for userId: {userId}", userId);

		string sql = @"SELECT * FROM ScoreHistories
							WHERE UserId=@userId";

		return connection.Query<ScoreHistory>(sql, new { userId }, transaction).AsList();
	}
}

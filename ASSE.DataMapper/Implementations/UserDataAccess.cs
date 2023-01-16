// --------------------------------------------------------------------------------------
// <copyright file="UserDataAccess.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.DataMapper.Interfaces;
using ASSE.DataMapper.Services;
using ASSE.DomainModel.Models;
using Serilog;

namespace ASSE.DataMapper.Implementations;

/// <summary>
/// Implementation of data access for <see cref="User"/>.
/// </summary>
public class UserDataAccess : DataAccess<User>, IUserDataAccess
{
	/// <summary>
	/// Initializes a new instance of the <see cref="UserDataAccess"/> class.
	/// </summary>
	/// <param name="dbConnectionProvider">Database connection provider instance.</param>
	/// <param name="logger">Serilog <see cref="ILogger"/> instance.</param>
	public UserDataAccess(IDbConnectionProvider dbConnectionProvider, ILogger logger)
		: base(dbConnectionProvider, logger)
	{
	}
}

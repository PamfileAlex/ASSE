// --------------------------------------------------------------------------------------
// <copyright file="RoleDataAccess.cs" company="Transilvania University of Brasov">
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
/// Implementation of data access for <see cref="Role"/>.
/// </summary>
public class RoleDataAccess : DataAccess<Role>, IRoleDataAccess
{
	/// <summary>
	/// Initializes a new instance of the <see cref="RoleDataAccess"/> class.
	/// </summary>
	/// <param name="dbConnectionProvider">Database connection provider instance.</param>
	/// <param name="logger">Serilog <see cref="ILogger"/> instance.</param>
	public RoleDataAccess(IDbConnectionProvider dbConnectionProvider, ILogger logger)
		: base(dbConnectionProvider, logger)
	{
	}
}

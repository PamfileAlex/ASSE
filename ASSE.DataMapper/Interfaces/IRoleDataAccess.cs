// --------------------------------------------------------------------------------------
// <copyright file="IRoleDataAccess.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.DomainModel.Models;

namespace ASSE.DataMapper.Interfaces;

/// <summary>
/// Represents data access for <see cref="Role"/>.
/// </summary>
public interface IRoleDataAccess : IDataAccess<Role>
{
}

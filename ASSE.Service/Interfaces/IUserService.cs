// --------------------------------------------------------------------------------------
// <copyright file="IUserService.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.DomainModel.Models;

namespace ASSE.Service.Interfaces;

/// <summary>
/// Represents service for <see cref="User"/>.
/// </summary>
public interface IUserService : IEntityService<User>
{
}

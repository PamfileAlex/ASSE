// --------------------------------------------------------------------------------------
// <copyright file="ICurrencyService.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.DomainModel.Models;

namespace ASSE.Service.Interfaces;

/// <summary>
/// Represents service for <see cref="Currency"/>.
/// </summary>
public interface ICurrencyService : IEntityService<Currency>
{
}

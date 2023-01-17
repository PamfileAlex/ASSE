﻿// --------------------------------------------------------------------------------------
// <copyright file="IProductService.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.DomainModel.Models;

namespace ASSE.Service.Interfaces;

/// <summary>
/// Represents service for <see cref="Product"/>.
/// </summary>
public interface IProductService : IEntityService<Product>
{
}

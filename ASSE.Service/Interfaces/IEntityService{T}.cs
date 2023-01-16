// --------------------------------------------------------------------------------------
// <copyright file="IEntityService{T}.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.Core.Models;
using ASSE.Core.Services;

namespace ASSE.Service.Interfaces;

/// <summary>
/// Represents entity service for <see cref="IKeyEntity"/>.
/// Extension of <see cref="ICrud{T}"/>.
/// </summary>
/// <typeparam name="T">Type of entity that implements <see cref="IKeyEntity"/>.</typeparam>
public interface IEntityService<T> : IEntityService, ICrud<T>
	where T : class, IKeyEntity, new()
{
}

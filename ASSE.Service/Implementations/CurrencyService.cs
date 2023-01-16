// --------------------------------------------------------------------------------------
// <copyright file="CurrencyService.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.Service.Interfaces;
using FluentValidation;

namespace ASSE.Service.Implementations;

/// <summary>
/// Implementation of service for <see cref="Currency"/>.
/// </summary>
public class CurrencyService : EntityService<Currency, ICurrencyDataAccess>, ICurrencyService
{
	/// <summary>
	/// Initializes a new instance of the <see cref="CurrencyService"/> class.
	/// </summary>
	/// <param name="dataAccess"><see cref="Currency"/> data access.</param>
	/// <param name="validator"><see cref="Currency"/> validator.</param>
	public CurrencyService(ICurrencyDataAccess dataAccess, IValidator<Currency> validator)
		: base(dataAccess, validator)
	{
	}
}

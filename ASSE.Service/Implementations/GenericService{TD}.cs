// --------------------------------------------------------------------------------------
// <copyright file="GenericService{TD}.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.DataMapper.Interfaces;

namespace ASSE.Service.Implementations;

/// <summary>
/// Abstract generic service with <see cref="IDataAccess"/> for entities without defined classes.
/// </summary>
/// <typeparam name="TD">Type for data access that implements <see cref="IDataAccess"/>.</typeparam>
public abstract class GenericService<TD>
	where TD : IDataAccess
{
	/// <summary>
	/// Initializes a new instance of the <see cref="GenericService{TD}"/> class.
	/// </summary>
	/// <param name="dataAccess">Data access that implements <see cref="IDataAccess"/>.</param>
	protected GenericService(TD dataAccess)
	{
		DataAccess = dataAccess;
	}

	/// <summary>
	/// Gets data access for service.
	/// </summary>
	protected TD DataAccess { get; }
}

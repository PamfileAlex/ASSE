// --------------------------------------------------------------------------------------
// <copyright file="DataMapperModule.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using System.Reflection;
using ASSE.DataMapper.Interfaces;
using ASSE.DataMapper.Services;
using Autofac;
using Module = Autofac.Module;

namespace ASSE.DataMapper;

/// <summary>
/// Autofac module for the ASSE.DataMapper project. Used by the Autofac dependency injection engine.
/// <br/>
/// For more information about the Autofac <see cref="Module"/>, see <a href="https://autofac.readthedocs.io/en/latest/configuration/modules.html">here</a>.
/// </summary>
public class DataMapperModule : Module
{
	/// <inheritdoc/>
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterType<PostgresConnectionProvider>().As<IDbConnectionProvider>();

		builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
				.Where(t => t.GetInterfaces().Any(i => i.Equals(typeof(IDataAccess))))
				.AsImplementedInterfaces();
	}
}

// --------------------------------------------------------------------------------------
// <copyright file="ServiceModule.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using System.Reflection;
using ASSE.DataMapper;
using ASSE.DomainModel;
using ASSE.Service.Interfaces;
using Autofac;
using Module = Autofac.Module;

namespace ASSE.Service;

/// <summary>
/// Autofac module for the ASSE.Service project. Used by the Autofac dependency injection engine.
/// <br/>
/// For more information about the Autofac <see cref="Module"/>, see <a href="https://autofac.readthedocs.io/en/latest/configuration/modules.html">here</a>.
/// </summary>
public class ServiceModule : Module
{
	/// <inheritdoc/>
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterModule<DomainModelModule>();
		builder.RegisterModule<DataMapperModule>();

		builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
				.Where(t => t.GetInterfaces().Any(i => i.Equals(typeof(IEntityService))))
				.AsImplementedInterfaces()
				.AsSelf();
	}
}

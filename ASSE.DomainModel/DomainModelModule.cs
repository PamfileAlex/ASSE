using System.Reflection;
using ASSE.Core;
using Autofac;
using FluentValidation;
using Module = Autofac.Module;

namespace ASSE.DomainModel;

/// <summary>
/// Autofac module for the ASSE.DomainModel project. Used by the Autofac dependency injection engine.
/// <br/>
/// For more information about the Autofac <see cref="Module"/>, see <a href="https://autofac.readthedocs.io/en/latest/configuration/modules.html">here</a>.
/// </summary>
public class DomainModelModule : Module
{
	/// <inheritdoc/>
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterModule<CoreModule>();

		builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
				.Where(t => t.GetInterfaces().Any(i => i.Equals(typeof(IValidator))))
				.AsImplementedInterfaces()
				.AsSelf();
	}
}

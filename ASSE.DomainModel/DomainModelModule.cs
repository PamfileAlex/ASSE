using System.Reflection;
using ASSE.Core;
using Autofac;
using FluentValidation;
using Module = Autofac.Module;

namespace ASSE.DomainModel;
public class DomainModelModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterModule<CoreModule>();

		builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
				.Where(t => t.GetInterfaces().Any(i => i.Equals(typeof(IValidator))))
				.AsSelf();
	}
}

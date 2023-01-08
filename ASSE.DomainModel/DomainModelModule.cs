using System.Reflection;
using Autofac;
using FluentValidation;
using Module = Autofac.Module;

namespace ASSE.DomainModel;
public class DomainModelModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
				.Where(t => t.GetInterfaces().Any(i => i.Equals(typeof(IValidator))))
				.AsImplementedInterfaces();
	}
}

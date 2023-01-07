using System.Reflection;
using ASSE.Service.Interfaces;
using Autofac;
using Module = Autofac.Module;

namespace ASSE.Service;
public class ServiceModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
				.Where(t => t.GetInterfaces().Any(i => i.Equals(typeof(IEntityService))))
				.AsImplementedInterfaces();
	}
}

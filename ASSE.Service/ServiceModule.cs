using System.Reflection;
using ASSE.DataMapper;
using ASSE.DomainModel;
using ASSE.Service.Interfaces;
using Autofac;
using Module = Autofac.Module;

namespace ASSE.Service;
public class ServiceModule : Module
{
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

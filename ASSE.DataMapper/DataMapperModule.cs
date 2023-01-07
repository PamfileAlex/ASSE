using ASSE.DataMapper.Interfaces;
using System.Reflection;
using ASSE.DataMapper.Services;
using Autofac;
using Module = Autofac.Module;

namespace ASSE.DataMapper;
public class DataMapperModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterType<PostgresConnectionProvider>().As<IDbConnectionProvider>();

		builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
				.Where(t => t.GetInterfaces().Any(i => i.Equals(typeof(IDataAccess))))
				.AsImplementedInterfaces();
	}
}

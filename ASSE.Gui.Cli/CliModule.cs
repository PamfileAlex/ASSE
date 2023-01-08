using ASSE.Core;
using ASSE.DataMapper;
using ASSE.DataMapper.Services;
using ASSE.DomainModel;
using ASSE.Gui.Cli.Services;
using ASSE.Service;
using Autofac;

namespace ASSE.Gui.Cli;
public class CliModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterModule<CoreModule>();
		builder.RegisterModule<DomainModelModule>();
		builder.RegisterModule<DataMapperModule>();
		builder.RegisterModule<ServiceModule>();

		builder.RegisterType<SQLiteConnectionProvider>().As<IDbConnectionProvider>();
	}
}

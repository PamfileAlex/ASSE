using ASSE.Core.Services;
using Autofac;
using AutofacSerilogIntegration;
using Serilog;

namespace ASSE.Core;
public class CoreModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		Log.Logger = new LoggerConfiguration()
			.WriteTo.Console()
			.WriteTo.File("log.txt")
			.CreateLogger();

		builder.RegisterLogger();
		builder.RegisterType<ConfigProvider>().As<IConfigProvider>();
	}
}

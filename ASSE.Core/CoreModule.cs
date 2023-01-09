using ASSE.Core.Services;
using Autofac;
using AutofacSerilogIntegration;
using Serilog;

namespace ASSE.Core;
public class CoreModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		SetupLogger();

		builder.RegisterLogger(Log.Logger);
		builder.RegisterType<ConfigProvider>().As<IConfigProvider>();
		builder.RegisterType<DateTimeProvider>().As<IDateTimeProvider>();
	}

	public static void SetupLogger()
	{
		Log.Logger = new LoggerConfiguration()
			.MinimumLevel.Verbose()
			.WriteTo.Debug()
			.WriteTo.Console()
			.WriteTo.File("log.txt")
			.CreateLogger();
	}
}

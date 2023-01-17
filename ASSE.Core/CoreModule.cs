// --------------------------------------------------------------------------------------
// <copyright file="CoreModule.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.Core.Services;
using Autofac;
using AutofacSerilogIntegration;
using Serilog;

namespace ASSE.Core;

/// <summary>
/// Autofac module for the ASSE.Core project. Used by the Autofac dependency injection engine.
/// <br/>
/// For more information about the Autofac <see cref="Module"/>, see <a href="https://autofac.readthedocs.io/en/latest/configuration/modules.html">here</a>.
/// </summary>
public class CoreModule : Module
{
	/// <summary>
	/// Setup for Serilog logger.
	/// </summary>
	public static void SetupLogger()
	{
		Log.Logger = new LoggerConfiguration()
			.MinimumLevel.Verbose()
			.WriteTo.Debug()
			.WriteTo.Console()
			.WriteTo.File("log.txt")
			.CreateLogger();
	}

	/// <inheritdoc/>
	protected override void Load(ContainerBuilder builder)
	{
		SetupLogger();

		builder.RegisterLogger(Log.Logger);
		builder.RegisterType<ConfigProvider>().As<IConfigProvider>().AsSelf();
		builder.RegisterType<DateTimeProvider>().As<IDateTimeProvider>().AsSelf();
	}
}

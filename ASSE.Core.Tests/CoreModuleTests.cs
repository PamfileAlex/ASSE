// --------------------------------------------------------------------------------------
// <copyright file="CoreModuleTests.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.Core.Services;
using Autofac;
using Autofac.Builder;
using FluentAssertions;
using FluentAssertions.Autofac;

namespace ASSE.Core.Tests;

/// <summary>
/// Tests for <see cref="CoreModule"/>.
/// </summary>
public class CoreModuleTests
{
	private readonly IContainer _container;

	/// <summary>
	/// Initializes a new instance of the <see cref="CoreModuleTests"/> class.
	/// </summary>
	public CoreModuleTests()
	{
		var builder = new ContainerBuilder();
		builder.RegisterModule<CoreModule>();
		_container = builder.Build(ContainerBuildOptions.IgnoreStartableComponents);
	}

	/// <summary>
	/// Test that all are registered.
	/// </summary>
	[Fact]
	public void AllAreRegistered()
	{
		_container.Should().Have().Registered<ConfigProvider>().AsSelf().AsImplementedInterfaces();
		_container.Should().Have().Registered<DateTimeProvider>().AsSelf().AsImplementedInterfaces();
	}
}

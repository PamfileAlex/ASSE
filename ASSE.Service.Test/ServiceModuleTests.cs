// --------------------------------------------------------------------------------------
// <copyright file="ServiceModuleTests.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.Service.Implementations;
using Autofac;
using Autofac.Builder;
using FluentAssertions;
using FluentAssertions.Autofac;

namespace ASSE.Service.Tests;

/// <summary>
/// Tests for <see cref="ServiceModule"/>.
/// </summary>
public class ServiceModuleTests
{
	private readonly IContainer _container;

	/// <summary>
	/// Initializes a new instance of the <see cref="ServiceModuleTests"/> class.
	/// </summary>
	public ServiceModuleTests()
	{
		var builder = new ContainerBuilder();
		builder.RegisterModule<ServiceModule>();
		_container = builder.Build(ContainerBuildOptions.IgnoreStartableComponents);
	}

	/// <summary>
	/// Test that all services are registered.
	/// </summary>
	[Fact]
	public void AllServicesAreRegistered()
	{
		_container.Should().Have().Registered<AuctionService>().AsSelf().AsImplementedInterfaces();
		_container.Should().Have().Registered<CategoryService>().AsSelf().AsImplementedInterfaces();
		_container.Should().Have().Registered<CurrencyService>().AsSelf().AsImplementedInterfaces();
		_container.Should().Have().Registered<ProductService>().AsSelf().AsImplementedInterfaces();
		_container.Should().Have().Registered<RoleService>().AsSelf().AsImplementedInterfaces();
		_container.Should().Have().Registered<UserService>().AsSelf().AsImplementedInterfaces();
		_container.Should().Have().Registered<UserRoleService>().AsSelf().AsImplementedInterfaces();
	}
}
// --------------------------------------------------------------------------------------
// <copyright file="DomainModelModuleTests.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.DomainModel.Validators;
using Autofac;
using Autofac.Builder;
using FluentAssertions;
using FluentAssertions.Autofac;

namespace ASSE.DomainModel.Tests;

/// <summary>
/// Tests for <see cref="DomainModelModule"/>.
/// </summary>
public class DomainModelModuleTests
{
	private readonly IContainer _container;

	/// <summary>
	/// Initializes a new instance of the <see cref="DomainModelModuleTests"/> class.
	/// </summary>
	public DomainModelModuleTests()
	{
		var builder = new ContainerBuilder();
		builder.RegisterModule<DomainModelModule>();
		_container = builder.Build(ContainerBuildOptions.IgnoreStartableComponents);
	}

	/// <summary>
	/// Test that all validators are registered.
	/// </summary>
	[Fact]
	public void AllValidatorsAreRegistered()
	{
		_container.Should().Have().Registered<AuctionValidator>().AsSelf().AsImplementedInterfaces();
		_container.Should().Have().Registered<CategoryValidator>().AsSelf().AsImplementedInterfaces();
		_container.Should().Have().Registered<CurrencyValidator>().AsSelf().AsImplementedInterfaces();
		_container.Should().Have().Registered<ProductValidator>().AsSelf().AsImplementedInterfaces();
		_container.Should().Have().Registered<RoleValidator>().AsSelf().AsImplementedInterfaces();
		_container.Should().Have().Registered<UserValidator>().AsSelf().AsImplementedInterfaces();
	}
}

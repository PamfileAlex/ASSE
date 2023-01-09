using ASSE.DomainModel.Validators;
using Autofac;
using Autofac.Builder;
using FluentAssertions;
using FluentAssertions.Autofac;

namespace ASSE.DomainModel.Test;
public class DomainModelModuleTests
{
	private readonly IContainer _container;

	public DomainModelModuleTests()
	{
		var builder = new ContainerBuilder();
		builder.RegisterModule<DomainModelModule>();
		_container = builder.Build(ContainerBuildOptions.IgnoreStartableComponents);
	}

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

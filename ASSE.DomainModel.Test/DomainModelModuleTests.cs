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
		_container.Should().Have().Registered<AuctionValidator>().AsSelf();
		_container.Should().Have().Registered<CategoryValidator>().AsSelf();
		_container.Should().Have().Registered<CurrencyValidator>().AsSelf();
		_container.Should().Have().Registered<ProductValidator>().AsSelf();
		_container.Should().Have().Registered<RoleValidator>().AsSelf();
		_container.Should().Have().Registered<UserValidator>().AsSelf();
	}
}

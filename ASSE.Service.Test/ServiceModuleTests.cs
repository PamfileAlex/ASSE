using Autofac.Builder;
using Autofac;
using FluentAssertions;
using FluentAssertions.Autofac;
using ASSE.Service.Implementations;

namespace ASSE.Service.Test;

public class ServiceModuleTests
{
	private readonly IContainer _container;

	public ServiceModuleTests()
	{
		var builder = new ContainerBuilder();
		builder.RegisterModule<ServiceModule>();
		_container = builder.Build(ContainerBuildOptions.IgnoreStartableComponents);
	}

	[Fact]
	public void AllServicesAreRegistered()
	{
		_container.Should().Have().Registered<AuctionService>().AsSelf().AsImplementedInterfaces();
		_container.Should().Have().Registered<CategoryService>().AsSelf().AsImplementedInterfaces();
		_container.Should().Have().Registered<CurrencyService>().AsSelf().AsImplementedInterfaces();
		_container.Should().Have().Registered<ProductService>().AsSelf().AsImplementedInterfaces();
		_container.Should().Have().Registered<RoleService>().AsSelf().AsImplementedInterfaces();
		_container.Should().Have().Registered<UserService>().AsSelf().AsImplementedInterfaces();
	}
}
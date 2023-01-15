using ASSE.Core.Services;
using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.Gui.Cli;
using Autofac;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = new ContainerBuilder();
		builder.RegisterModule<CliModule>();

		IContainer container = builder.Build();
		using ILifetimeScope scope = container.BeginLifetimeScope();

		//var userDataAccess = scope.Resolve<IUserDataAccess>();
		//var result = userDataAccess.Update(new User());
		//Console.WriteLine(result);

		//var config = scope.Resolve<IConfigProvider>();
		//Console.WriteLine(config.MaxAuctions);

		var roleDataAccess = scope.Resolve<IRoleDataAccess>();
		roleDataAccess.Add(new Role() { Name = "Seller" });
		roleDataAccess.Add(new Role() { Name = "Buyer" });
		Console.WriteLine(roleDataAccess.GetAll());
	}
}
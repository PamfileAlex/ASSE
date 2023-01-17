// --------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using Autofac;

namespace ASSE.Gui.Cli;

/// <summary>
/// Command line interface entry point class.
/// </summary>
public class Program
{
	/// <summary>
	/// Command line interface entry point method.
	/// </summary>
	/// <param name="args">Array of arguments.</param>
	public static void Main(string[] args)
	{
		var builder = new ContainerBuilder();
		builder.RegisterModule<CliModule>();

		IContainer container = builder.Build();
		using ILifetimeScope scope = container.BeginLifetimeScope();

		/*var userDataAccess = scope.Resolve<IUserDataAccess>();
		var result = userDataAccess.Update(new User());
		Console.WriteLine(result);

		var config = scope.Resolve<IConfigProvider>();
		Console.WriteLine(config.MaxAuctions);*/

		var roleDataAccess = scope.Resolve<IRoleDataAccess>();
		roleDataAccess.Add(new Role() { Name = "Seller" });
		roleDataAccess.Add(new Role() { Name = "Buyer" });
		Console.WriteLine(roleDataAccess.GetAll());
	}
}
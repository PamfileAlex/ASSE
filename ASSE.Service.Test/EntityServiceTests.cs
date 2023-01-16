// --------------------------------------------------------------------------------------
// <copyright file="EntityServiceTests.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.Core.Models;
using ASSE.DataMapper.Interfaces;
using ASSE.Service.Implementations;
using FluentAssertions;
using FluentValidation;
using Moq;

namespace ASSE.Service.Tests;

/// <summary>
/// Test data entity.
/// </summary>
public class TestData : IKeyEntity
{
	/// <inheritdoc/>
	public int Id { get; set; }
}

/// <summary>
/// Test data entity service.
/// </summary>
public class TestEntityService : EntityService<TestData>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="TestEntityService"/> class.
	/// </summary>
	/// <param name="dataAccess">Test data access.</param>
	/// <param name="validator">Test data validator.</param>
	public TestEntityService(IDataAccess<TestData> dataAccess, IValidator<TestData> validator)
		: base(dataAccess, validator)
	{
	}
}

/// <summary>
/// Tests for <see cref="EntityService{T}"/>.
/// </summary>
public class EntityServiceTests
{
	/// <summary>
	/// Test <see cref="EntityService{T}"/> constructor.
	/// </summary>
	[Fact]
	public void EntityServiceConstructor()
	{
		var dataAccess = new Mock<IDataAccess<TestData>>();
		var validator = new Mock<IValidator<TestData>>();

		Func<TestEntityService> act = () => new TestEntityService(dataAccess.Object, validator.Object);
		var service = act();

		act.Should().NotThrow();
		service.Should().NotBeNull();
	}
}

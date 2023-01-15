using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASSE.Core.Models;
using ASSE.DataMapper.Interfaces;
using ASSE.DomainModel.Models;
using ASSE.Service.Implementations;
using FluentAssertions;
using FluentValidation;
using Moq;

namespace ASSE.Service.Tests;

public class TestData : IKeyEntity
{
	public int Id { get; set; }
}

public class TestEntityService : EntityService<TestData>
{
	public TestEntityService(IDataAccess<TestData> dataAccess, IValidator<TestData> validator)
		: base(dataAccess, validator)
	{
	}
}

public class EntityServiceTests
{
	[Fact]
	public void EntityServiceConstructor()
	{
		var dataAccess = new Mock<IDataAccess<TestData>>();
		var validator = new Mock<IValidator<TestData>>();

		Action act = () => new TestEntityService(dataAccess.Object, validator.Object);

		act.Should().NotThrow();
	}
}

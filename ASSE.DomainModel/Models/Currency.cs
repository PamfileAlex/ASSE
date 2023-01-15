using ASSE.Core.Models;
using Dapper.Contrib.Extensions;

namespace ASSE.DomainModel.Models;
[Table("Currencies")]
public class Currency : IKeyEntity
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
}

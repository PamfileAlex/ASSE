using ASSE.Core.Models;
using Dapper.Contrib.Extensions;

namespace ASSE.DomainModel.Models;
[Table("Categories")]
public class Category : IKeyEntity
{
	public int Id { get; set; }
	public int? ParentId { get; set; }
	public string Name { get; set; } = string.Empty;
}

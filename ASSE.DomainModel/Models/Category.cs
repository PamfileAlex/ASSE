using ASSE.Core.Models;

namespace ASSE.DomainModel.Models;
public class Category : IKeyEntity
{
	public int Id { get; set; }
	public int? ParentId { get; set; }
	public string Name { get; set; } = string.Empty;
}

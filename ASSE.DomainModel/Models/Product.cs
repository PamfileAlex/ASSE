using ASSE.Core.Models;

namespace ASSE.DomainModel.Models;
public class Product : IKeyEntity
{
	public int Id { get; set; }
	public int CategoryId { get; set; }
	public string Name { get; set; } = string.Empty;
}

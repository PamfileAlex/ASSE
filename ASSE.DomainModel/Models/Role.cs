using ASSE.Core.Models;

namespace ASSE.DomainModel.Models;
public class Role : IKeyEntity
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
}

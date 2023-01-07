using ASSE.Core.Models;

namespace ASSE.DomainModel.Models;
public class Currency : IKeyEntity
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
}

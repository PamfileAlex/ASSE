using ASSE.Core.Models;

namespace ASSE.DomainModel.Models;
public class User : IKeyEntity
{
	public int Id { get; set; }
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public double Score { get; set; }
}

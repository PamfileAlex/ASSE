using ASSE.Core.Models;

namespace ASSE.DomainModel.Models;
public class Auction : IKeyEntity
{
	public int Id { get; set; }
	public int OwnerId { get; set; }
	public int ProductId { get; set; }
	public int CurrencyId { get; set; }
	public int? BuyerId { get; set; }
	public string Description { get; set; } = string.Empty;
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public double StartingPrice { get; set; }
	public double? CurrentPrice { get; set; }
	public bool IsActive { get; set; }
}

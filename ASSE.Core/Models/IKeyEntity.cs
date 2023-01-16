namespace ASSE.Core.Models;

/// <summary>
///  Interface that represents an entity with primary key being <see cref="int"/>. Database entity objects should implement this interface.
/// </summary>
public interface IKeyEntity : IEntity
{
	/// <summary>
	/// Gets or sets primary key of the entity.
	/// </summary>
	int Id { get; set; }
}

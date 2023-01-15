namespace ASSE.Core.Services;
public class DateTimeProvider : IDateTimeProvider
{
	/// <inheritdoc/>
	public DateTime Now => DateTime.Now;
}

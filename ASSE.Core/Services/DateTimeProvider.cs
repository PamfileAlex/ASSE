namespace ASSE.Core.Services;

/// <inheritdoc/>
public class DateTimeProvider : IDateTimeProvider
{
	/// <inheritdoc/>
	public DateTime Now => DateTime.Now;
}

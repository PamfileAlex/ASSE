namespace ASSE.Core.Services;
public interface IDateTimeProvider
{
	/// <summary>
	/// Gets a System.DateTime object that is set to the current date and time on this
	/// computer, expressed as the local time.
	/// </summary>
	public DateTime Now { get; }
}

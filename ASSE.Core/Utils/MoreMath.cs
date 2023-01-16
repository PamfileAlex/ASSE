namespace ASSE.Core.Utils;

/// <summary>
/// Additional mathematical constants and static methods.
/// </summary>
public static class MoreMath
{
	/// <summary>
	/// Returns the smaller of the provided 32-bit signed integers.
	/// </summary>
	/// <param name="values">Array of 32-bit signed integers.</param>
	/// <returns>Smallest value from array.</returns>
	public static int Min(params int[] values)
	{
		return values.Min();
	}
}

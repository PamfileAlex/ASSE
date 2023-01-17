// --------------------------------------------------------------------------------------
// <copyright file="MoreMath.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

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

	/// <summary>
	/// Calculates median of numbers.
	/// </summary>
	/// <param name="numbers"><see cref="List{T}"/> of numbers.</param>
	/// <returns>median of numbers.</returns>
	public static double Median(List<double> numbers)
	{
		numbers.Sort();

		int size = numbers.Count;
		int mid = size / 2;
		return (size % 2 != 0) ?
			numbers[mid] :
			(numbers[mid] + numbers[mid - 1]) / 2;
	}
}

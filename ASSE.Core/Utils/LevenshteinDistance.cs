using System.Text.RegularExpressions;

namespace ASSE.Core.Utils;

/// <summary>
/// Static class that works with all of Levenshtein distance.
/// For more information about Levenshtein distance, see <a href="https://en.wikipedia.org/wiki/Levenshtein_distance">wikipedia page</a>.
/// </summary>
public static class LevenshteinDistance
{
	/// <summary>
	/// Constant for minimum accepted difference between two sequences.
	/// </summary>
	public const int AcceptedDistance = 6;

	/// <summary>
	/// Calculates the minimum number of single-character edits (insertions, deletions or substitutions) required to change one sequence into the other.
	/// </summary>
	/// <param name="first">First sequence.</param>
	/// <param name="second">Second sequence.</param>
	/// <returns>The number of edits required to transform one string into the other.
	/// This is at most the length of the longest string, and at least the difference in length between the two strings.</returns>
	public static int Calculate(string first, string second)
	{
		if (string.IsNullOrEmpty(first))
		{
			return second is not null ? second.Length : default;
		}

		if (string.IsNullOrEmpty(second))
		{
			return first is not null ? first.Length : default;
		}

		/*var tailFirst = first[1..];
		var tailSecond = second[1..];

		if (first[0] == second[0])
		{
			return Calculate(tailFirst, tailSecond);
		}

		return 1 + MoreMath.Min(Calculate(tailFirst, second),
							 Calculate(first, tailSecond),
							 Calculate(tailFirst, tailSecond));*/

		int[,] matrix = new int[first.Length + 1, second.Length + 1];

		for (var row = 0; row <= first.Length; matrix[row, 0] = row++)
		{
		}

		for (var column = 0; column <= second.Length; matrix[0, column] = column++)
		{
		}

		for (var row = 1; row <= first.Length; row++)
		{
			for (var column = 1; column <= second.Length; column++)
			{
				var cost = (first[row - 1] == second[column - 1]) ? 0 : 1;

				matrix[row, column] = MoreMath.Min(
					matrix[row - 1, column] + 1,
					matrix[row, column - 1] + 1,
					matrix[row - 1, column - 1] + cost);
			}
		}

		return matrix[first.Length, second.Length];
	}

	/// <summary>
	/// Calculates the Levenshtein distance on normalized strings.
	/// </summary>
	/// <param name="first">First sequence.</param>
	/// <param name="second">Second sequence.</param>
	/// <returns>The number of edits required.</returns>
	public static int CalculateNormalized(string first, string second)
	{
		return Calculate(Normalize(first), Normalize(second));
	}

	/// <summary>
	/// Normalizes the <see cref="string"/> by removing all punctuation marks.
	/// </summary>
	/// <param name="input">Input <see cref="string"/> to be normalized.</param>
	/// <returns>The <see cref="string"/> normalized.</returns>
	public static string Normalize(string input)
	{
		// return new string(input.Where(c => !char.IsPunctuation(c)).ToArray()).ToLower();
		return Regex.Replace(input, "[^a-zA-Z0-9]", string.Empty, RegexOptions.Compiled).ToLower();
	}

	/// <summary>
	/// Determines if the two provided sequences are different enough by comparing the result from the normalized.
	/// Levenshtein distance calculation to the <see cref="AcceptedDistance"/> constant.
	/// </summary>
	/// <param name="first">First sequence.</param>
	/// <param name="second">Second sequence.</param>
	/// <returns><see langword="true"/> if the two sequences are different enough, <see langword="false"/> otherwise.</returns>
	public static bool AreDifferent(string first, string second)
	{
		return CalculateNormalized(first, second) > AcceptedDistance;
	}
}

using System.Text.RegularExpressions;

namespace ASSE.Core.Utils;
public static class LevenshteinDistance
{
	public const int AcceptedDistance = 6;

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

		//var tailFirst = first[1..];
		//var tailSecond = second[1..];

		//if (first[0] == second[0])
		//{
		//	return Calculate(tailFirst, tailSecond);
		//}

		//return 1 + MoreMath.Min(Calculate(tailFirst, second),
		//						Calculate(first, tailSecond),
		//						Calculate(tailFirst, tailSecond));

		int[,] matrix = new int[first.Length + 1, second.Length + 1];

		for (var row = 0; row <= first.Length; matrix[row, 0] = row++) { }
		for (var column = 0; column <= second.Length; matrix[0, column] = column++) { }

		for (var row = 1; row <= first.Length; row++)
		{
			for (var column = 1; column <= second.Length; column++)
			{
				var cost = (first[row - 1] == second[column - 1]) ? 0 : 1;

				matrix[row, column] = MoreMath.Min(matrix[row - 1, column] + 1,
													matrix[row, column - 1] + 1,
													matrix[row - 1, column - 1] + cost);
			}
		}
		return matrix[first.Length, second.Length];
	}

	public static int CalculateNormalized(string first, string second)
	{
		return Calculate(Normalize(first), Normalize(second));
	}

	public static string Normalize(string input)
	{
		//return new string(input.Where(c => !char.IsPunctuation(c)).ToArray()).ToLower();
		return Regex.Replace(input, "[^a-zA-Z0-9]", "", RegexOptions.Compiled).ToLower();
	}

	public static bool AreDifferent(string first, string second)
	{
		return CalculateNormalized(first, second) > AcceptedDistance;
	}
}

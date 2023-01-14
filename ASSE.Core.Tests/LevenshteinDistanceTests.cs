using ASSE.Core.Utils;
using FluentAssertions;

namespace ASSE.Core.Tests;
public class LevenshteinDistanceTests
{
	[Theory]
	[InlineData("", "")]
	[InlineData(",'-.!?", "")]
	[InlineData("This, Is? A. Test!", "thisisatest")]
	public void Normalize(string input, string expected)
	{
		var result = LevenshteinDistance.Normalize(input);

		result.Should().Be(expected);
	}

	[Theory]
	[InlineData("", "", 0)]
	[InlineData("This is a test", "ThisIsATest", 6)]
	public void Calculate(string first, string second, int expected)
	{
		var result = LevenshteinDistance.Calculate(first, second);

		result.Should().Be(expected);
	}

	[Theory]
	[InlineData("", "", 0)]
	[InlineData("This is a test", "ThisIsATest", 0)]
	public void CalculateNormalized(string first, string second, int expected)
	{
		var result = LevenshteinDistance.CalculateNormalized(first, second);

		result.Should().Be(expected);
	}
}

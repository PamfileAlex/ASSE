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
	[InlineData("this is a sentence", "This is another sentence", 7)]
	[InlineData("The quick brown fox jumps over the lazy dog", "The slow yellow fox jumps over the lazy dog", 10)]
	[InlineData("Lorem ipsum dolor sit amet, consectetur adipiscing elit.", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed euismod.", 13)]
	public void Calculate(string first, string second, int expected)
	{
		var result = LevenshteinDistance.Calculate(first, second);

		result.Should().Be(expected);
	}

	[Theory]
	[InlineData("", "", 0)]
	[InlineData("This is a test", "ThisIsATest", 0)]
	[InlineData("kitten", "sitting", 3)]
	[InlineData("this is a sentence", "This is another sentence", 6)]
	[InlineData("Lorem ipsum dolor sit amet, consectetur adipiscing elit.", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed euismod.", 10)]
	public void CalculateNormalized(string first, string second, int expected)
	{
		var result = LevenshteinDistance.CalculateNormalized(first, second);

		result.Should().Be(expected);
	}

	[Theory]
	[InlineData(false, "", "")]
	[InlineData(true, "qwertyuiop", "asdfghjkl")]
	[InlineData(true, "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed euismod.")]
	public void AreDifferent(bool status, string first, string second)
	{
		var result = LevenshteinDistance.AreDifferent(first, second);

		result.Should().Be(status);
	}
}

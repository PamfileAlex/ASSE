namespace ASSE.Core.Extensions;

/// <summary>
/// Extensions methods for primitive types (ex: <see cref="int"/>, <see cref="double"/> etc.)
/// </summary>
public static class PrimitiveTypesExtensions
{
	/// <summary>
	/// Using the <see cref="IConvertible"/> interface, converts the primitive type to <see cref="bool"/>.
	/// </summary>
	/// <typeparam name="T">The type of the primitive object.</typeparam>
	/// <param name="this">The primitive object.</param>
	/// <returns><see cref="bool"/> value of primitive.</returns>
	public static bool ToBoolean<T>(this T @this)
		where T : struct, IConvertible
	{
		return @this.ToBoolean();
	}
}

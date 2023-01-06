namespace ASSE.Core.Extensions;
public static class PrimitiveTypesExtensions
{
	public static bool ToBoolean<T>(this T @this) where T : struct, IConvertible
	{
		return @this.ToBoolean();
	}
}

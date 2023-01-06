using System.Collections.ObjectModel;

namespace ASSE.Core.Extensions;

public static class IDictionaryExtensions
{
	public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> @this)
		where TKey : notnull
	{
		return new ReadOnlyDictionary<TKey, TValue>(@this);
	}
}

using System.Collections.ObjectModel;

namespace ASSE.Core.Extensions;

public static class IEnumerableExtensions
{
	public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
	{
		return source.Select((item, index) => (item, index));
	}
	public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> list)
	{
		return new ObservableCollection<T>(list);
	}

	/// <summary>
	/// Obtains the data as a list; if it is *already* a list, the original object is returned without
	/// any duplication; otherwise, ToList() is invoked.
	/// </summary>
	/// <typeparam name="T">The type of element in the list.</typeparam>
	/// <param name="source">The enumerable to return as a list.</param>
	public static List<T> AsList<T>(this IEnumerable<T> source)
	{
		return (source == null || source is List<T>) ? (List<T>)source : source.ToList();
	}
}

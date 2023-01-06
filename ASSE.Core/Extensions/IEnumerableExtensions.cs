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
}

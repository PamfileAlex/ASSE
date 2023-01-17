// --------------------------------------------------------------------------------------
// <copyright file="IDictionaryExtensions.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using System.Collections.ObjectModel;

namespace ASSE.Core.Extensions;

/// <summary>
/// Extensions methods for <see cref="IDictionary{TKey, TValue}"/>.
/// </summary>
public static class IDictionaryExtensions
{
	/// <summary>
	/// Converts a <see cref="IDictionary{TKey, TValue}"/> instance to <see cref="ReadOnlyDictionary{TKey, TValue}"/> using its constructor.
	/// </summary>
	/// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
	/// <param name="this"><see cref="IDictionary{TKey, TValue}"/> instance.</param>
	/// <returns>A new instance of <see cref="ReadOnlyDictionary{TKey, TValue}"/> that is a wrapper on the <see cref="IDictionary{TKey, TValue}"/> provided object.</returns>
	public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> @this)
		where TKey : notnull
	{
		return new ReadOnlyDictionary<TKey, TValue>(@this);
	}
}

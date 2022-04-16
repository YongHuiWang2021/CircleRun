using System;
using System.Collections.Generic;

namespace XD.Core
{
	public static class DictionaryExtensions
	{
		public static TValue GetValue<TKey, TValue> (this IDictionary<TKey, TValue> dict, TKey key)
		{
			return DictionaryUtil.GetValue (dict, key);
		}

		public static TValue GetOrAddNewValue<TKey, TValue> (this IDictionary<TKey, TValue> dict, TKey key) where TValue : class, new ()
		{
			return DictionaryUtil.GetOrAddNewValue (dict, key);
		}

		public static TValue GetOrAddDefaultValue<TKey, TValue> (this IDictionary<TKey, TValue> dict, TKey key)
		{
			return DictionaryUtil.GetOrAddDefaultValue (dict, key);
		}

		public static void ForEach<TKey, TValue> (this IDictionary<TKey, TValue> dict, Action<TKey, TValue> action)
		{
			DictionaryUtil.ForEach (dict, action);

		}

		public static void ForEach<TKey, TValue> (this IDictionary<TKey, TValue> dict, Action<TValue> action)
		{
			DictionaryUtil.ForEach (dict, action);
		}
	}
}
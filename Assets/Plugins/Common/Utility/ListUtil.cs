using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace XD.Core
{
	public static class ListUtil
	{
		#region Create

		public static List<T> Create<T> (int count, T value)
		{
			var list = new List<T> (count);
			for (var i = 0; i < count; i++) {
				list.Add (value);
			}
			return list;
		}
		
		public static List<T> Clone<T> (this List<T> items)
		{
			var list = new List<T> ();
			for (var i = 0; i < items.Count; ++i) {
				list.Add (items[i]);
			}
			return list;
		}

		public static T[] CreateArray<T> (int count, T value)
		{
			var array = new T[count];
			for (var i = 0; i < count; i++) {
				array[i] = value;
			}
			return array;
		}

		#endregion


		#region Random
		static public int GetRandomSeed()
		{
			byte[] bytes = new byte[4];
			System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
			rng.GetBytes(bytes);
			return BitConverter.ToInt32(bytes, 0);
		}
		public static T GetRandomItem<T> (this IList<T> list)
		{
			if (list == null || list.Count <= 0) {
				return default(T);
			}
			UnityEngine.Random.seed = GetRandomSeed();
			return list[Random.Range (0, list.Count)];
		}

		#endregion
	}
}
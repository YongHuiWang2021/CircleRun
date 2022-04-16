using System;

namespace XD.Core
{
	public static class EnumExtensions
	{
		public static bool ToEnum<TEnum> (this string value, out TEnum result) where TEnum : Enum
		{
			return EnumUtil.ToEnum (value, out result);
		}
		public static float SafeToFlolat (this string value)
		{
			float ret = 0;
			float.TryParse(value, out ret);
			return ret;
		}
		public static int SafeToInt32 (this string value)
		{
			int ret = 0;
			int.TryParse(value, out ret);
			return ret;
		}
		public static long SafeToInt64 (this string value)
		{
			long ret = 0;
			long.TryParse(value, out ret);
			return ret;
		}
		
	}
}
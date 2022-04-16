using System;

namespace XD.Core
{
	public static class EnumUtil
	{
		public static bool ToEnum<TEnum> (string value, out TEnum result) where TEnum : Enum
		{
			var success = true;
			try {
				result = (TEnum)Enum.Parse (typeof(TEnum), value);
			} catch (Exception) {
				result = default;
				success = false;
			}
			return success;
		}
	}
}
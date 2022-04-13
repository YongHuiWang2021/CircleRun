using UnityEngine;
using System.Collections;
using System.Globalization;

namespace LitJson.Extensions {

	public static class Extensions {

		public static void WriteProperty(this JsonWriter w,string name,long value){
			w.WritePropertyName(name);
			w.Write(value);
		}
		
		public static void WriteProperty(this JsonWriter w,string name,string value){
			w.WritePropertyName(name);
			w.Write(value);
		}
		
		public static void WriteProperty(this JsonWriter w,string name,bool value){
			w.WritePropertyName(name);
			w.Write(value);
		}
		
		public static void WriteProperty(this JsonWriter w,string name,double value){
			w.WritePropertyName(name);
			w.Write(value);
		}
		public static bool ToTryDouble(this string pVal, out double pResult)
		{
			return double.TryParse(pVal, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture,
				out pResult);
		}
	}
}
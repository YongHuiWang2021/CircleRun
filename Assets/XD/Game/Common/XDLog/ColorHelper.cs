using UnityEngine;

namespace XD.XDLog
{
    public  class ColorHelper
    {
        public static string ToHtml( Color c)
        {
            return c.ToHtml();
        }

    }
    public static class ColorExt
    {
        public static string ToHtml(this Color c)
        {
            Color32 col = c;
            return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", new object[] { col.r, col.g, col.b, col.a });
        }

        public static string AddHTMLColor(this string LogObject, Color color)
        {
#if !UNITY_EDITOR
        return LogObject;
#endif
            return TextHelper.AddHTMLColor(LogObject,color);
        }
    }
    public  class TextHelper
    {
        public static string AddHTMLColor( string LogObject, Color color)
        {
            string ret = "";
            if (color != Color.white)
                
            {
                ret = "<color="+color.ToHtml() +">" + LogObject.ToString() + "</color>";
          
            }
            else
            {
                ret = LogObject.ToString();
            }
            return ret;
        }
        public static string AddHTMLSizeReductionEtc(string LogObject,int size)
        {
            return "<size=-"+size+">" + LogObject + "</size>";
        }
        public static string AddHTMLSizeAndEtc(string LogObject,int size)
        {
            return "<size=+"+size+">" + LogObject + "</size>";
        }
        // <size=48>Point size 48</size>
        public static string AddHTMLSize(string LogObject,int size)
        {
            return "<bsize"+size+">" + LogObject + "</size>";
        }
        public static string AddHTMLBold(string LogObject)
        {
            return "<b>" + LogObject + "</b>";
        }
        public static string AddHTMLUnderline(string LogObject)
        {
            return "<u>" + LogObject + "</u>";
        }
    }
    
   
}
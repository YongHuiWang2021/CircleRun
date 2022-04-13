using UnityEngine;

namespace Plugins.Package.LogWriterToLocal
{
    public static class LogHelper
    {
        public static string AddHTMLColor(this string LogObject, Color color,string Tag ="Circle")
        {
/*#if !UNITY_EDITOR
        return LogObject;
#endif*/
            return TextHelper.AddHTMLColor(LogObject,color,Tag);
        }
        
    }public  class TextHelper
    {
        public static string AddHTMLColor( string LogObject, Color color,string Tag ="")
        {
            string ret = "";
#if UNITY_EDITOR
            if (color != Color.white)
            {
                ret = Tag +" "+"<color="+color.ToHtml() +">" + LogObject.ToString() + "</color>";
          
            }
            else
#endif
            {
                ret =Tag+" "+ LogObject.ToString();
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
using UnityEngine;

namespace Plugins.Package.LogWriterToLocal
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

      
    }
}
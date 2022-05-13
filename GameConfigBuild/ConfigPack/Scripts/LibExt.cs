using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class LibExt
{
    public static bool IsNullOrEmpty(this string v)
    {
        return string.IsNullOrEmpty(v);
    }
}
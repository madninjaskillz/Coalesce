using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coalesce
{
    public static class Extensions
    {
        public static string ToUIString(this double input)
        {
            string s = input.ToString();
            if (s.Length > 5)
            {
                s = s.Substring(0, 5);
            }

            return s;
        }
    }
}

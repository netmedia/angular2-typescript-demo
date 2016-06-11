using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netmedia.Common.Extensions
{
    public static class DecimalExtensions
    {
        public static int Scale(this decimal value)
        {
            return BitConverter.GetBytes(decimal.GetBits(value)[3])[2];
        }
    }
}

using System;

namespace Netmedia.Common
{
    public static class Calculations
    {
        const int BINARY_UNIT_CONVERSION_VALUE = 1024;
        
        public enum BinaryUnits
        {
            Kilobyte = 1,
            Megabyte = 2,
            Gigabyte = 3
        }
        
        public static double CalculateSquare(Int32 number)
        {
            return Math.Pow(number, 2);
        }


        public static double CalculateCube(Int32 number)
        {
            return Math.Pow(number, 3);
        }


        public static double ConvertBytes(double bytes, BinaryUnits unitType)
        {
            try
            {
                switch (unitType)
                {
                    case BinaryUnits.Kilobyte:
                        return (bytes / BINARY_UNIT_CONVERSION_VALUE);
                    case BinaryUnits.Megabyte:
                        return (bytes / CalculateSquare(BINARY_UNIT_CONVERSION_VALUE));
                    case BinaryUnits.Gigabyte:
                        return (bytes / CalculateCube(BINARY_UNIT_CONVERSION_VALUE));
                    default:
                        return bytes;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static bool IsNumeric(string text)
        {
            int result;
            return int.TryParse(text, out result);
        }

        public static bool IsGuid(string text)
        {
            Guid result;
            return Guid.TryParse(text, out result);
        }
    }
}

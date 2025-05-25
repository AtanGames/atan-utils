using UnityEngine;

namespace AtanUtils.Utils
{
    public static class UtilsA
    {
        public static string FormatBytesAsString(long bytes)
        {
            if (bytes < 0)
                return "-" + FormatBytesAsString(-bytes);
            if (bytes == 0)
                return "0 bytes";

            string[] units = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB" };
            int unitIndex = (int)Mathf.Floor(Mathf.Log(bytes, 1024));
            double adjustedSize = bytes / Mathf.Pow(1024, unitIndex);
    
            // Format the number to have at most two decimal places.
            return string.Format("{0:0.##} {1}", adjustedSize, units[unitIndex]);
        }

        public static string FormatDollarAsString(long dollar)
        {
            return (dollar / 100M).ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
        }
    }
}
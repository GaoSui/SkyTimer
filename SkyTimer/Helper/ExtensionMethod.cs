using SkyTimer.Properties;
using System;

namespace SkyTimer.Helper
{
    public static class ExtensionMethod
    {
        public static string ToStackmatFormat(this int time)
        {
            if (time < 0) return "DNF";
            var span = TimeSpan.FromMilliseconds(time);
            if (Settings.Default.DoublePrecision) return span.ToString("m\\:ss\\.ff");
            return span.ToString("m\\:ss\\.fff");
        }

        public static bool IsOlder(this string current, string latest)
        {
            if (int.Parse(latest[4].ToString()) > int.Parse(current[4].ToString()) ||
                int.Parse(latest[2].ToString()) > int.Parse(current[2].ToString()) ||
                int.Parse(latest[0].ToString()) > int.Parse(current[0].ToString()))
            {
                return true;
            }
            return false;
        }
    }
}

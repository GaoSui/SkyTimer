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
            var cv = current.Split('.');
            var lv = latest.Split('.');
            if (int.Parse(lv[2].ToString()) > int.Parse(cv[2].ToString()) ||
                int.Parse(lv[1].ToString()) > int.Parse(cv[1].ToString()) ||
                int.Parse(lv[0].ToString()) > int.Parse(cv[0].ToString()))
            {
                return true;
            }
            return false;
        }
    }
}

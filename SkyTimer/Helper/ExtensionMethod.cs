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
    }
}

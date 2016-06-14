namespace SkyTimer.Helper
{
    public static class ExtensionMethod
    {
        public static string ToStackmatFormat(this int time)
        {
            if (time < 0) return "DNF";
            var min = time / 60000;
            time %= 60000;
            var sec = time / 1000;
            var ms = time % 1000;
            return string.Format("{0:0}:{1:00}.{2:000}", min, sec, ms);
        }
    }
}

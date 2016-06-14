using System;
using System.Net;

namespace SkyTimer.Utils.Scramble
{
    public class TNoodleScramble : IScrambleService
    {
        private WebClient client = new WebClient();

        public string GetScramble(string type = "333")
        {
            try
            {
                return client.DownloadString($"http://127.0.0.1:2014/scramble/.txt?={type}");
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}

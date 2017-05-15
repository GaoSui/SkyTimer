using System;
using System.Net;
using System.Threading.Tasks;

namespace SkyTimer.Service
{
    public interface IScrambleService
    {
        Task<string> GetScramble(string type = "333");
    }

    public class TNoodleScramble : IScrambleService
    {
        public async Task<string> GetScramble(string type = "333")
        {
            try
            {
                var client = new WebClient();
                var scramble = await client.DownloadStringTaskAsync($"http://127.0.0.1:2014/scramble/.txt?={type}");
                return scramble.TrimEnd('\n', '\r');
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}

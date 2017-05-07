using Octokit;
using System.Threading.Tasks;

namespace SkyTimer.Service
{
    public interface IGetRemoteVersionService
    {
        Task<string> GetRemoteVersion();
    }

    public class GetRemoteVersionService : IGetRemoteVersionService
    {
        private GitHubClient client = new GitHubClient(new ProductHeaderValue("SkyTimer"));

        public async Task<string> GetRemoteVersion()
        {
            var realease = await client.Repository.Release.GetLatest("GaoSui", "Skytimer");
            return realease.TagName.TrimStart('v');
        }
    }
}

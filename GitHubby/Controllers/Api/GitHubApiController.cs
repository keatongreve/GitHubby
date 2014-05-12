using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GitHubby.Controllers.Api
{
    public class GitHubApiController : ApiController
    {
        private static Octokit.Credentials credentials = new Octokit.Credentials(ConfigurationManager.AppSettings["GitHubApiSecretToken"]);
        private static Octokit.Connection connection = new Octokit.Connection(new Octokit.ProductHeaderValue("GitHubby"))
        {
            Credentials = credentials
        };
        private static Octokit.GitHubClient gitHubClient = new Octokit.GitHubClient(connection);

        // GET /api/GitHubApi/SearchUsers?username={username}
        [HttpGet]
        [ActionName("SearchUsers")]
        public async Task<IList<Octokit.User>> SearchUsers(string search)
        {
            Octokit.SearchUsersRequest request = new Octokit.SearchUsersRequest(search);
            Octokit.SearchUsersResult result = await gitHubClient.Search.SearchUsers(request);
            return result.Items.ToList();
        }

        [HttpGet]
        [ActionName("GetUser")]
        public async Task<object> GetUser(string username)
        {
            var user = await gitHubClient.User.Get(username);
            var userRepos = await gitHubClient.Repository.GetAllForUser(username);
            var result = new { User = user, Repos = userRepos };
            return result;
        }

        [HttpGet]
        [ActionName("GetRepo")]
        public async Task<Octokit.Repository> GetRepo(string owner, string repositoryName)
        {
            var repository = await gitHubClient.Repository.Get(owner, repositoryName);
            var commits = await gitHubClient.GitDatabase.Commit.Get(owner, repositoryName, "HEAD");
            return repository;
        }

    }
}

using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CaseStudy
{
    
    public class GitHub
    {
        private static Lazy<GitHubClient> client = new Lazy<GitHubClient>(() => new GitHubClient(new ProductHeaderValue("CaseStudy")));
        public static GitHubClient Client { get { return client.Value; } }

        public string CaseOne(string userName)
        {
            var repoList = GetRepository(userName);
            if (repoList == null) return "Repository not found for User: " + userName;
            return Routines.ListRepository(repoList);
        }
        public string CaseTwo(string[] args)
        {
            string ret = String.Empty;
            var repoListOne = GetRepository(args[0]);
            var repoListTwo = GetRepository(args[1]);
            if (repoListOne == null) return "Repository not found for User: " + args[0];
            if (repoListTwo == null) return "Repository not found for User: " + args[1];
            int starredOne = GetStarred(repoListOne);
            int starredTwo = GetStarred(repoListTwo);

            ret = ret + Routines.ListStarred(starredOne, args[0]) + "<br>";
            ret = ret + Routines.ListStarred(starredTwo, args[1]) + "<br>";
            ret = ret + (starredOne == starredTwo ? "Equal." : (starredOne > starredTwo ? "First repository has more stars." : "Second repository has more stars."));
            return ret;
        }
        public int GetStarred(Task<IReadOnlyList<Repository>> repository)
        {
            return repository.Result.Count(n => n.StargazersCount > 0);
        }
        public Task<IReadOnlyList<Repository>> GetRepository(string userName)
        {
            Task<IReadOnlyList<Repository>> repository = null;
            try
            {
                repository = Client.Repository.GetAllForUser(userName);
                repository.Wait();
            }
            catch (Exception) { return null; }
            return repository;
        }
    }
    static class Routines
    {
        public static string ListStarred(int starred, string name)
        {
            return starred + " starred repositories for user: " + name;
        }
        public static string ListRepository(Task<IReadOnlyList<Repository>> repository)
        {
            string ret = "Repositories :<br>";
            
            foreach (Repository repo in repository.Result)
            {
                ret = ret + repo.Name + "<br>";
            }
            return ret;
        }
    }

}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using TFSTeamForge.DataProcessing.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSTFServiceHook.Services.Tests
{
    [TestClass()]
    public class GitProviderServiceTests
    {
        private string SourceRepository = "https://cmason.visualstudio.com/DefaultCollection/_git/GitSyncThrowAway";
        private string DestinationRepository = "https://github.com/masonch/ThrowAwayTest.git";
        private string LocalGitRepository = @"C:\Repositories\TFSTF\GitSyncThrowAway\.git";
        private string SourceBranchName = "tf_release";
        private string DestinationBranchName = "tf_release";


        [TestMethod()]
        public void PullFromRepositoryTest()
        {
            var gitProvider = CreateProvider();
            var repoPath = gitProvider.PullFromRepository(SourceRepository, SourceBranchName);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(repoPath));
        }

        [TestMethod()]
        public void PushToRepositoryTest()
        {
            var gitProvider = CreateProvider();
            var pushed = gitProvider.PushToRepository(LocalGitRepository, DestinationRepository, DestinationBranchName);
            Assert.IsTrue(pushed);
        }

        private GitProviderService CreateProvider()
        {
            return new GitProviderService()
            {
                SourceUserName = "",
                SourcePassword = "",
                RemoteUserName = "",
                RemotePassword = "",
                SignatureUserName = "Chris Mason",
                SignatureUserEmail = "chmason@microsoft.com",
                DefaultRemoteName = "tf"
            };
        }
    }
}
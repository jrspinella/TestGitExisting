using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSTeamForge.DataProcessing.Models.TeamSystem.TeamFoundation;
using TFSTeamForge.DataProcessing.Services.TFS;

namespace TFSTFServiceHookTests.Services
{
    [TestClass()]
    public class TFSProviderServiceTests
    {
        [TestMethod()]
        public void GetWorkItemTest()
        {
            var tfsService = CreateTFSService();
            var workItem = tfsService.GetWorkItemAsync(71).Result;
            Assert.IsNotNull(workItem);
        }

        [TestMethod()]
        public void UpdateWorkItemTest()
        {
            var tfsService = CreateTFSService();
            var result = tfsService.AddTeamForgeArtifactIdAsync(71, "1234");
        }

        private TeamFoundationService CreateTFSService()
        {
            var tfsService = new TeamFoundationService();
            var connection = new TeamFoundationConnection()
            {
                Collection = "DefaultCollection",
                ProjectName = "Test1",
                ServerUrl = "http://he3intvdapp175:8080/tfs/",
                UserName = "c39471",
                Password = "4hltasuX!"
            };

            tfsService.SetConnection(connection);

            return tfsService;
        }
    }
}

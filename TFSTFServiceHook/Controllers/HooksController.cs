using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TFSTeamForge.DataProcessing.Models.ServiceHook;
using TFSTeamForge.DataProcessing.Services;

namespace TFSTFServiceHook.Controllers
{
    [RoutePrefix("services/hooks")]
    public class HooksController : ApiController
    {
        private readonly IConfigurationProviderService _configurationService;

        public HooksController(IConfigurationProviderService configurationService)
        {
            _configurationService = configurationService;
        }

        [HttpGet]
        [Route("Test")]
        public string Test()
        {
            return "Test";
        }

        [HttpPost]
        [Route("WorkItemChanged")]
        public async Task<HttpResponseMessage> WorkItemChanged([FromBody]JObject workItemEvent)
        {
            var response = new HttpResponseMessage();
            var rawJson = workItemEvent.ToString();
            try
            {
                var client = new TFSTFServices.TFSMessageServiceClient();
                await client.NewWorkItemMessageAsync(rawJson);

                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Content = new StringContent(e.ToString());
            }
            return response;
        }

        [HttpPost]
        [Route("GitCodePushed")]
        public async Task<HttpResponseMessage> GitCodePushed([FromBody]JObject codePushEvent)
        {
            var response = new HttpResponseMessage();
            var rawJson = codePushEvent.ToString();

            try
            {
                var client = new TFSTFServices.TFSMessageServiceClient();
                await client.NewGitPushMessageAsync(rawJson);

                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Content = new StringContent(e.ToString());
            }
            return response;
        }
    }
}

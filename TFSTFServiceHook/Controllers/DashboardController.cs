using System.Collections.Generic;
using System.Web.Http;
using TFSTFServiceHook.Models.Client;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TFSTFServiceHook.Controllers
{
    [Route("api/dashboard")]
    public class DashboardController : ApiController
    {
        // GET: api/values
        [HttpGet]
        [Route("RecentActivity")]
        public IEnumerable<StatusItem> GetRecentItems()
        {
            var statusItems = new List<StatusItem>();

            return statusItems;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
    }
}

using Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Service2.Controllers
{
    [ApiController]
    [Route("api/number")]
    public class SecondController : ControllerBase
    {
        private static readonly ActivitySource _activitySource = new(ParametersForLogger.GetServiceName(System.Reflection.Assembly.GetEntryAssembly()));

        /// <summary>
        /// Get api values
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> Get()
        {
            string raw;

            using (var activity = _activitySource.StartActivity("CheckSecondController"))
            {
                activity.SetTag("secondControllerNumber", "2");
                var client = new HttpClient();
                raw = await client.GetStringAsync("http://third-service/api/number");                
            }
            return $"второй {raw}";

        }
    }
}

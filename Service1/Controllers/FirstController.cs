using Common;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Service1.Controllers
{
    [ApiController]
    [Route("api/number")]
    public class FirstController : ControllerBase
    {
        private static readonly ActivitySource _activitySource = new(ParametersForLogger.GetServiceName(System.Reflection.Assembly.GetEntryAssembly()));

        /// <summary>
        /// Get api values
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> Get()
        {
            using(var newActivity= _activitySource.StartActivity("CheckFirstController"))
            {
                var client = new HttpClient();
                var raw = await client.GetStringAsync("http://second-service/api/number");

                var activityEvent = new ActivityEvent("Some useful information from first service", DateTimeOffset.Now,
                   tags: new ActivityTagsCollection { new("Here should be Key", new { x = 5, y = 9, coment = "anonymous object, struct like" }) });

                newActivity.AddEvent(activityEvent);

                return $"первый {raw}";
            }
            
        }
    }
}

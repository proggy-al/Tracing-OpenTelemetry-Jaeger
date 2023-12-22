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

                var activityEvent = new ActivityEvent("Some useful information from second service",DateTimeOffset.Now,
                   tags: new ActivityTagsCollection { new("Here should be Key", new  { x=1,y=2,coment="anonymous object, struct like"}) });

                var client = new HttpClient();
                raw = await client.GetStringAsync("http://third-service/api/number");      
                
                Thread.Sleep(3000);

                activity.AddEvent(activityEvent);

                return $"второй {raw}";
            }
            

        }
    }
}

using Common;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Service3.Controllers
{
    [ApiController]
    [Route("api/number")]
    public class ThirdController : ControllerBase
    {
        private static readonly ActivitySource _activitySource = new(ParametersForLogger.GetServiceName(System.Reflection.Assembly.GetEntryAssembly()));

        /// <summary>
        /// Get api values
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> Get()
        {
            using (var act = _activitySource.StartActivity("ThirdControllerActivity"))
            {
                var activityEvent = new ActivityEvent("ProductsRetrievedFromCache", 
                    tags: new ActivityTagsCollection { new("products.count", 3) });

                act.AddEvent(activityEvent);
            }
            return $"третий";
        }
    }
}

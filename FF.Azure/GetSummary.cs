//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Extensions.Http;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;

//namespace FF.Azure
//{
//    public static class GetSummary
//    {
//        [FunctionName(nameof(GetSummary))]
//        public static async Task<IActionResult> Run(
//            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "Summary")] HttpRequest req,
//            ILogger log)
//        {
//            return new OkObjectResult();
//        }
//    }
//}

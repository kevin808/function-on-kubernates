using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Dotnet
{
    public static class Todo
    {
        [FunctionName("Todo")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = "World";

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;


            var response = new OkObjectResult($"Hello, {name}");
            // response.Headers.Add("Access-Control-Allow-Origin", "*");
            // response.Headers.Add("Access-Control-Allow-Methods", "GET,POST,PATCH,DELETE,PUT,OPTIONS");
            // response.Headers.Add("Access-Control-Allow-Headers", "Origin, Content-Type, X-Auth-Token, content-type");
            return name != null
                ? (ActionResult)response
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");

            // response.Headers.Add("Access-Control-Allow-Origin", "*");
            // response.Headers.Add("Access-Control-Allow-Methods", "GET,POST,PATCH,DELETE,PUT,OPTIONS");
            // response.Headers.Add("Access-Control-Allow-Headers", "Origin, Content-Type, X-Auth-Token, content-type");

            // return name
        }
    }
}

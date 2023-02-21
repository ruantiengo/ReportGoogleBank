using System.Net;
using System.Web;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace ReportGoogleBank
{
    public class Report
    {
        private readonly ILogger _logger;

        public Report(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Report>();
        }

        [Function("Report")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "report")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var userEmail = HttpUtility.ParseQueryString(req.Url.Query)["userEmail"];

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

           
            if (userEmail != null) {
                response.WriteString(userEmail);
            }
            else
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.WriteString("Error. invalid userEmail");
            }
         

          

            return response;
        }
    }
}

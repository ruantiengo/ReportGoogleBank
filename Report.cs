using System.Net;
using System.Web;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Npgsql;
using Dapper;
using  Newtonsoft.Json;

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
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "report")] HttpRequestData req)
        {
            string connectionString = "Server=containers-us-west-32.railway.app;Port=6513;User Id=postgres;Password=MqENGILIAnexYhF0bHbR;Database=railway;";
            TransferService service = new TransferService();
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var id = HttpUtility.ParseQueryString(req.Url.Query)["userId"];
            var userId = Int32.Parse(id!);

           var response = req.CreateResponse(HttpStatusCode.OK);
         
          
            try{
                using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();  
                var transfers = await service.GetTransfers(connection, userId);  
               
                 
                await response.WriteAsJsonAsync(transfers);
              
          
                return response;
            } catch(Exception ex){
                _logger.LogError($"Erro ao buscar transferências: {ex.Message}");
                response.StatusCode = HttpStatusCode.BadGateway;
                return response;
            }


         
        }
    }
}

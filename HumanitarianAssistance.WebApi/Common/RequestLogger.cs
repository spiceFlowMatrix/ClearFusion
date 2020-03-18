using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using HumanitarianAssistance.WebApi.Controllers;
using ClearFusion_ERP_Logs;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System;
using Google.Cloud.Logging.V2;
using Google.Apis.Auth.OAuth2;
using Grpc.Auth;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Google.Cloud.Logging.Type;
using Google.Api;
using Google.Api.Gax.Grpc;

namespace HumanitarianAssistance.Common
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private IConfiguration Configuration { get; }
        // private string LogType = "ClearFusion ERP Logs";
        public RequestLogger(ILogger<ApplicationLogs> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            string type="";
            if(request.GetType().ToString().Contains(".Commands.")){
                type = "CommandType";
            } else {
                type = "QueryType";
            }
            var googleCredential = GoogleCredential.FromFile(Directory.GetCurrentDirectory() + "/clear-fusion-193608-0deb8499ba04.json");
            var channel = new Grpc.Core.Channel(
                LoggingServiceV2Client.DefaultEndpoint.Host,
                googleCredential.ToChannelCredentials());
            var client = LoggingServiceV2Client.Create(channel);
            LogName logName = new LogName(Configuration["Stackdriver:ProjectId"], (type == "CommandType")? "Application_Commands": "Application_Queries");
            LogEntry logEntry = new LogEntry
            {
                LogName = logName.ToString(),
                Severity = LogSeverity.Info,
                TextPayload = String.Format("Command/Query Name: {0}, Payload: {1}", request.GetType(), JsonConvert.SerializeObject(request))
            };
            MonitoredResource resource = new MonitoredResource{ Type="gke_container" };
            // IDictionary<string, string> label = new Dictionary<string, string>{
            //     {"abc","fv"}
            // };
            IDictionary<string, string> entryLabels = new Dictionary<string, string>
            {
                // { "Performed By",  ""},
                { "CreatedDate", DateTime.UtcNow.ToString()},
                {  type,  request.GetType().ToString()},
                {  "ClientPayload",  "{}"}
                // { "ActionDescription", model.ActionDescription}
            };
            client.WriteLogEntries(LogNameOneof.From(logName), resource, entryLabels,
                new[] { logEntry }, null);
            // var name = typeof(TRequest).Name;
            // _logger.LogInformation("Api Request: {Name} {@Request}", 
            //     name, JsonConvert.SerializeObject(request));

            return Task.CompletedTask;
        }
    }
}

namespace ClearFusion_ERP_Logs 
{
    public class ApplicationLogs 
    {

    }
}
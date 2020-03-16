using System;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonModels;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Domain.Entities.ActionLog;
using HumanitarianAssistance.Persistence;
using Microsoft.Extensions.Logging;
using Google.Cloud.Logging.V2;
using Google.Apis.Auth.OAuth2;
using Grpc.Auth;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Google.Cloud.Logging.Type;
using Newtonsoft.Json;
using Google.Api;
using Google.Api.Gax.Grpc;
using System.Linq;

namespace HumanitarianAssistance.Application.CommonServices {
    public class ActionLogServices : IActionLogService {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        private IConfiguration Configuration { get; }
        private readonly CallSettings _retryAWhile =
            CallSettings.FromCallTiming(CallTiming.FromRetry(new RetrySettings(
                    new BackoffSettings(TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(12), 2.0),
                    new BackoffSettings(TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(120)),
                    Google.Api.Gax.Expiration.FromTimeout(TimeSpan.FromSeconds(180)),
                    (Grpc.Core.RpcException e) =>
                    new[] { Grpc.Core.StatusCode.Internal,
                        Grpc.Core.StatusCode.DeadlineExceeded }
                        .Contains(e.Status.StatusCode))));
        public ActionLogServices (HumanitarianAssistanceDbContext dbContext, IConfiguration configuration) {
            _dbContext = dbContext;
            Configuration = configuration;
        }
        public async Task<bool> AuditLog (AuditLogModel model) 
        {
            bool isAuditLogged = false;
            try {

                AuditLog auditDetails = new AuditLog () {
                    CreatedById = model.EmployeeId.ToString(),
                    CreatedDate = DateTime.UtcNow,
                    TypeOfEntity = model.TypeOfEntity,
                    EntityId = model.EntityId,
                    ActionTypeId = model.ActionTypeId,
                    ActionDescription = model.ActionDescription
                };
                await _dbContext.AuditLog.AddAsync(auditDetails);
                await _dbContext.SaveChangesAsync();
                isAuditLogged = true;
                return isAuditLogged;
            } catch (Exception ex) {
                Console.WriteLine (ex.Message);
                return isAuditLogged;
            }
        }

        public void HRMAuditLogService(AuditLogModel model, dynamic request) 
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
            LogName logName = new LogName(Configuration["Stackdriver:ProjectId"], (type == "CommandType")? "HRM_Commands": "HRM_Queries");
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
                { "Performed By",  model.EmployeeId.ToString()},
                { "CreatedDate", DateTime.UtcNow.ToString()},
                { "TypeOfEntity",  model.TypeOfEntityName},
                {  type,  request.GetType().ToString()},
                {  "ClientPayload",  "{}"}
                // { "ActionDescription", model.ActionDescription}
            };
            client.WriteLogEntries(LogNameOneof.From(logName), resource, entryLabels,
                new[] { logEntry }, null);
        }

        public dynamic ListHRMLogEntries()
        {
            var googleCredential = GoogleCredential.FromFile(Directory.GetCurrentDirectory() + "/clear-fusion-193608-0deb8499ba04.json");
            var channel = new Grpc.Core.Channel(
                LoggingServiceV2Client.DefaultEndpoint.Host,
                googleCredential.ToChannelCredentials());
            var client = LoggingServiceV2Client.Create(channel);
            LogName logName = new LogName(Configuration["Stackdriver:ProjectId"], "HRM_Audit_Logs");
            ProjectName projectName = new ProjectName(Configuration["Stackdriver:ProjectId"]);
            IEnumerable<string> projectIds = new string[] { projectName.ToString() };
            var results = client.ListLogEntries(projectIds, $"logName={logName.ToString()}",
                "timestamp desc",null , 10, callSettings: _retryAWhile);
            // foreach (var row in results)
            // {
            //     Console.WriteLine($"{row.TextPayload.Trim()}");
            // }
            return results;
        }
    }
}
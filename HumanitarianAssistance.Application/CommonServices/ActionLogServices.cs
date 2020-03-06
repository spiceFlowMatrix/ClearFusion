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

namespace HumanitarianAssistance.Application.CommonServices {
    public class ActionLogServices : IActionLogService {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        private IConfiguration Configuration { get; }
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
            var googleCredential = GoogleCredential.FromFile(Directory.GetCurrentDirectory() + "/clear-fusion-193608-0deb8499ba04.json");
            var channel = new Grpc.Core.Channel(
                LoggingServiceV2Client.DefaultEndpoint.Host,
                googleCredential.ToChannelCredentials());
            var client = LoggingServiceV2Client.Create(channel);
            LogName logName = new LogName(Configuration["Stackdriver:ProjectId"], "HRM_Audit_Logs");
            LogEntry logEntry = new LogEntry
            {
                LogName = logName.ToString(),
                Severity = LogSeverity.Info,
                TextPayload = String.Format("Command/Query Name: {0}, Payload: {1}", request.GetType(), JsonConvert.SerializeObject(request))
            };
            MonitoredResource resource = new MonitoredResource { Type = "global"};
            IDictionary<string, string> entryLabels = new Dictionary<string, string>
            {
                { "CreatedById",  model.EmployeeId.ToString()},
                { "CreatedDate", DateTime.UtcNow.ToString()},
                { "TypeOfEntity",  model.TypeOfEntityName},
                { "EntityId", model.EntityId.ToString()},
                { "ActionTypeId",  model.ActionTypeIdName},
                { "ActionDescription", model.ActionDescription}
            };
            client.WriteLogEntries(LogNameOneof.From(logName), resource, entryLabels,
                new[] { logEntry }, null);
        }
    }
}
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

namespace HumanitarianAssistance.Common
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private string LogType = "ClearFusion ERP Logs";
        public RequestLogger(ILogger<ApplicationLogs> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;
            _logger.LogInformation("Api Request: {Name} {@Request}", 
                name, JsonConvert.SerializeObject(request));

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
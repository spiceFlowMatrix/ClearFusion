using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetCriteriaEvaluationDetailReportPdfQueryHandler : IRequestHandler<GetCriteriaEvaluationDetailReportPdfQuery, byte[]>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
         private readonly IHostingEnvironment _env;
        private readonly IMapper _mapper;
        public GetCriteriaEvaluationDetailReportPdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IPdfExportService pdfExportService,IHostingEnvironment env,IMapper mapper)
        {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
            _env = env;
            _mapper = mapper;
        }

        public Task<byte[]> Handle(GetCriteriaEvaluationDetailReportPdfQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
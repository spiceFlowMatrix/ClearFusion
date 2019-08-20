using AutoMapper;
using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAllExitInterviewQueryHandler : IRequestHandler<GetAllExitInterviewQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;

        public GetAllExitInterviewQueryHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(GetAllExitInterviewQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var exitInterviewList = await _dbContext.ExistInterviewDetails.Where(x => x.IsDeleted == false).ToListAsync();
                List<ExitInterviewModel> lst = new List<ExitInterviewModel>();
                foreach (var item in exitInterviewList)
                {
                    ExitInterviewModel model = new ExitInterviewModel();
                    _mapper.Map(item, model);
                    var empRecord = await _dbContext.EmployeeDetail.Include(x => x.EmployeeProfessionalDetail).Include(x => x.EmployeeProfessionalDetail.DesignationDetails).Include(x => x.EmployeeProfessionalDetail.Department).Where(x => x.EmployeeID == item.EmployeeID).ToListAsync();
                    if (empRecord != null)
                    {
                        model.EmployeeCode = empRecord[0].EmployeeCode;
                        model.EmployeeName = empRecord[0].EmployeeName;
                        model.Position = empRecord[0].EmployeeProfessionalDetail?.DesignationDetails?.Designation ?? null;
                        model.Department = empRecord[0].EmployeeProfessionalDetail?.Department?.DepartmentName ?? null;
                        model.TenureWithCHA = (DateTime.Now.Date - empRecord[0].EmployeeProfessionalDetail.HiredOn.Value.Date).Days.ToString() + " Days";
                        model.Gender = empRecord[0].SexId == 1 ? "Male" : empRecord[0].SexId == 2 ? "Female" : "Other";
                    }
                    lst.Add(model);
                }
                response.data.ExitInterviewList = lst;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}

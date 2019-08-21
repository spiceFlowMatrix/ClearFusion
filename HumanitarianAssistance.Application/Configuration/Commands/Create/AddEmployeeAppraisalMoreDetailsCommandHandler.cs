using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
    public class AddEmployeeAppraisalMoreDetailsCommandHandler : IRequestHandler<AddEmployeeAppraisalMoreDetailsCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddEmployeeAppraisalMoreDetailsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddEmployeeAppraisalMoreDetailsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var recordList = await _dbContext.EmployeeEvaluation.Where(x => x.EmployeeId == request.EmployeeId && x.CurrentAppraisalDate.Date == DateTime.Now.Date).ToListAsync();
                _dbContext.RemoveRange(recordList);

                List<EmployeeEvaluation> lst = new List<EmployeeEvaluation>();
                List<StrongandWeakPoints> StrongList = new List<StrongandWeakPoints>();
                List<StrongandWeakPoints> WeakList = new List<StrongandWeakPoints>();
                foreach (var item in request.EmployeeEvaluationModelList)
                {
                    EmployeeEvaluation obj = new EmployeeEvaluation();
                    obj.FinalResultQues1 = request.FinalResultQues1;
                    obj.FinalResultQues2 = request.FinalResultQues2;
                    obj.FinalResultQues3 = request.FinalResultQues3;
                    obj.FinalResultQues4 = request.FinalResultQues4;
                    obj.FinalResultQues5 = request.FinalResultQues5;
                    obj.DirectSupervisor = request.DirectSupervisor;
                    obj.CommentsByEmployee = request.CommentsByEmployee;
                    obj.CreatedById = request.CreatedById;
                    obj.CreatedDate = request.CreatedDate;
                    obj.CurrentAppraisalDate = DateTime.Now;
                    obj.EmployeeId = request.EmployeeId;
                    lst.Add(obj);
                }
                await _dbContext.EmployeeEvaluation.AddRangeAsync(lst);

                foreach (var item in request.StrongPoints)
                {
                    StrongandWeakPoints obj = new StrongandWeakPoints();
                    obj.CreatedById = request.CreatedById;
                    obj.CreatedDate = request.CreatedDate;
                    obj.CurrentAppraisalDate = DateTime.Now;
                    obj.EmployeeId = request.EmployeeId;
                    obj.Point = item;
                    obj.Status = 1;         // 1 for strong points
                    StrongList.Add(obj);
                }
                await _dbContext.StrongandWeakPoints.AddRangeAsync(StrongList);

                foreach (var item in request.WeakPoints)
                {
                    StrongandWeakPoints obj = new StrongandWeakPoints();
                    obj.CreatedById = request.CreatedById;
                    obj.CreatedDate = request.CreatedDate;
                    obj.CurrentAppraisalDate = DateTime.Now;
                    obj.EmployeeId = request.EmployeeId;
                    obj.Point = item;
                    obj.Status = 2;         // 2 for Weak points
                    WeakList.Add(obj);
                }
                await _dbContext.StrongandWeakPoints.AddRangeAsync(WeakList);
                await _dbContext.SaveChangesAsync();
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

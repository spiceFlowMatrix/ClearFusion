using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries {
    public class GetAllEmployeeLanguagesQueryHandler : IRequestHandler<GetAllEmployeeLanguagesQuery, ApiResponse> {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllEmployeeLanguagesQueryHandler (HumanitarianAssistanceDbContext dbContext, IMapper mapper) {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all languages that an employee can speak and understand
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns>List of languages that employee understands</returns>
        public async Task<ApiResponse> Handle (GetAllEmployeeLanguagesQuery request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            try {
                var employeeRecord = await (from e in _dbContext.EmployeeLanguages.Where (x => x.EmployeeId == request.EmployeeId && x.IsDeleted == false) 
                join l in _dbContext.LanguageDetail on e.LanguageId equals l.LanguageId 
                into ld from l in ld.DefaultIfEmpty () 
                select new EmployeeLanguageModel {
                    SpeakLanguageId = e.SpeakLanguageId,
                        LanguageName = l.LanguageName,
                        LanguageId = e.LanguageId,
                        Reading = e.Reading,
                        Writing = e.Writing,
                        Speaking = e.Speaking,
                        Listening = e.Listening,
                        EmployeeId = e.EmployeeId
                }).ToListAsync ();

                if (employeeRecord != null) {
                    response.data.EmployeeLanguagesList = employeeRecord;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                } else {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "No Record Found";
                }
            } catch (Exception ex) {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}
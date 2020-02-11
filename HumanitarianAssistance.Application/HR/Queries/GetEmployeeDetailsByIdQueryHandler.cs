using AutoMapper;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.FileManagement.Models;
using HumanitarianAssistance.Application.Store.Models;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeDetailsByIdQueryHandler : IRequestHandler<GetEmployeeDetailsByIdQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileManagementService _fileManagement;

        public GetEmployeeDetailsByIdQueryHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper, IFileManagementService fileManagement)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileManagement = fileManagement;
        }

        public async Task<ApiResponse> Handle(GetEmployeeDetailsByIdQuery request, CancellationToken cancellationToken)
        {

            ApiResponse response = new ApiResponse();
            try
            {
                FileModel model = new FileModel()
                {
                    PageId = (int)FileSourceEntityTypes.EmployeeProfile,
                    RecordId = request.EmployeeId,
                    DocumentTypeId = (int)DocumentFileTypes.EmployeeProfile
                };
                 StoreDocumentModel documentModel = new StoreDocumentModel();
                 documentModel = await _fileManagement.GetFilesByRecordIdAndDocumentType(model);
                var employeelist =
                     await _dbContext.EmployeeDetail
                        .Include(e => e.EmployeeType)
                        .Include(p => p.ProvinceDetails)
                        .Include(c => c.CountryDetails)
                        .Include(h => h.QualificationDetails)
                        .Include(p => p.EmployeeProfessionalDetail)
                        .ThenInclude(p => p.professionDetails)
                        .Where(x => x.EmployeeID == request.EmployeeId && x.IsDeleted == false)
                        .Select(x => new EmployeeDetailModel
                        {
                            EmployeeID = x.EmployeeID,
                            EmployeeCode = x.EmployeeCode,
                            EmployeeTypeId = x.EmployeeTypeId,
                            EmployeeTypeName = x.EmployeeType.EmployeeTypeName ?? null,
                            EmployeeName = x.EmployeeName,
                            FatherName = x.FatherName,
                            GradeId = x.GradeId,
                            PermanentAddress = x.PermanentAddress,
                            CurrentAddress = x.CurrentAddress,
                            City = x.City,
                            ProvinceId = x.ProvinceId,
                            ProvinceName = x.ProvinceDetails.ProvinceName ?? null,
                            CountryId = x.CountryId,
                            CountryName = x.CountryDetails.CountryName ?? null,
                            Phone = x.Phone,
                            Email = x.Email,
                            ReferBy = x.ReferBy,
                            SexId = x.SexId,
                            SexName = x.SexId == (int)Gender.MALE ? "Male" : x.SexId == (int)Gender.FEMALE ? "Female" : x.SexId == (int)Gender.OTHER ? "Other" : null,
                            DateOfBirth = x.DateOfBirth.ToString(),
                            Age = x.Age ?? 0,
                            HigherQualificationId = x.HigherQualificationId,
                            HigherQualificationName = x.QualificationDetails.QualificationName ?? null,
                            ProfessionName = x.EmployeeProfessionalDetail.professionDetails.ProfessionName ?? null,
                            ProfessionId = x.EmployeeProfessionalDetail.ProfessionId,
                            PreviousWork = x.PreviousWork,
                            ExperienceYear = x.ExperienceYear,
                            ExperienceMonth = x.ExperienceMonth,
                            Resume = x.Resume,
                            EmployeePhoto = documentModel.SignedURL,
                            DocumentGUID = x.DocumentGUID + x.Extension,
                            MaritalStatus = x.MaritalStatusId,
                            University = x.University,
                            BirthPlace = Convert.ToInt32(x.BirthPlace),
                            IssuePlace = x.IssuePlace,
                            PassportNo = x.PassportNo,
                            TinNumber = x.EmployeeProfessionalDetail.TinNumber
                        }).ToListAsync();

                response.data.EmployeeDetailList = employeelist;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}

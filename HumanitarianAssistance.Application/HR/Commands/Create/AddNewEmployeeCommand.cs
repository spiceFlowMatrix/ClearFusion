﻿using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddNewEmployeeCommand : BaseModel, IRequest<ApiResponse>
    {
        public AddNewEmployeeCommand()
        {
            PensionDetailModel = new PensionDetailModel();
            //PensionDetail = new PensionList();
        } 
        public int EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public int? EmployeeTypeId { get; set; }
        public string EmployeeTypeName { get; set; }
        public string EmployeeName { get; set; }
        public string IDCard { get; set; }
        public string FatherName { get; set; }
        public int? GradeId { get; set; }
        public string PermanentAddress { get; set; }
        public string CurrentAddress { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public int? ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string ReferBy { get; set; }
        public string Passport { get; set; }
        public int? NationalityId { get; set; }
        public string NationalityName { get; set; }
        public string Language { get; set; }
        public int? SexId { get; set; }
        public string SexName { get; set; }
        public string DateOfBirth { get; set; }
        public int? Age { get; set; }
        public string PlaceOfBirth { get; set; }
        public int? HigherQualificationId { get; set; }
        public string HigherQualificationName { get; set; }
        public int? ProfessionId { get; set; }
        public string ProfessionName { get; set; }
        public string PreviousWork { get; set; }
        public string Remarks { get; set; }
        public int? ExperienceYear { get; set; }
        public int? ExperienceMonth { get; set; }
        public string Resume { get; set; }
        public string EmployeePhoto { get; set; }
        public string DocumentGUID { get; set; }
        public int OfficeId { get; set; }

        public int? MaritalStatus { get; set; }
        public string PassportNo { get; set; }
        public string University { get; set; }
        public string BirthPlace { get; set; }
        public string IssuePlace { get; set; }
        public int? MaritalStatusId { get; set; }
        public string TinNumber { get; set; }
        public double OpeningPension { get; set; }
        public int? EmployeeContractTypeId { get; set; }
        public DateTime? HiredOn { get; set; }
        public DateTime? FiredOn { get; set; }
        public string FiredReason { get; set; }
        public DateTime? ResignationOn { get; set; }
        public string ResignationReason { get; set; }
        public string Password { get; set; }
        public int? AttendanceGroupId { get; set; }
        public int? DutyStation { get; set; }
public string TrainingAndBenefits { get; set; }
public string JobDescription { get; set; }
public int? Department { get; set; }
public int? Designation { get; set; }

        public PensionDetailModel PensionDetailModel { get; set; }
    }
    public class PensionDetailModel
    {
        public DateTime? PensionDate { get; set; }
        public List<PensionDetail> PensionDetail { get; set; }
    }
    public class PensionDetail
    {
        public int CurrencyId { get; set; }
        public double?  Amount { get; set; }
    }
}

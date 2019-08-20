using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Commands.Create
{
    public class AddChartOfAccountCommandHandler : IRequestHandler<AddChartOfAccountCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public AddChartOfAccountCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(AddChartOfAccountCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request != null)
                {
                    if (request.AccountName != null)
                    {
                        request.AccountName = request.AccountName.Trim();
                    }
                }

                //Main Level
                if (request.AccountLevelId == (int)AccountLevels.MainLevel)
                {
                    int levelcount = await _dbContext.ChartOfAccountNew.CountAsync(x => x.AccountLevelId == (int)AccountLevels.MainLevel && x.IsDeleted == false);

                    if (levelcount < (int)AccountLevelLimits.MainLevel)
                    {

                        bool isCOACodeExists = await _dbContext.ChartOfAccountNew.AnyAsync(x => x.ChartOfAccountNewCode == request.ChartOfAccountNewCode && x.IsDeleted == false);

                        if (isCOACodeExists)
                        {
                            throw new Exception("Account Code Already Exists!!!");
                        }

                        ChartOfAccountNew obj = new ChartOfAccountNew();

                        obj.AccountLevelId = (int)AccountLevels.MainLevel;
                        obj.AccountHeadTypeId = request.AccountHeadTypeId;
                        obj.ParentID = -1;
                        obj.AccountName = request.AccountName;
                        obj.ChartOfAccountNewCode = request.ChartOfAccountNewCode;
                        obj.CreatedById = request.CreatedById;
                        obj.CreatedDate = request.CreatedDate;
                        obj.IsDeleted = false;

                        await _dbContext.ChartOfAccountNew.AddAsync(obj);
                        await _dbContext.SaveChangesAsync();

                        obj.ParentID = obj.ChartOfAccountNewId;

                        await _dbContext.SaveChangesAsync();

                        response.data.ChartOfAccountNewDetail = obj;
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = StaticResource.SuccessText;
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.ExceedLevelCount;
                    }
                }

                //Control Level
                else if (request.AccountLevelId == (int)AccountLevels.ControlLevel)
                {
                    int levelcount = await _dbContext.ChartOfAccountNew.CountAsync(x => x.AccountLevelId == (int)AccountLevels.ControlLevel && x.ParentID == request.ParentID);

                    if (levelcount < (int)AccountLevelLimits.ControlLevel)
                    {
                        ChartOfAccountNew obj = new ChartOfAccountNew();

                        bool isCOACodeExists = await _dbContext.ChartOfAccountNew.AnyAsync(x => x.ChartOfAccountNewCode == request.ChartOfAccountNewCode && x.IsDeleted == false);

                        if (isCOACodeExists)
                        {
                            throw new Exception("Account Code Already Exists!!!");
                        }

                        ChartOfAccountNew parentPresent = await _dbContext.ChartOfAccountNew.FirstOrDefaultAsync(x => x.ChartOfAccountNewId == request.ParentID);

                        if (parentPresent != null)
                        {
                            obj.AccountLevelId = (int)AccountLevels.ControlLevel;
                            obj.AccountHeadTypeId = request.AccountHeadTypeId;
                            obj.ParentID = request.ParentID;
                            obj.ChartOfAccountNewCode = request.ChartOfAccountNewCode;
                            obj.AccountName = request.AccountName;
                            obj.CreatedById = request.CreatedById;
                            obj.CreatedDate = request.CreatedDate;
                            obj.IsDeleted = false;

                            await _dbContext.ChartOfAccountNew.AddAsync(obj);
                            await _dbContext.SaveChangesAsync();

                            response.data.ChartOfAccountNewDetail = obj;
                            response.StatusCode = StaticResource.successStatusCode;
                            response.Message = StaticResource.SuccessText;
                        }
                        else
                        {
                            response.StatusCode = StaticResource.failStatusCode;
                            response.Message = StaticResource.ParentIdNotPresent;
                        }
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.ExceedLevelCount;
                    }
                }

                //Sub Level
                else if (request.AccountLevelId == (int)AccountLevels.SubLevel)
                {
                    int levelcount = await _dbContext.ChartOfAccountNew.CountAsync(x => x.AccountLevelId == (int)AccountLevels.SubLevel && x.ParentID == request.ParentID);

                    if (levelcount < (int)AccountLevelLimits.SubLevel)
                    {
                        ChartOfAccountNew obj = new ChartOfAccountNew();

                        bool isCOACodeExists = await _dbContext.ChartOfAccountNew.AnyAsync(x => x.ChartOfAccountNewCode == request.ChartOfAccountNewCode && x.IsDeleted == false);

                        if (isCOACodeExists)
                        {
                            throw new Exception("Account Code Already Exists!!!");
                        }

                        ChartOfAccountNew parentPresent = await _dbContext.ChartOfAccountNew.FirstOrDefaultAsync(x => x.ChartOfAccountNewId == request.ParentID);

                        if (parentPresent != null)
                        {

                            obj.AccountLevelId = (int)AccountLevels.SubLevel;
                            obj.AccountHeadTypeId = request.AccountHeadTypeId;
                            obj.ParentID = request.ParentID;
                            obj.AccountName = request.AccountName;
                            obj.ChartOfAccountNewCode = request.ChartOfAccountNewCode;

                            obj.AccountFilterTypeId = request.AccountFilterTypeId != null ? request.AccountFilterTypeId : null;  //dropdown

                            if (request.AccountTypeId != null)
                            {
                                obj.AccountTypeId = request.AccountTypeId;
                                // Get balance type.
                                obj.IsCreditBalancetype = await GetAccountBalanceTypeByAccountType((int)obj.AccountTypeId);
                            }
                            else
                            {
                                obj.AccountTypeId = null;
                                obj.IsCreditBalancetype = null;
                            }

                            obj.CreatedById = request.CreatedById;
                            obj.CreatedDate = request.CreatedDate;
                            obj.IsDeleted = false;

                            await _dbContext.ChartOfAccountNew.AddAsync(obj);
                            await _dbContext.SaveChangesAsync();

                            response.data.ChartOfAccountNewDetail = obj;
                            response.StatusCode = StaticResource.successStatusCode;
                            response.Message = StaticResource.SuccessText;
                        }
                        else
                        {
                            response.StatusCode = StaticResource.failStatusCode;
                            response.Message = StaticResource.ParentIdNotPresent;
                        }

                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.ExceedLevelCount;
                    }
                }

                //Input Level
                else if (request.AccountLevelId == (int)AccountLevels.InputLevel)
                {
                    int levelcount = await _dbContext.ChartOfAccountNew.CountAsync(x => x.AccountLevelId == (int)AccountLevels.InputLevel && x.ParentID == request.ParentID);

                    if (levelcount < (int)AccountLevelLimits.InputLevel)
                    {

                        bool isCOACodeExists = await _dbContext.ChartOfAccountNew.AnyAsync(x => x.ChartOfAccountNewCode == request.ChartOfAccountNewCode && x.IsDeleted == false);

                        if (isCOACodeExists)
                        {
                            throw new Exception("Account Code Already Exists!!!");
                        }

                        ChartOfAccountNew obj = new ChartOfAccountNew();

                        ChartOfAccountNew parentPresent = await _dbContext.ChartOfAccountNew.FirstOrDefaultAsync(x => x.ChartOfAccountNewId == request.ParentID);

                        if (parentPresent != null)
                        {
                            obj.AccountLevelId = (int)AccountLevels.InputLevel;
                            obj.AccountHeadTypeId = request.AccountHeadTypeId;
                            obj.ParentID = request.ParentID;
                            obj.ChartOfAccountNewCode = request.ChartOfAccountNewCode;
                            obj.AccountName = request.AccountName;
                            obj.CreatedById = request.CreatedById;
                            obj.CreatedDate = request.CreatedDate;

                            // set account type and balance type
                            obj.AccountTypeId = parentPresent.AccountTypeId;
                            obj.IsCreditBalancetype = parentPresent.IsCreditBalancetype;
                            obj.AccountFilterTypeId = parentPresent.AccountFilterTypeId;
                            obj.IsDeleted = false;

                            await _dbContext.ChartOfAccountNew.AddAsync(obj);
                            await _dbContext.SaveChangesAsync();

                            response.data.ChartOfAccountNewDetail = obj;
                            response.StatusCode = StaticResource.successStatusCode;
                            response.Message = StaticResource.SuccessText;
                        }
                        else
                        {
                            response.StatusCode = StaticResource.failStatusCode;
                            response.Message = StaticResource.ParentIdNotPresent;
                        }
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.ExceedLevelCount;
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<bool> GetAccountBalanceTypeByAccountType(int accountTypeId)
        {
            var accountType = await _dbContext.AccountType.FirstOrDefaultAsync(x => x.AccountTypeId == accountTypeId);
            var accountHeadType = await _dbContext.AccountHeadType.FirstOrDefaultAsync(x => x.AccountHeadTypeId == accountType.AccountHeadTypeId);
            return accountHeadType.IsCreditBalancetype;
        }
    }
}
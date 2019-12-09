using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using HumanitarianAssistance.Common.Helpers;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Queries;
using HumanitarianAssistance.Application.Project.Commands.Create;
using HumanitarianAssistance.Application.Project.Commands.Delete;
using HumanitarianAssistance.Application.Project.Commands.Update;
using HumanitarianAssistance.Application.Project.Commands.Common;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Enums;
using System.Linq;

namespace HumanitarianAssistance.WebApi.Controllers.Project
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/ProjectLogistic/[Action]")]
    [ApiExplorerSettings(GroupName = nameof(SwaggerGrouping.Project))]
    [Authorize]
    public class ProjectLogisticController : BaseController
    {
        private IHostingEnvironment _hostingEnvironment;
        public ProjectLogisticController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        
        [HttpPost]
        public async Task<ApiResponse> AddLogisticRequest([FromBody]AddLogisticRequestCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> GetAllLogisticRequest([FromBody]GetAllLogisticRequestQuery model)
        {
            return await _mediator.Send(model);
        }
        
        [HttpPost]
        public async Task<ApiResponse> DeleteLogisticRequest([FromBody]long RequestId)
        {   
            DeleteLogisticRequestCommand model = new DeleteLogisticRequestCommand();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.RequestId = RequestId; 
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> GetRequestDetailById([FromBody]long RequestId)
        {   
            GetRequestDetailByIdQuery model = new GetRequestDetailByIdQuery();
            model.RequestId = RequestId; 
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> AddLogisticRequestItems([FromBody]AddLogisticRequestItemsCommand model)
        {   
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> GetItemsByRequestId([FromBody]long RequestId)
        {   
            GetItemsByRequestIdQuery model = new GetItemsByRequestIdQuery();
            model.RequestId = RequestId; 
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteLogisticRequestItem([FromBody]DeleteLogisticRequestItemCommand model)
        {   
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow; 
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> EditLogisticRequestItems([FromBody]EditLogisticRequestItemsCommand model)
        {   
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> CancelLogisticRequest([FromBody]long RequestId)
        {   
            CancelLogisticRequestCommand model = new CancelLogisticRequestCommand();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.RequestId = RequestId; 
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> IssuePurchaseOrder([FromBody]long RequestId)
        {   
            IssuePurchaseOrderCommand model = new IssuePurchaseOrderCommand();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.RequestId = RequestId; 
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> SubmitPurchaseOrder([FromBody]SubmitPurchaseOrderCommand model)
        {   
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }
        
        [HttpPost]
        public async Task<ApiResponse> GetPurchasedItemsList([FromBody]long RequestId)
        {   
            GetPurchasedItemsListQuery model = new GetPurchasedItemsListQuery();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.RequestId = RequestId; 
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> CancelComparativeRequest([FromBody]long RequestId)
        {   
            CancelComparativeRequestCommand model = new CancelComparativeRequestCommand();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.RequestId = RequestId; 
            return await _mediator.Send(model);
        }
        
        [HttpPost]
        public async Task<ApiResponse> IssueComparativeStatement([FromBody]long RequestId)
        {   
            IssueComparativeStatementCommand model = new IssueComparativeStatementCommand();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.RequestId = RequestId; 
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> AddLogisticSupplier([FromBody]AddLogisticSupplierCommand model)
        {   
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> GetLogisticSupplierList([FromBody]long RequestId)
        {   
            GetLogisticSupplierListQuery model = new GetLogisticSupplierListQuery();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.requestId = RequestId; 
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteLogisticSupplier([FromBody]long SupplierId)
        {   
            DeleteLogisticSupplierCommand model = new DeleteLogisticSupplierCommand();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.SupplierId = SupplierId; 
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> EditLogisticSupplier([FromBody]EditLogisticSupplierCommand model)
        {   
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> SubmitComparativeStatement([FromBody]SubmitComparativeStatementCommand model)
        {   
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> GetComparativeStatement([FromBody]long requestId)
        {   
           GetComparativeStatementQuery model = new GetComparativeStatementQuery();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.requestId = requestId; 
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> RejectComparativeStatement([FromBody]long requestId)
        {   
           RejectComparativeStatementCommand model = new RejectComparativeStatementCommand();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.requestId = requestId;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow; 
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> ApproveComparativeStatement([FromBody]long requestId)
        {   
           ApproveComparativeStatementCommand model = new ApproveComparativeStatementCommand();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.requestId = requestId;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow; 
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> RejectPurchaseOrder([FromBody]long requestId)
        {   
           RejectPurchaseOrderCommand model = new RejectPurchaseOrderCommand();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.requestId = requestId;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow; 
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> GetGoodsRecievedNote([FromBody]long requestId)
        {   
           GetGoodsRecievedNoteQuery model = new GetGoodsRecievedNoteQuery();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.requestId = requestId;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> EditLogisticRequest([FromBody]EditLogisticRequestCommand model)
        {   
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow; 
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> GetPurchaseOrderDetail([FromBody]long requestId)
        {   
            GetPurchaseOrderDetailQuery model = new GetPurchaseOrderDetailQuery();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.requestId = requestId;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> VerifyPurchaseOrder([FromBody]VerifyPurchaseOrderCommand model)
        {   
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }
        
        
    }

}
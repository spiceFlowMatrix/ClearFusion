//using AutoMapper;
//using HumanitarianAssistance.Application.Infrastructure;
//using HumanitarianAssistance.Application.Marketing.Models;
//using HumanitarianAssistance.Common.Helpers;
//using HumanitarianAssistance.Persistence;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using SelectPdf;
//using System.Threading.Tasks;

//namespace HumanitarianAssistance.Application.Marketing.Commands.Create
//{
//    public class CreatePDFCommandHandler : IRequestHandler<CreatePDFCommand, ApiResponse>
//    {
//        private HumanitarianAssistanceDbContext _dbContext;
//        private IMapper _mapper;
//        public CreatePDFCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
//        {
//            _dbContext = dbContext;
//            _mapper = mapper;
//        }
//        public async Task<ApiResponse> Handle(CreatePDFCommand request, CancellationToken cancellationToken)
//        {
//            ApiResponse response = new ApiResponse();
//            byte[] pdf = null;
//            try
//            {
//                JobPriceModel JobDetails = await (from j in _dbContext.JobDetails
//                                                  join jp in _dbContext.JobPriceDetails on j.JobId equals jp.JobId
//                                                  join cd in _dbContext.ContractDetails on j.ContractId equals cd.ContractId
//                                                  join cur in _dbContext.CurrencyDetails on cd.CurrencyId equals cur.CurrencyId
//                                                  where !j.IsDeleted && !jp.IsDeleted && j.JobId == request.JobId
//                                                  select (new JobPriceModel
//                                                  {
//                                                      JobName = j.JobName,
//                                                      JobCode = j.JobCode,
//                                                      UnitRate = jp.UnitRate,
//                                                      EndDate = j.EndDate,
//                                                      StartDate = cd.StartDate,
//                                                      IsApproved = j.IsApproved,
//                                                      ClientName = cd.ClientName
//                                                  })).FirstOrDefaultAsync();

//                //var imagepath = Path.Combine(_hostingEnvironment.WebRootPath, "agreement-logo.png");
//                //< img width = '100' height = '100' src = " + imagepath + @" >
//                DdlPageSize = "A4";
//                PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize),
//                    DdlPageSize, true);
//                DdlPageOrientation = "Portrait";
//                PdfPageOrientation pdfOrientation =
//                    (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
//                    DdlPageOrientation, true);

//                int webPageWidth = 1024;

//                webPageWidth = Convert.ToInt32(TxtWidth);


//                int webPageHeight = 0;

//                webPageHeight = Convert.ToInt32(TxtHeight);


//                // instantiate a html to pdf converter object
//                HtmlToPdf converter = new HtmlToPdf();

//                // set converter options
//                converter.Options.PdfPageSize = pageSize;
//                converter.Options.PdfPageOrientation = pdfOrientation;
//                converter.Options.WebPageWidth = webPageWidth;
//                converter.Options.WebPageHeight = webPageHeight;
//                TxtHtmlCode = @"<div style='padding-left:40px !important' id='jobReportPdf' align='center'>
//                        <div class='container-fluid'>
//                        <div class='col-md-12'>
//                            <table align = 'center' width='700px;' style='margin:0 auto; vertical-align: middle; font-family: Arial, Helvetica, sans-serif;'>
//                               <tbody>
//                                  <tr>
//                                      <td colspan = '2'>
//                                         <table width='100%' cellpadding='0' cellspacing='0'>
//                                            <tbody>
//                                               <tr>
//                                                  <td width = '85%' style='font-size:16px; font-weight:700; vertical-align:top; text-align: center;'>NAWA RADIO<br>
//                                                      <p style = 'margin:5px 0;'> Marketing Department</p>
//                                                      Broadcasting Agreement Paper
//                                                  </td>
//                                                  <td width = '15%' style= 'text-align:right;'>  
                                                    
//                                                  </td>
//                                              </tr>
//                                          </tbody>
//                                       </table>
//                                    </td>
//                                   <td></td>
//                               </tr>
//                               <tr>
//                                  <td colspan= '2'>
//                                      <table width= '100%' style= 'border: 1px solid; padding: 5px;'>
//                                          <tbody>
//                                              <tr>
//                                                 <td colspan= '2' style= 'font-size:14px; text-align:center;'>
//                                                    Add: Khushhal Khan Meena in front of Dawat University<br>
//                                                    Kabul, Afghanistan<br>
//                                                    Email: Marketing @sabacent.org<br>
//                                                    Phone # 0703141414<br>
//                                                 </td>
//                                             </tr>
//                                         </tbody>
//                                     </table>
//                                  </td>
//                               </tr>
//                               <tr>
//                                  <td colspan = '2'>
//                                     <table width= '100%' cellpadding= '0' cellspacing= '0'>
//                                        <tbody>
//                                           <tr>
//                                              <td colspan= '2' style= 'font-size:13px;' ><b> Contractor:</b><br>
//                                                 <b>Subject of Contract:</b> Broadcasting of Spots<br>
//                                                 This contract is between NAWA RADIO 103.1FM as vender and (
//                                                 <b>" + JobDetails.ClientName + @"</b> ) as a client.<br>
//                                                 The contract is based on: <b>" + JobDetails.StartDate.ToShortDateString() + @"</b> up
//                                                 to <b>" + JobDetails.EndDate.ToShortDateString() + @"</b><br>
//                                              </td>
//                                          </tr>
//                                          <tr>
//                                             <td colspan = '2' style='font-size:15px;font-weight: 700; color:#000;'>
//                                                <p style = 'padding:10px  0;' > Both parties' responsibilities are as follow:</p>
//                                            </td>
//                                          </tr>
//                                          <tr>
//                                              <td colspan = '2' style='font-size:13px; color:#000;padding-left:20px;'>
//                                                  <p>1. Broadcasting of(Spots) in NAWA Radio.</p>
//                                                  <p>2. Radio airtimes should be in Flat time.</p>
//                                                  <p>3. Programs status : ( Active )</p>
//                                                  <p>4. The broadcasting will be provided by</p>
//                                                  <p>The broadcasting cost of one month will be (
//                                                  <b>" + JobDetails.UnitRate + @"</b> )</p>
//                                              </td>
//                                          </tr>
//                                          <tr>
//                                             <td colspan = '2' style='font-size:15px;font-weight: 700; color:#000;'>
//                                                 <p style = 'padding:10px  0;'> Both parties' responsibilities are as follow:</p>
//                                             </td>
//                                          </tr>
//                                          <tr>
//                                             <td colspan = '2' style='font-size:13px; color:#000;padding-left:20px;'>
//                                                <p style = 'font-weight:700;' > 1 - Customer:</p>
//                                                <p>- Payment of amount.before the starting of broadcasting.<br>
//                                                   - If client once approve the program format it will be his/her
//                                                   responsibility even if any mistakes were there.<br>
//                                                   - Provision of the schedule.<br>
//                                                   - The programs should not be against National benefits and Radio
//                                                   Nawa policies.
//                                                </p>
//                                                <p style = 'font-weight:700;' > 2 - NAWA RADIO</p>
//                                                <p>  - Broadcasting of (Spots) Audio programs in Flat times as per
//                                                     the approved schedule.<br>
//                                                </p>
//                                            </td>
//                                         </tr>
//                                         <tr>
//                                           <td colspan = '2' style= 'font-size:15px;padding-top:10px;'>
//                                             <b> Note:</b>  NAWA RADIO has no legal responsibility for the subjects and
//                                             contents of the programs and advertisements.
//                                           </td>
//                                         </tr>
//                                      </tbody>
//                                   </table>
//                                 </td>
//                               </tr>
//                               <tr>
//                                 <td colspan = '2'><hr></td>
//                               </tr>
//                               <tr>
//                                 <td colspan= '2'>
//                                    <p style= 'font-size:13px;' > Both parties are agreed to terms and conditions in the contract stated above.</p>
//                                 </td>
//                               </tr>
//                               <tr>
//                                  <td style = 'font-size: 15px;padding-top:10px;'>
//                                     <p><b> NAWA RADIO</b></p>
//                                     <p><b>Representative Name:</b></p>
//                                     <p><b>Signature:</b></p>
//                                     <p><b>Date:</b>" + DateTime.UtcNow.ToShortDateString() + @"</p>
//                                  </td>
//                                  <td>
//                                     <p><b>Customer's Name: </b>" + JobDetails.ClientName + @"</p>
//                                     <p><b>Signature:</b></p>
//                                  </td>
//                               </tr>
//                            </tbody>
//                        </table>
//                     </div>
//                     </div>
//                 </div>";

//                PdfDocument doc = converter.ConvertHtmlString(TxtHtmlCode);
//                pdf = doc.Save();
//                Console.WriteLine(doc);
//                response.StatusCode = StaticResource.successStatusCode;
//                response.Message = "Success";
//                response.data.pdf = pdf;
//                doc.Close();

//            }
//            catch (Exception ex)
//            {
//                response.StatusCode = StaticResource.failStatusCode;
//                response.Message = StaticResource.SomethingWrong + ex.Message;
//            }
//            return response;
//        }

//        [BindProperty]
//        public string TxtHtmlCode { get; set; }

//        [BindProperty]
//        public string DdlPageSize { get; set; }

//        [BindProperty]
//        public string DdlPageOrientation { get; set; }
//        public List<SelectListItem> PageOrientations { get; } = new List<SelectListItem>
//        {
//            new SelectListItem { Value = "Portrait", Text = "Portrait" },
//            new SelectListItem { Value = "Landscape", Text = "Landscape" },
//        };

//        [BindProperty]
//        public string TxtWidth { get; set; }

//        [BindProperty]
//        public string TxtHeight { get; set; }

//        // GET: api/<controller>
//        [HttpGet]
//        public IEnumerable<string> Get(string html)
//        {

//            return new string[] { "value1", "value2" };
//        }


//    }
//}

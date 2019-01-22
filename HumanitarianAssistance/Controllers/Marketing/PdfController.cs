using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelectPdf;

namespace HumanitarianAssistance.WebAPI.Controllers.Marketing
{
  [Produces("application/json")]
  [Route("api/Pdf/[Action]")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class PdfController : Controller
  {
    private IHostingEnvironment _hostingEnvironment;
    public PdfController(IHostingEnvironment environment)
    {
      _hostingEnvironment = environment;
    }
    [BindProperty]
    public string TxtHtmlCode { get; set; }

    [BindProperty]
    public string TxtBaseUrl { get; set; }

    [BindProperty]
    public string DdlPageSize { get; set; }
    public void OnGet()
    {
      DdlPageSize = "A4";
      DdlPageOrientation = "Portrait";
      TxtHtmlCode = @"
      <html>
        <body>
          Hello World from selectpdf.com.
        </body>
     </html>";
    }
    [BindProperty]
    public string DdlPageOrientation { get; set; }
    public List<SelectListItem> PageSizes { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "A1", Text = "A1" },
            new SelectListItem { Value = "A2", Text = "A2" },
            new SelectListItem { Value = "A3", Text = "A3" },
            new SelectListItem { Value = "A4", Text = "A4" },
            new SelectListItem { Value = "A5", Text = "A5" },
            new SelectListItem { Value = "Letter", Text = "Letter" },
            new SelectListItem { Value = "HalfLetter", Text = "HalfLetter" },
            new SelectListItem { Value = "Ledger", Text = "Ledger" },
            new SelectListItem { Value = "Legal", Text = "Legal" },
        };
    public List<SelectListItem> PageOrientations { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Portrait", Text = "Portrait" },
            new SelectListItem { Value = "Landscape", Text = "Landscape" },
        };

    [BindProperty]
    public string TxtWidth { get; set; }

    [BindProperty]
    public string TxtHeight { get; set; }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public HttpResponseMessage CreatePDF(string GetHTMLString)
    {
      string path = Path.Combine(_hostingEnvironment.WebRootPath, "export.pdf");
      var imagepath = Path.Combine(_hostingEnvironment.WebRootPath, "agreement-logo.png");
      DdlPageSize = "A4";
      PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize),
          DdlPageSize, true);
      DdlPageOrientation = "Portrait";
      PdfPageOrientation pdfOrientation =
          (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
          DdlPageOrientation, true);

      int webPageWidth = 1024;
      try
      {
        webPageWidth = Convert.ToInt32(TxtWidth);
      }
      catch { }

      int webPageHeight = 0;
      try
      {
        webPageHeight = Convert.ToInt32(TxtHeight);
      }
      catch { }

      // instantiate a html to pdf converter object
      HtmlToPdf converter = new HtmlToPdf();

      // set converter options
      converter.Options.PdfPageSize = pageSize;
      converter.Options.PdfPageOrientation = pdfOrientation;
      converter.Options.WebPageWidth = webPageWidth;
      converter.Options.WebPageHeight = webPageHeight;
      TxtHtmlCode = @"<div class='container-fluid'>
                       <div id='jobReportPdf' align='center'>
                        <div class='col-md-12'>
                            <table align = 'center' width='700px;' style='margin:0 auto; vertical-align: middle; font-family: Arial, Helvetica, sans-serif;'>
                            <tbody><tr>
              <td colspan = '2'>
                <table width='100%' cellpadding='0' cellspacing='0'>
                  <tbody><tr>
                    <td width = '80%' style='font-size:16px; font-weight:700; vertical-align:top; text-align: center;'>NAWA RADIO<br>
                          <p style = 'margin:5px 0;'> Marketing Department</p>
                          Broadcasting Agreement Paper
                    </td>
                    <td width = '20%' style= 'text-align:right;'>
  
                        <img width= '100' height= '100' src=" + imagepath + @">
                      </td>
                    </tr>
                  </tbody></table>
                </td>
                <td></td>
              </tr>
              <tr>
                <td colspan= '2'>
                  <table width= '100%' style= 'border: 1px solid; padding: 5px;'>
                    <tbody><tr>
                      <td colspan= '2' style= 'font-size:14px; text-align:center;'>
                          Add: Khushhal Khan Meena in front of Dawat University<br>
                          Kabul, Afghanistan<br>
                          Email: Marketing @sabacent.org<br>
                          Phone # 0703141414<br>
                    </td>
                  </tr>
                </tbody></table>
              </td>
            </tr>
            <tr>
              <td colspan = '2'>
                      <table width= '100%' cellpadding= '0' cellspacing= '0'>
                    <tbody><tr>
                      <td colspan= '2' style= 'font-size:13px;' ><b> Contractor:</b><br>
                        <b>Subject of Contract:</b> Broadcasting of Spots<br>
                          This contract is between NAWA RADIO 103.1FM as vender and (
                              <b>{ { jobDetails.ClientName } }</b> ) as a client.<br>
The contract is based on: <b>{ { jobDetails.StartDate } }</b> up
to <b>{{ jobDetails.EndDate } }</b><br>
                    </td>
                  </tr>
             <tr>
                    <td colspan = '2' style='font-size:15px;font-weight: 700; color:#000;'>
                        <p style = 'padding:10px  0;' > Both parties' responsibilities are as follow:</p>
                 </td>
             </tr>
             <tr>
                    <td colspan = '2' style='font-size:13px; color:#000;padding-left:20px;'>
                        <p>1. Broadcasting of(Spots) in NAWA Radio.</p>
                        <p>2. Radio airtimes should be in Flat time.</p>
                        <p>3. Programs status : ( )</p>
                        <p>4. The broadcasting will be provided by</p>
                        <p>The broadcasting cost of one month will be (
                                <b>{ { jobDetails.UnitRate } }</b> )</p>
                 </td>
             </tr>
             <tr>
                    <td colspan = '2' style='font-size:15px;font-weight: 700; color:#000;'>
                        <p style = 'padding:10px  0;' > Both parties' responsibilities are as follow:</p>
                 </td>
             </tr>
             <tr>
                    <td colspan = '2' style='font-size:13px; color:#000;padding-left:20px;'>
                        <p style = 'font-weight:700;' > 1 - Customer:</p>
                        <p>- Payment of amount.before the starting of broadcasting.<br>
                            - If client once approve the program format it will be his/her
                            responsibility even if any mistakes were there.<br>
                            - Provision of the schedule.<br>
                                    - The programs should not be against National benefits and Radio
                                    Nawa policies.
                        </p>
                        <p style = 'font-weight:700;' > 2 - NAWA RADIO</p>
                        <p>  - Broadcasting of (Spots) Audio programs in Flat times as per
                                the approved schedule.<br>
                            </p>
                 </td>
             </tr>
                  <tr>
                    <td colspan = '2' style= 'font-size:15px;padding-top:10px;' >
                      <b> Note:</b>  NAWA RADIO has no legal responsibility for the subjects and
                      contents of the programs and advertisements.
                    </td>
                  </tr>
                </tbody></table>
              </td>
            </tr>
            <tr>
              <td colspan = '2' ><hr></td>
            </tr>
            <tr>
              <td colspan= '2' >
                <p style= 'font-size:13px;' > Both parties are agreed to terms and conditions in the contract stated above.</p>
              </td>
            </tr>
            <tr>
              <td style = 'font-size: 15px;padding-top:10px;' >
                <p><b> NAWA RADIO</b></p>
                <p><b>Representative Name:</b></p>
                <p><b>Signature:</b>------</p>
                <p><b>Date:</b></p>
              </td>

              <td>
                <p><b>Customer's Name</b></p>

                <p><b>Signature:</b>-------</p>
              </td>
            </tr>
          </tbody></table>
    </div>
  </div>
</div>
";
      // create a new pdf document converting an url
      PdfDocument doc = converter.ConvertHtmlString(TxtHtmlCode);

      // save pdf document
      byte[] pdf = doc.Save();

      // close pdf document
      doc.Close();

      System.IO.File.WriteAllBytes(path, pdf);
      var result = new HttpResponseMessage(HttpStatusCode.OK)
      {
        Content = new ByteArrayContent(pdf)
      };
      result.Content.Headers.ContentDisposition =
          new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
          {
            FileName = path
          };
      result.Content.Headers.ContentType =
          new MediaTypeHeaderValue("application/pdf");
      
      return result;
    }

  }
}

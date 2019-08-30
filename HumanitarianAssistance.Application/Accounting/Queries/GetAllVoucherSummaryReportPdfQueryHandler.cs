using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Persistence;
using MediatR;
using RazorLight;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetAllVoucherSummaryReportPdfQueryHandler : IRequestHandler<GetAllVoucherSummaryReportPdfQuery, string>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IRazorLightEngine _razorEngine;

        public GetAllVoucherSummaryReportPdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IRazorLightEngine razorEngine)
        {
            _dbContext = dbContext;
            _razorEngine = razorEngine;
        }

        public async Task<string> Handle(GetAllVoucherSummaryReportPdfQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var model = new List<CarModel>()
            {
                new CarModel{NameOfCar="Audi Q7",FirstRegistration = DateTime.UtcNow.AddYears(-3),MaxSpeed = 200,NumberOfDoors = 4},
                new CarModel{NameOfCar="Audi A5",FirstRegistration = DateTime.UtcNow,MaxSpeed = 180,NumberOfDoors = 4},
                new CarModel{NameOfCar="Audi Q3",FirstRegistration = DateTime.UtcNow,MaxSpeed = 245,NumberOfDoors = 2},
                new CarModel{NameOfCar="Mercedes SLI",FirstRegistration = DateTime.UtcNow,MaxSpeed = 150,NumberOfDoors = 4},
                new CarModel{NameOfCar="Chevrolet",FirstRegistration = DateTime.UtcNow,MaxSpeed = 220,NumberOfDoors = 4},
                new CarModel{NameOfCar="BMW",FirstRegistration = DateTime.UtcNow,MaxSpeed = 200,NumberOfDoors = 4},
        };
                // var templatePath = $"./PdfTemplates/VoucherSummaryReport.cshtml";
                var templatePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), $"PdfTemplates/VoucherSummaryReport.cshtml");

                Console.WriteLine(templatePath);

                string template = await _razorEngine.CompileRenderAsync(templatePath, model);
                Console.WriteLine(template);

                var sb = new StringBuilder();
                sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>This is the generated PDF report!!!</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Name</th>
                                        <th>LastName</th>
                                        <th>Age</th>
                                        <th>Gender</th>
                                    </tr>");

                sb.Append(@"
                                </table>
                            </body>
                        </html>");


                Console.WriteLine(sb.ToString());
              
                return sb.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
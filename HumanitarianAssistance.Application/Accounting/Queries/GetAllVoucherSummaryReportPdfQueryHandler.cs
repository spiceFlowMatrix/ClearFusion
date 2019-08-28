using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RazorLight;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetAllVoucherSummaryReportPdfQueryHandler : IRequestHandler<GetAllVoucherSummaryReportPdfQuery, byte[]>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IRazorLightEngine _razorEngine;
        private readonly IConverter _pdfConverter;

        public GetAllVoucherSummaryReportPdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IRazorLightEngine razorEngine, IConverter pdfConverter)
        {
            _dbContext = dbContext;
            _razorEngine = razorEngine;
            _pdfConverter = pdfConverter;
        }

        public async Task<byte[]> Handle(GetAllVoucherSummaryReportPdfQuery request, CancellationToken cancellationToken)
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
                var templatePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), $"PdfTemplates/CarPdf.cshtml");
                string template = await _razorEngine.CompileRenderAsync(templatePath, model);
                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings() { Top = 10, Bottom = 10, Left = 10, Right = 10 },
                    DocumentTitle = "Simple PDF document",
                };
                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = template,
                    WebSettings = { DefaultEncoding = "utf-8" },
                    HeaderSettings = { FontName = "Arial", FontSize = 12, Line = true, Center = "Fun pdf document" },
                    FooterSettings = { FontName = "Arial", FontSize = 12, Line = true, Right = "Page [page] of [toPage]" }
                };
                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };
                byte[] file = _pdfConverter.Convert(pdf);
                return file;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.IO;
using HumanitarianAssistance.Application.CommonServicesInterface;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;

namespace HumanitarianAssistance.Application.CommonServices
{
    public class ExcelExportService : IExcelExportService
    {
        private IHostingEnvironment _env;
        public ExcelExportService(IHostingEnvironment env)
        {
            _env = env;
        }
        public byte[] ExportToExcel(List<ExpandoObject> model, string worksheetName, string excelHeaderString)
        {
            byte[] result;
            int rowCount = 4;

            try
            {
               // string fileName = "ExcellData.xlsx";
                //var file = new FileInfo(fileName);
                using (var package = new OfficeOpenXml.ExcelPackage(new MemoryStream()))
                {
                    // var worksheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == "Attempts");
                   var worksheet = package.Workbook.Worksheets.Add(worksheetName);

                    int Height = 135;
                    int Width = 55;
                    Image img = Image.FromFile(_env.WebRootFileProvider.GetFileInfo("ReportLogo/logo.jpg")?.PhysicalPath);
                    ExcelPicture pic = worksheet.Drawings.AddPicture("logo", img);
                    pic.SetPosition(0, 0, 0, 0);
                    pic.SetSize(Height, Width);

                    //Column Header Names
                    IDictionary<string, object> headerValues = model[0];
                    int cell = 1;
                    worksheet.Row(3).Height = 15;

                    // worksheet.Cells[FromRow, FromColumn, ToRow, ToColumn].Merge = true;

                    // Merge first 3 rows
                    worksheet.Cells[1, 1, 1, headerValues.Keys.Count].Merge = true;
                    worksheet.Cells[2, 1, 2, headerValues.Keys.Count].Merge = true;
                    worksheet.Cells[3, 1, 3, headerValues.Keys.Count].Merge = true;

                    using (ExcelRange Rng = worksheet.Cells[2, 1, 2, headerValues.Keys.Count])
                    {
                        Rng.Value = "Coordination of Humanitarian Assistance (CHA)";
                        Rng.Style.Font.Size = 16;
                        Rng.Style.Font.Bold = true;
                        Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    using (ExcelRange Rng = worksheet.Cells[3, 1, 3, headerValues.Keys.Count])
                    {
                        Rng.Value = excelHeaderString;
                        Rng.Style.Font.Size = 12;
                        Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    foreach (var property in headerValues.Keys)
                    {
                        worksheet.Cells[rowCount, cell].Value = property;
                        worksheet.Cells[rowCount, cell].Style.Font.Bold= true; 
                        cell++;
                    }

                    //Column values
                    foreach (var item in model)
                    {
                        IDictionary<string, object> propertyValues = item;
                        int cellCount = 1;
                        worksheet.Row(rowCount++).Height = 15;

                        foreach (var property in propertyValues.Keys)
                        {
                            worksheet.Cells[rowCount, cellCount].Value = propertyValues[property];
                            cellCount++;
                        }
                    }

                   // worksheet.Cells.AutoFitColumns();
                    result = package.GetAsByteArray();
                    package.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }
    }
}
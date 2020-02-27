using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.IO;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Common.Helpers;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using HumanitarianAssistance.Application.HR.Models;

namespace HumanitarianAssistance.Application.CommonServices
{
    public class ExcelExportService : IExcelExportService
    {
        private IHostingEnvironment _env;
        public ExcelExportService(IHostingEnvironment env)
        {
            _env = env;
        }
        public byte[] ExportToExcel(List<ExpandoObject> model, string worksheetName, string excelHeaderString, bool calculateSum, List<int> calculateSumOnKeyIndex)
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

                    //int Height = 135;
                    //int Width = 55;
                    //Image img = Image.FromFile(_env.WebRootFileProvider.GetFileInfo("ReportLogo/logo.jpg")?.PhysicalPath);
                    //ExcelPicture pic = worksheet.Drawings.AddPicture("logo", img);
                    //pic.SetPosition(0, 0, 0, 0);
                    //pic.SetSize(Height, Width);

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
                        worksheet.Cells[rowCount, cell].Style.Font.Bold = true;
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

                    if (calculateSum)
                    {
                        foreach (int index in calculateSumOnKeyIndex)
                        {
                            string text = StaticFunctions.GetCharacterFromACIICode(index); //get Alphabet character
                            string col = text + (model.Count + 5); // +5 to skip first 4 rows and last row where sum will be displayed
                            string formula = $"SUM({text}1:{text}{model.Count.ToString()})";
                            worksheet.Cells[text + (model.Count + 5).ToString()].Formula = $"SUM({text}5:{text}{(model.Count + 4).ToString()})";
                        }
                    }

                    // worksheet.Cells.AutoFitColumns();
                    worksheet.Calculate();
                    result = package.GetAsByteArray();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }



        public byte[] ExportEmployeePayrollExcel(EmployeesPayrollExcelModel model, List<string> headers, List<int> calculateSumOnKeyIndex)
        {
            byte[] result;
            int rowCount = 7;

            try
            {
                // string fileName = "ExcellData.xlsx";
                //var file = new FileInfo(fileName);
                using (var package = new OfficeOpenXml.ExcelPackage(new MemoryStream()))
                {
                    // var worksheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == "Attempts");
                    var worksheet = package.Workbook.Worksheets.Add("EmployeePayroll");

                    //int Height = 135;
                    //int Width = 55;
                    //Image img = Image.FromFile(_env.WebRootFileProvider.GetFileInfo("ReportLogo/logo.jpg")?.PhysicalPath);
                    //ExcelPicture pic = worksheet.Drawings.AddPicture("logo", img);
                    //pic.SetPosition(0, 0, 0, 0);
                    //pic.SetSize(Height, Width);

                    //Column Header Names
                    // IDictionary<string, object> headerValues = headers[0];
                    int cell = 1;
                    worksheet.Row(3).Height = 15;

                    // worksheet.Cells[FromRow, FromColumn, ToRow, ToColumn].Merge = true;

                    // Merge first 3 rows
                    worksheet.Cells[1, 1, 1, headers.Count].Merge = true;
                    worksheet.Cells[2, 1, 2, headers.Count].Merge = true;
                    worksheet.Cells[3, 1, 3, headers.Count].Merge = true;
                    worksheet.Cells[4, 1, 4, 4].Merge = true;
                    worksheet.Cells[5, 1, 5, 4].Merge = true;
                    worksheet.Cells[6, 1, 6, 4].Merge = true;

                    using (ExcelRange Rng = worksheet.Cells[2, 1, 2, headers.Count])
                    {
                        Rng.Value = model.HeaderAndFooter.Header;
                        Rng.Style.Font.Size = 18;
                        Rng.Style.Font.Bold = true;
                        Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    using (ExcelRange Rng = worksheet.Cells[3, 1, 3, headers.Count])
                    {
                        Rng.Value = model.HeaderAndFooter.SubHeader;
                        Rng.Style.Font.Size = 14;
                        Rng.Style.Font.Bold = true;
                        Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    using (ExcelRange Rng = worksheet.Cells[4, 1])
                    {
                        Rng.Value = "Currency: " + model.HeaderAndFooter.Currency;
                        Rng.Style.Font.Size = 11;
                        Rng.Style.Font.Bold = true;
                        //Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    using (ExcelRange Rng = worksheet.Cells[5, 1])
                    {
                        Rng.Value = "Office: " + model.HeaderAndFooter.Office;
                        Rng.Style.Font.Size = 11;
                        Rng.Style.Font.Bold = true;
                        //Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    using (ExcelRange Rng = worksheet.Cells[6, 1])
                    {
                        Rng.Value = "Payment Date: " + model.HeaderAndFooter.Date;
                        Rng.Style.Font.Size = 11;
                        Rng.Style.Font.Bold = true;
                        //Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    using (ExcelRange Rng = worksheet.Cells[7, 1, 7, headers.Count])
                    {
                        Rng.Style.Font.Size = 11;
                        Rng.Style.Font.Bold = true;
                        //Rng.Style.Font.Color.SetColor(Color.White);
                        //Rng.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);  
                        Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    foreach (var property in headers)
                    {
                        worksheet.Cells[rowCount, cell].Value = property;
                        worksheet.Cells[rowCount, cell].Style.Font.Bold = true;
                        cell++;
                    }

                    int sNo = 1;
                    // //Column values
                    foreach (var item in model.PayrollExcelData)
                    {
                        // int cellCount = 2;
                        worksheet.Row(rowCount++).Height = 15;

                        worksheet.Cells[rowCount, 1].Value = sNo;
                        worksheet.Cells[rowCount, 2].Value = item.Month;
                        worksheet.Cells[rowCount, 3].Value = item.EmployeeId;
                        worksheet.Cells[rowCount, 4].Value = item.Name;
                        worksheet.Cells[rowCount, 5].Value = item.Designation;
                        worksheet.Cells[rowCount, 6].Value = item.Gender;
                        worksheet.Cells[rowCount, 7].Value = item.Currency;
                        worksheet.Cells[rowCount, 8].Value = item.Office;
                        worksheet.Cells[rowCount, 9].Value = item.BasicPay;
                        worksheet.Cells[rowCount, 10].Value = item.AttendedHours;
                        worksheet.Cells[rowCount, 11].Value = item.AbsentHours;
                        worksheet.Cells[rowCount, 12].Value = item.Salary;
                        worksheet.Cells[rowCount, 13].Value = item.Bonus;
                        worksheet.Cells[rowCount, 14].Value = item.GrossSalary;
                        worksheet.Cells[rowCount, 15].Value = item.CapacityBuilding;
                        worksheet.Cells[rowCount, 16].Value = item.Security;
                        worksheet.Cells[rowCount, 17].Value = item.SalaryTax;
                        worksheet.Cells[rowCount, 18].Value = item.Fine;
                        worksheet.Cells[rowCount, 19].Value = item.Advance;
                        worksheet.Cells[rowCount, 20].Value = item.Pension;
                        worksheet.Cells[rowCount, 21].Value = item.NetSalary;
                        worksheet.Cells[rowCount, 22].Value = item.Project;
                        worksheet.Cells[rowCount, 23].Value = item.Job;
                        worksheet.Cells[rowCount, 24].Value = item.BudgetLine;
                        worksheet.Cells[rowCount, 25].Value = item.Percentage;
                        sNo++;
                    }

                    worksheet.Cells[rowCount+1, 1, rowCount+1, 8].Merge = true;
                    worksheet.Cells[rowCount+1, 1, rowCount+1, 8].Value = "Grand Total";
                    worksheet.Cells[rowCount+1, 1, rowCount+1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    foreach (int index in calculateSumOnKeyIndex)
                    {
                        string text = StaticFunctions.GetCharacterFromACIICode(index); //get Alphabet character
                        worksheet.Cells[text + (rowCount+1).ToString()].Formula = $"SUM({text}8:{text}{(rowCount).ToString()})";
                    }
                    // worksheet.Cells.AutoFitColumns();

                    worksheet.Cells[rowCount + 3, 2, rowCount + 3, 3].Merge = true;
                    worksheet.Cells[rowCount + 3, 2, rowCount + 3, 3].Value = "Prepared By";
                    worksheet.Cells[rowCount + 3, 2, rowCount + 3, 3].Style.Font.Bold  = true;
                    worksheet.Cells[rowCount + 3, 12, rowCount +3, 13].Merge = true;
                    worksheet.Cells[rowCount + 3, 12, rowCount +3, 13].Value = "Checked By";
                    worksheet.Cells[rowCount + 3, 12, rowCount +3, 13].Style.Font.Bold  = true;
                    worksheet.Cells[rowCount + 3, 21, rowCount +3, 22].Merge = true;
                    worksheet.Cells[rowCount + 3, 21, rowCount +3, 22].Value = "Approved By";
                    worksheet.Cells[rowCount + 3, 21, rowCount +3, 22].Style.Font.Bold = true;

                    worksheet.Cells[rowCount + 5, 2, rowCount + 5, 3].Merge = true;
                    worksheet.Cells[rowCount + 5, 2, rowCount + 5, 3].Value = "Date: "+ model.HeaderAndFooter.Date;

                    worksheet.Cells[rowCount + 5, 12, rowCount + 5, 13].Merge = true;
                    worksheet.Cells[rowCount + 5, 12, rowCount + 5, 13].Value = "Date: "+ model.HeaderAndFooter.Date;

                    worksheet.Cells[rowCount + 5, 21, rowCount + 5, 22].Merge = true;
                    worksheet.Cells[rowCount + 5, 21, rowCount + 5, 22].Value = "Date: "+ model.HeaderAndFooter.Date;


                    worksheet.Calculate();
                    result = package.GetAsByteArray();
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
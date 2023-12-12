using GymProject.Infrastructure.ViewModels;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.Report
{
    public class ReportManager
    {
        public byte[] GenerateReport<TEntity>(IEnumerable<TEntity> info)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Отчёт");

            var entityType = typeof(TEntity);

            var properties = entityType.GetProperties();

            var columnNames = new List<string>();
            var columnValues = new List<string>();

            
            int columnIndex = 1;
            foreach (var property in properties)
            {
                if (entityType == typeof(ClientViewModel) && (property.Name == "DiscountId"))
                    continue;
                if (entityType == typeof(EmployeeViewModel) && (property.Name == "PositionId"))
                    continue;
                if (entityType == typeof(ProductViewModel) && (property.Name == "ProductCategoryId"))
                    continue;
                if (entityType == typeof(SubscriptionViewModel) && (property.Name == "SubscriptionTypeId" || property.Name == "ClientId" || property.Name == "StatusId"))
                    continue;
                if (entityType == typeof(LessonViewModel) && (property.Name == "SubscriptionId" || property.Name == "SubscriptionTypeId" || property.Name == "HallId" || property.Name == "GymId" || property.Name == "ProgramId"))
                    continue;
                 if (entityType == typeof(SubscriptionViewModel) && property.Name == "Client")
                {  
                    columnNames.Add(property.Name);
                    sheet.Cells[1, 4].Value = "Name";
                    sheet.Cells[1, 5].Value = "SecondName";
                    sheet.Cells[1, 6].Value = "MiddleName";
                    sheet.Cells[1, 7].Value = "DateOfBirth";
                    sheet.Cells[1, 8].Value = "Login";
                    sheet.Cells[1, 9].Value = "Password";
                    
                 }
                else if (entityType == typeof(ClientViewModel) && property.Name == "Discount")
                {
                    sheet.Cells[1, columnIndex + 1].Value = "Value";
                    columnNames.Add(property.Name);
                    sheet.Cells[1, columnIndex].Value = "Name";
                }
                else if (entityType == typeof(SubscriptionViewModel) && property.Name == "Status")
                {
                    sheet.Cells[1, 10].Value = "Status";
                    columnNames.Add(property.Name);

                }
                else if (entityType == typeof(LessonViewModel) && property.Name == "Hall")
                {
                    sheet.Cells[1, 11].Value = "Hall";
                    columnNames.Add(property.Name);

                }
                else if (entityType == typeof(EmployeeViewModel) && property.Name == "Position")
                {
                    sheet.Cells[1, columnIndex + 1].Value = "Salary";
                    sheet.Cells[1, columnIndex + 2].Value = "WorkSchedule";
                    columnNames.Add(property.Name);
                    sheet.Cells[1, columnIndex].Value = "Title";
                }
                else if (entityType == typeof(SubscriptionViewModel) && property.Name == "Subscription_type")
                {
                    sheet.Cells[1, 12].Value = "Cost";
                    sheet.Cells[1, 13].Value = "Duration";
                    sheet.Cells[1, 14].Value = "NumberOfClasses";
                    sheet.Cells[1, 15].Value = "DateAndTimeOfPurchase";
                    columnNames.Add(property.Name);
                    sheet.Cells[1, 11].Value = "Name";
                }
               
                else if (entityType == typeof(LessonViewModel) && property.Name == "Lesson_program")
                {
                    sheet.Cells[1, 8].Value = "Description";
                    sheet.Cells[1,7].Value = "ProgramDuration";
                    columnNames.Add(property.Name);
                    sheet.Cells[1, 6].Value = "Name";
                }
                else if(entityType == typeof(LessonViewModel) && property.Name == "Gym")
                {
                    sheet.Cells[1, 3].Value = "StartTime";
                    sheet.Cells[1, 4].Value = "EndTime";
                    columnNames.Add(property.Name);
                    sheet.Cells[1, 5].Value = "Adress";
                }
                else if(entityType == typeof(LessonViewModel) && property.Name == "Subscription")
                {
                    sheet.Cells[1, 10].Value = "ValidityExpirationDate";
                    columnNames.Add(property.Name);
                    sheet.Cells[1, 9].Value = "ValidityStartDate";
                }
                else
                {
                    sheet.Cells[1, columnIndex].Value = property.Name;
                    columnNames.Add(property.Name);
                }

                columnIndex++;
            }
            int rowIndex = 2;
            foreach (var item in info)
            {
                columnIndex = 1;
                foreach (var propertyName in columnNames)
                {

                    var propertyValue = item.GetType().GetProperty(propertyName).GetValue(item);
                    if (propertyName == "Discount")
                    {
                        var cell = propertyValue as DiscountViewModel;
                        columnValues.Add(cell.Name);
                        columnValues.Add(cell.Value.ToString());
                    }
                    else if (propertyName == "ProductCategory")
                    {
                        var cell = propertyValue as ProductCategoryViewModel;
                        columnValues.Add(cell.Name);
                    }
                    else if (propertyName == "Position")
                    { 
                        var cell = propertyValue as PositionViewModel;
                        columnValues.Add(cell.Title);
                        columnValues.Add(cell.Salary.ToString());
                        columnValues.Add(cell.WorkSchedule);
                    }
                    else if (propertyName == "Hall")
                    {
                        var cell = propertyValue as HallViewModel;
                        columnValues.Add(cell.HallNumber.ToString());
                    }
                    else if (propertyName == "Client")
                    {
                        var cell = propertyValue as ClientViewModel;
                        columnValues.Add(cell.Name);
                        columnValues.Add(cell.SecondName);
                        columnValues.Add(cell.MiddleName);
                        columnValues.Add(cell.Login);
                        columnValues.Add(cell.Password);
                        columnValues.Add(cell.DateOfBirth);
                    }
                    else if (propertyName == "Status")
                    {
                        var cell = propertyValue as StatusViewModel;
                        columnValues.Add(cell.Title);
                    }
                    else if (propertyName == "Subscription_type")
                    {
                        var cell = propertyValue as SubscriptionTypeViewModel;
                        columnValues.Add(cell.Name);
                        columnValues.Add(cell.Cost.ToString()); 
                        columnValues.Add(cell.Duration.ToString());
                        columnValues.Add(cell.NumberOfClasses.ToString());
                        columnValues.Add(cell.DateAndTimeOfPurchase);
                    }
                    else if (propertyName == "Subscription")
                    {
                        var cell = propertyValue as SubscriptionViewModel;
                        columnValues.Add(cell.ValidityExpirationDate);
                        columnValues.Add(cell.ValidityStartDate);
                    }
                    else if (propertyName == "Gym")
                    {
                        var cell = propertyValue as GymViewModel;
                        columnValues.Add(cell.StartTime);
                        columnValues.Add(cell.EndTime);
                        columnValues.Add(cell.Adress);
                    }
                    else if (propertyName == "Lesson_program")
                    {
                        var cell = propertyValue as LessonProgramViewModel;
                        columnValues.Add(cell.Name);
                        columnValues.Add(cell.ProgramDuration.ToString());
                        columnValues.Add(cell.Description);
                    }
                    else
                    {
                        columnValues.Add(propertyValue.ToString());
                    }

                    columnIndex++;
                }
                for (int i = 1; i <= columnValues.Count; i++)
                {
                    sheet.Cells[rowIndex, i].Value = columnValues[i - 1];
                }
                rowIndex++;
                columnValues.Clear();
            }

            sheet.Cells.Style.Font.Name = "Times New Roman";
            sheet.Cells.Style.Font.Size = 12;
            sheet.Cells.Style.Font.Color.SetColor(System.Drawing.Color.Red);

            sheet.Cells.AutoFitColumns();

            return package.GetAsByteArray();
        }
    }
}

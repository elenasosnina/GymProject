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
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;// Установка лицензии для библиотеки EPPlus (работа с Excel).
            var package = new ExcelPackage(); // Создание нового пакета Excel.
            var sheet = package.Workbook.Worksheets.Add("Отчёт");// Добавление листа "Отчёт" в созданный пакет.
            // Получение типа объектов, для которых генерируется отчёт.
            var entityType = typeof(TEntity);

            var properties = entityType.GetProperties();// Получение свойств объектов.
            // Инициализация списков для хранения имен колонок и их значений.
            var columnNames = new List<string>();
            var columnValues = new List<string>();

            
            int columnIndex = 1; // Индекс колонки в Excel.
            foreach (var property in properties)// Цикл по свойствам объектов.
            {// Пропуск свойств для объектов определенного типа 
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
                // Логика формирования колонок для объектов различных типов.
                if (entityType == typeof(SubscriptionViewModel) && property.Name == "Client")
                {   // Добавление колонок для свойств объекта ClientViewModel.
                    columnNames.Add(property.Name);
                    sheet.Cells[1, 4].Value = "Name";
                    sheet.Cells[1, 5].Value = "SecondName";
                    sheet.Cells[1, 6].Value = "MiddleName";
                    sheet.Cells[1, 7].Value = "DateOfBirth";
                    sheet.Cells[1, 8].Value = "Login";
                    sheet.Cells[1, 9].Value = "Password";
                    
                 }
                else if (entityType == typeof(ClientViewModel) && property.Name == "Discount")
                {// Добавление колонок для свойств объекта 
                    sheet.Cells[1, columnIndex + 1].Value = "Value";
                    columnNames.Add(property.Name);
                    sheet.Cells[1, columnIndex].Value = "Name";
                }
                else if (entityType == typeof(SubscriptionViewModel) && property.Name == "Status")
                {// Добавление колонок для свойств объекта
                    sheet.Cells[1, 10].Value = "Status";
                    columnNames.Add(property.Name);

                }
                else if (entityType == typeof(LessonViewModel) && property.Name == "Hall")
                {// Добавление колонок для свойств объекта
                    sheet.Cells[1, 11].Value = "Hall";
                    columnNames.Add(property.Name);

                }
                else if (entityType == typeof(EmployeeViewModel) && property.Name == "Position")
                {// Добавление колонок для свойств объекта
                    sheet.Cells[1, columnIndex + 1].Value = "Salary";
                    sheet.Cells[1, columnIndex + 2].Value = "WorkSchedule";
                    columnNames.Add(property.Name);
                    sheet.Cells[1, columnIndex].Value = "Title";
                }
                else if (entityType == typeof(SubscriptionViewModel) && property.Name == "Subscription_type")
                {// Добавление колонок для свойств объекта
                    sheet.Cells[1, 12].Value = "Cost";
                    sheet.Cells[1, 13].Value = "Duration";
                    sheet.Cells[1, 14].Value = "NumberOfClasses";
                    sheet.Cells[1, 15].Value = "DateAndTimeOfPurchase";
                    columnNames.Add(property.Name);
                    sheet.Cells[1, 11].Value = "Name";
                }
               
                else if (entityType == typeof(LessonViewModel) && property.Name == "Lesson_program")
                {// Добавление колонок для свойств объекта
                    sheet.Cells[1, 8].Value = "Description";
                    sheet.Cells[1,7].Value = "ProgramDuration";
                    columnNames.Add(property.Name);
                    sheet.Cells[1, 6].Value = "Name";
                }
                else if(entityType == typeof(LessonViewModel) && property.Name == "Gym")
                {// Добавление колонок для свойств объекта
                    sheet.Cells[1, 3].Value = "StartTime";
                    sheet.Cells[1, 4].Value = "EndTime";
                    columnNames.Add(property.Name);
                    sheet.Cells[1, 5].Value = "Adress";
                }
                else if(entityType == typeof(LessonViewModel) && property.Name == "Subscription")
                {// Добавление колонок для свойств объекта
                    sheet.Cells[1, 10].Value = "ValidityExpirationDate";
                    columnNames.Add(property.Name);
                    sheet.Cells[1, 9].Value = "ValidityStartDate";
                }
                else
                {  // Добавление обычных колонок.
                    sheet.Cells[1, columnIndex].Value = property.Name;
                    columnNames.Add(property.Name);
                }

                columnIndex++;
            }
            int rowIndex = 2; // Индекс строки в Excel.
            foreach (var item in info)// Цикл по данным для заполнения ячеек отчёта.
            {
                columnIndex = 1;
                foreach (var propertyName in columnNames)// Заполнение значений ячеек отчёта.
                {

                    var propertyValue = item.GetType().GetProperty(propertyName).GetValue(item);
                    // Логика обработки особых случаев (например, связанных свойств).
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
                        columnValues.Add(propertyValue.ToString());  // Добавление обычных значений.
                    }

                    columnIndex++;
                }
                for (int i = 1; i <= columnValues.Count; i++)// Заполнение ячеек Excel значениями из списков
                {
                    sheet.Cells[rowIndex, i].Value = columnValues[i - 1];
                }
                rowIndex++;
                columnValues.Clear();
            }
            // Настройка стиля ячеек Excel.
            sheet.Cells.Style.Font.Name = "Times New Roman";
            sheet.Cells.Style.Font.Size = 12;
            sheet.Cells.Style.Font.Color.SetColor(System.Drawing.Color.Red);

            sheet.Cells.AutoFitColumns();// Автоматическая подгонка ширины колонок.

            return package.GetAsByteArray();// Возврат отчёта в виде массива байт
        }
    }
}

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
            sheet.Cells["A1"].LoadFromCollection(info, true);

            // Установка шрифта и размера шрифта
            sheet.Cells.Style.Font.Name = "Times New Roman";
            sheet.Cells.Style.Font.Size = 12;

            // Установка цвета шрифта
            sheet.Cells.Style.Font.Color.SetColor(System.Drawing.Color.Red);

            // Автоматическое определение ширины столбцов
            sheet.Cells.AutoFitColumns();

            return package.GetAsByteArray();
        }
    }
}

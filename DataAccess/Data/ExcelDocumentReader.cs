using Core.Domain;
using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Export;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class ExcelDocumentReader
    {
        public Task ReadAsync(string path)
        {
            using(Workbook wb = new Workbook())
            {
                wb.LoadDocument(path);
                DataTable dt = GetData(wb.Worksheets.First());

                DataFactory.Fill(dt);
            }
            return Task.CompletedTask;
        }
        private static DataTable GetData(Worksheet worksheet)
        {
            CellRange dataRangeWithHeaders = worksheet.GetDataRange();
            DataTable dataTable = worksheet.CreateDataTable(dataRangeWithHeaders, true);
            DataTableExporter exporter = worksheet.CreateDataTableExporter(dataRangeWithHeaders, dataTable, true);
            exporter.Export();
            return dataTable;
        }
      
    }

  
}

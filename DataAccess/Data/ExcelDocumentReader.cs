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
    public static class ExcelDocumentReader
    {
        public static List<Department> Read(string path)
        {
            using(Workbook wb = new Workbook())
            {
                wb.LoadDocument(path);
                DataTable dt = GetData(wb.Worksheets.First());
                
                return InitData(dt);
            }
        }
        private static DataTable GetData(Worksheet worksheet)
        {
            CellRange dataRangeWithHeaders = worksheet.GetDataRange();
            DataTable dataTable = worksheet.CreateDataTable(dataRangeWithHeaders, true);
            DataTableExporter exporter = worksheet.CreateDataTableExporter(dataRangeWithHeaders, dataTable, true);
            exporter.Export();
            return dataTable;
        }
        private static List<Department> InitData(DataTable dataTable)
        {
            if (dataTable.Rows.Count == 0) return null;

            int deptId = 0;
            int userId = 0;
            List<Department> departments = new List<Department>();
            foreach(DataRow dataRow in dataTable.Rows)
            {
                string deptName = dataRow["Отдел"].ToString().Trim();
                Department? department = departments.Find(dept => dept.Name == deptName);
                if (department == null)
                {
                    Department dept = DataRowToDepartmentObjectMapper.Create(dataRow, deptId, userId);
                    departments.Add(dept);
                    deptId++;
                    userId++;
                }
                else
                {
                    DataRowToDepartmentObjectMapper.AddUser(dataRow, department, userId);
                    userId++;
                }
               
            }
            return departments;
        }
    }

  
}

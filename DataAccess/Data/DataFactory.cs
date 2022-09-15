using Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public static class DataFactory
    {
        public static List<Department> Departments { get;  private set; } = new List<Department>();

        public static bool Fill(DataTable dataTable)
        {
            if (dataTable.Rows.Count == 0) return false;

            Departments.Clear();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                string deptName = dataRow["Отдел"].ToString().Trim();
                Department? department = Departments.Find(dept => dept.Name == deptName);
                if (department == null)
                {
                    string parentDeptName = dataRow["Родительский Отдел"].ToString().Trim();
                    Department? parentDepartment = null;
                    Department dept = null;
                    if (!string.IsNullOrEmpty(parentDeptName))
                    {
                        parentDepartment = Departments.Find(dept => dept.Name == parentDeptName);
                        if (parentDepartment == null)
                        {
                            parentDepartment = DataRowToDepartmentObjectMapper.Create(dataRow, parentDeptName,
                                null, false);
                           
                        }
                        dept = DataRowToDepartmentObjectMapper.Create(dataRow, deptName, parentDepartment, true);
                        parentDepartment.SubDepartments ??= new List<Department>();
                        parentDepartment.SubDepartments.Add(dept);
                    }
                    else
                        dept = DataRowToDepartmentObjectMapper.Create(dataRow, deptName, null, true);
                    Departments.Add(dept);
                }
                else
                {
                    DataRowToDepartmentObjectMapper.AddUser(dataRow, department);
                }

            }
            return true;
        }
    }
}

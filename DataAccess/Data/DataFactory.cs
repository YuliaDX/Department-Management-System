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
        private static List<Department> departments;

        public static List<Department> Departments
        {
            get
            {
                if (departments == null)
                    departments = new List<Department>();
                return departments;
            }

            private set => departments = value;
        }

        public static bool Fill(DataTable dataTable)
        {
            if (dataTable.Rows.Count == 0) return false;

            int deptId = 0;
            int userId = 0;
            Departments.Clear();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                string deptName = dataRow["Отдел"].ToString().Trim();
                Department? department = departments.Find(dept => dept.Name == deptName);
                if (department == null)
                {
                    string parentDeptName = dataRow["Родительский Отдел"].ToString().Trim();
                    Department? parentDepartment = null;
                    Department dept = null;
                    if (!string.IsNullOrEmpty(parentDeptName))
                    {
                        parentDepartment = departments.Find(dept => dept.Name == parentDeptName);
                        if (parentDepartment == null)
                        {
                            parentDepartment = DataRowToDepartmentObjectMapper.Create(dataRow, deptId, parentDeptName,
                                null, null);
                            deptId++;
                        }
                        dept = DataRowToDepartmentObjectMapper.Create(dataRow, deptId, deptName, parentDepartment, userId);
                    }
                    else
                        dept = DataRowToDepartmentObjectMapper.Create(dataRow, deptId, deptName, null, userId);
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
            return true;
        }
    }
}

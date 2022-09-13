using Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public static class DataRowToDepartmentObjectMapper
    {
        public static Department Create(DataRow row, string deptName,
            Department parentDepartment, bool hasUser)
        {
            Department dept = new Department();
            dept.Users = new List<User>();
            dept.Name = deptName;
            dept.ParentDepartment = parentDepartment;

            if (hasUser)
                AddUser(row, dept);
            
            return dept;
        }
        public static bool AddUser(DataRow row, Department department)
        {
            User user = new User();
            user.FullName = row["Пользователь"].ToString();
            user.Position = row["Должность"].ToString();
            department.Users.Add(user);
            return true;
        }
    }
}

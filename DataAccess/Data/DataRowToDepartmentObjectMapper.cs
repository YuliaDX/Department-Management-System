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
        public static Department Create(DataRow row, int id, string deptName,
            Department parentDepartment, int? userId = null)
        {
            Department dept = new Department();
            dept.Id = id;
            dept.Users = new List<User>();
            dept.Name = deptName;
            dept.ParentDepartment = parentDepartment;
            dept.ParentDepartmentId = parentDepartment?.Id;

            if (userId != null)
                AddUser(row, dept, (int)userId );
            
            return dept;
        }
        public static bool AddUser(DataRow row, Department department, int userId)
        {
            User user = new User();
            user.Id = userId;
            user.FullName = row["Пользователь"].ToString();
            user.Position = row["Должность"].ToString();
            user.DepartmentId = department.Id;
            department.Users.Add(user);
            return true;
        }
    }
}

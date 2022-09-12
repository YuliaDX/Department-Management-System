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
        public static Department Create(DataRow row, int id, int userId)
        {
            Department dept = new Department();
            dept.Id = id;
            dept.Users = new List<User>();
            dept.Name = row["Отдел"].ToString();

            User user = new User();
            user.Id = userId;
            user.FullName = row["Пользователь"].ToString();
            user.Position = row["Должность"].ToString();
            user.DepartmentId = id;
            dept.Users.Add(user);

            //dept.ParentDepartment = 
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

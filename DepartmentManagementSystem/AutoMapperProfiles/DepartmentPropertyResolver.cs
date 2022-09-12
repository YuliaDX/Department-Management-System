using AutoMapper;
using Core.Domain;
using DepartmentManagementSystem.DTO;

namespace DepartmentManagementSystem.AutoMapperProfiles
{
    public class CustomResolver : IValueResolver<Department, DepartmentResponse, string>
    {
        public string Resolve(Department source, DepartmentResponse destination, string member, ResolutionContext context)
        {
            return source.ParentDepartment?.Name;
        }
    }
}

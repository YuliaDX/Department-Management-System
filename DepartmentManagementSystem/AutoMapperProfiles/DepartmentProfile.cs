using AutoMapper;
using Core.Domain;
using DepartmentManagementSystem.DTO;

namespace DepartmentManagementSystem.AutoMapperProfiles
{
    public class DepartmentProfile:Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentShortResponse>();
            CreateMap<CreateOrEditDepartmentRequest, Department> ();
        }
    }
}

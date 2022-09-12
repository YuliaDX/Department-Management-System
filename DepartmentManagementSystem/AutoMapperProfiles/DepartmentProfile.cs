using AutoMapper;
using Core.Domain;
using DepartmentManagementSystem.DTO;

namespace DepartmentManagementSystem.AutoMapperProfiles
{
    public class DepartmentProfile:Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentResponse>().
                ForMember(dest => dest.ParentDepartment, opt => opt.MapFrom(new CustomResolver()));
            CreateMap<CreateOrEditDepartmentRequest, Department> ();
        }
    }
}

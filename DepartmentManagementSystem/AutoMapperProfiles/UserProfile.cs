using AutoMapper;
using Core.Domain;
using DepartmentManagementSystem.DTO;

namespace DepartmentManagementSystem.AutoMapperProfiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponse>();
        }
    }
}

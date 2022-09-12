namespace DepartmentManagementSystem.DTO
{
    public class DepartmentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserResponse> Users { get; set; }
        public string ParentDepartment { get; set; }
    }
    public class DepartmentShortResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
